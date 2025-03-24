using APIsLayer.Errors;
using APIsLayer.Helpers;
using CoreLayer.RepoContract;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Contract;
using RepoLayer.UnitOfWork;

namespace APIsLayer.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            //services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<UnitOfWork>();
            services.AddScoped<IBasketRepo, BasketRepo>();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0).SelectMany(p => p.Value.Errors).Select(e => e.ErrorMessage).ToArray();
                    var reponse = new APIResponseValidationError(errors);
                    return new BadRequestObjectResult(reponse);
                };
            });
            return services;
        }
    }
}
