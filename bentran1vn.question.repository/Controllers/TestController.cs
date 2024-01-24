using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bentran1vn.question.src.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class TestController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("api/[controller]/testing")]
        public IActionResult Get()
        {
            return Ok("Mlem");
        }

        [HttpPost("api/[controller]/testing")]
        [Authorize]
        public IActionResult Post()
        {
            return Ok("Mlem");
        }
    }
}
