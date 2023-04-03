using DattingAppUpdate.Entites;
using DattingAppUpdate.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DattingAppUpdate.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        public BuggyController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return Ok("secret text");
        }

        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var thing = await _userManager.FindByNameAsync("xxxx");

            if (thing == null) return NotFound(new ApiErrorResponse(404));

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public async Task<ActionResult<string>> GetServerError()
        {
            var thing = await _userManager.FindByNameAsync("xxxx");

            var thingToReturn = thing.ToString();

            return Ok(thingToReturn);
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(401));
        }
    }
}
