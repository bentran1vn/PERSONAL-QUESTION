using bentran1vn.question.src.Repositories.User;
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
        IAccountRepository _accountRepository;
        public TestController(IAccountRepository accountRepository) {
            _accountRepository = accountRepository;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("api/[controller]/testing")]
        public async Task<IActionResult> Get()
        {
            var users = await _accountRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("api/[controller]/testing")]
        [Authorize]
        public IActionResult Post()
        {
            throw new Exception("ahihi");
        }
    }
}
