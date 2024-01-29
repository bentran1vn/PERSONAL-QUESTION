using bentran1vn.question.src.Repositories.User;
using bentran1vn.question.src.Requests.Account;
using bentran1vn.question.src.Requests.UserRequests;
using bentran1vn.question.src.Services.RefreshToken;
using bentran1vn.question.src.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace bentran1vn.question.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IRefreshTokenServices _refreshTokenServices;
        public AccountController(IUserServices userServices, IRefreshTokenServices refreshTokenServices)
        {
            _userServices = userServices;
            _refreshTokenServices = refreshTokenServices;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await _userServices.SignUpAsync(model);
            return Ok(result);
        }

        [HttpPost("/refreshToken")]
        public async Task<IActionResult> refreshToken(RefreshTokenModel model)
        {
            var result = await _refreshTokenServices.refreshAccessTokenAsync(model);
            return Ok(result);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var result = await _userServices.SignInAsync(model);
            return Ok(result);
        }

        [HttpPost("/logout")]
        public async Task<IActionResult> SignOut(RefreshTokenModel model)
        {
            await _userServices.SignOutAsync(model);
            return Ok("Logout Successfully");
        }
    }
}
