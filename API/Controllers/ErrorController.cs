using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // will override the routes that we get from our BaseApiController
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]  // swaggerdaki hatayı kaldırmak için
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}