using bentran1vn.question.src.Extensions;
using bentran1vn.question.src.Requests.UserQuestion;
using bentran1vn.question.src.Services.UserQuestion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bentran1vn.question.src.Controllers
{
    [Route("api/user_questions")]
    [ApiController]
    public class UserQuestionController : ControllerBase
    {
        private readonly IUserQuestionServices _userQuestionServices;
        public UserQuestionController(IUserQuestionServices userQuestionServices)
        {
            _userQuestionServices = userQuestionServices;
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> AddNewQuestion(AddNewQuestionModel model)
        {
            var userId = HeaderExtensions.GetUserIdFromTokenHeader(HttpContext);

            if (userId == String.Empty)
            {
                return BadRequest("Adding New Question Fail !");
            }

            await _userQuestionServices.AddingNewUserQuestionAsync(model, userId);

            return Ok("Adding New Question Successfully !");
        }

        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetAllQuestion()
        {
            var userId = HeaderExtensions.GetUserIdFromTokenHeader(HttpContext);

            if (userId == String.Empty) {
                return BadRequest("Get User's Questions Fail !");
            }

            var result = await _userQuestionServices.GetAllUserQuestionAsync(userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserQuestion(int id)
        {
            var userId = HeaderExtensions.GetUserIdFromTokenHeader(HttpContext);

            if (userId == String.Empty)
            {
                return BadRequest("Get User's Questions Fail !");
            }

            var result = await _userQuestionServices.GetUserQuestionAsync(userId, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveUserQuestion(int id)
        {
            var userId = HeaderExtensions.GetUserIdFromTokenHeader(HttpContext);

            if (userId == String.Empty)
            {
                return BadRequest("Get User's Questions Fail !");
            }

            await _userQuestionServices.RemoveUserQuestionAsync(userId, id);
            return Ok("Remove User Question Successfully !");
        }
    }
}
