using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //Olmayan endpoint user tarafından girildiği  zaman vermesi gereken hata kodunu refaktöre ettik 
    // will override the routes that we get from our BaseApiController
    [Route("errors/{code}")]
    //swaggerda göstermemesi için
    [ApiExplorerSettings(IgnoreApi = true)]  // swaggerdaki girişteki hatayı kaldırmak için
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}