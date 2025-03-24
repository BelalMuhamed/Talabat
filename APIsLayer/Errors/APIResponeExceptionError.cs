namespace APIsLayer.Errors
{
    public class APIResponeExceptionError:APIResponse
    {
        public string? details { get; set; }
        public APIResponeExceptionError(int statuscode, string? message = null, string? Details = null):base(statuscode,message)
        {
            details = Details;
        }
    }
}
