using DattingAppUpdate.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DattingAppUpdate.Controllers
{
    [Route("errors/{code}")]
    public class ErrorController : BaseApiController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiErrorResponse(code, "Page not found !"));
        }
    }
}
