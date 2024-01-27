using bentran1vn.question.src.Repositories.User;
using bentran1vn.question.src.Requests.UserRequests;
using bentran1vn.question.src.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace bentran1vn.question.src.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("api/[controller]/register")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await _userServices.SignUpAsync(model);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();
        }

        [HttpPost("api/[controller]/login")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var result = await _userServices.SignInAsync(model);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
