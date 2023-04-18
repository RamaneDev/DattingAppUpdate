using DattingAppUpdate.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DattingAppUpdate.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}
