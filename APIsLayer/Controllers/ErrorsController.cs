using APIsLayer.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIsLayer.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            if (code == 401)
                return Unauthorized(new APIResponse(401));
            else if (code == 404)
                return NotFound(new APIResponse(404));
            return StatusCode(code);
                 
                   
           
        }
             
    }
}
