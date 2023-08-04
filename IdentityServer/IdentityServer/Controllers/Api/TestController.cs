using Duende.IdentityServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers.Api
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        [Authorize(Roles = "superadmin")]
        [HttpGet] 
        public ActionResult<string> Test() {
           
            return Ok("hello!");
        }

    }
}
