using bentran1vn.question.src.Extensions;
using bentran1vn.question.src.Requests.UserQuestion;
using bentran1vn.question.src.Services.UserQuestion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bentran1vn.question.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQuestionController : ControllerBase
    {
        private readonly IUserQuestionServices _userQuestionServices;
        public UserQuestionController(IUserQuestionServices userQuestionServices)
        {
            _userQuestionServices = userQuestionServices;
        }

        [HttpPost("/add_new_question")]
        [Authorize]
        public async Task<IActionResult> AddNewQuestion(AddNewQuestionModel model)
        {
            var authorizationHeader =  HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                var accessToken = authorizationHeader.Substring("Bearer ".Length);

                var tokenPayLoad = JwtExtensions.DecodeJwt(accessToken);

                await _userQuestionServices.AddingNewUserQuestionAsync(model, tokenPayLoad["nameid"]);

                return Ok("Adding New Question Successfully !");
            }

            return BadRequest("Adding New Question Fail !");
        }

        [HttpGet("/get_all_question")]
        [Authorize]
        public async Task<IActionResult> GetAllQuestion()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                var accessToken = authorizationHeader.Substring("Bearer ".Length);

                var tokenPayLoad = JwtExtensions.DecodeJwt(accessToken);

                var result = await _userQuestionServices.GetAllUserQuestionAsync(tokenPayLoad["nameid"]);

                return Ok(result);
            }

            return BadRequest("Get User's Questions Fail !");
        }
    }
}
