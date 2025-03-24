using APIsLayer.Errors;
using System.Text.Json;

namespace APIsLayer.MiddleWares
{
    public class ExceptionMiddlewarecs
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddlewarecs> logger;
        private readonly IWebHostEnvironment env;

        public ExceptionMiddlewarecs(RequestDelegate next ,  ILogger<ExceptionMiddlewarecs> logger ,IWebHostEnvironment env )
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }
        public async  Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await next.Invoke(httpcontext);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                httpcontext.Response.StatusCode = 500;
                httpcontext.Response.ContentType = "application/json";
                var response = env.IsDevelopment() ? new APIResponeExceptionError(500, ex.Message, ex.StackTrace.ToString()) : new APIResponeExceptionError(500);
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var jsonresponse = JsonSerializer.Serialize(response, options);
                await httpcontext.Response.WriteAsync(jsonresponse);
            }
        }
    }
}
