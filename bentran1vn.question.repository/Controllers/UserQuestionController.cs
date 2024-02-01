using bentran1vn.question.src.Extensions;
using bentran1vn.question.src.Requests.UserQuestion;
using bentran1vn.question.src.Services.UserQuestion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bentran1vn.question.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAllQuestion([FromQuery]int page, [FromQuery]int num_of_ele)
        {
            var userId = HeaderExtensions.GetUserIdFromTokenHeader(HttpContext);

            if (userId == String.Empty) {
                return BadRequest("Get User's Questions Fail !");
            }

            var result = await _userQuestionServices.GetAllUserQuestionAsync(userId, page, num_of_ele);
            return Ok(result);
        }

        [HttpGet("{question_id}")]
        [Authorize]
        public async Task<IActionResult> GetUserQuestion(int question_id)
        {
            var userId = HeaderExtensions.GetUserIdFromTokenHeader(HttpContext);

            if (userId == String.Empty)
            {
                return BadRequest("Get User's Questions Fail !");
            }

            var result = await _userQuestionServices.GetUserQuestionAsync(userId, question_id);
            return Ok(result);
        }

        [HttpDelete("{question_id}")] 
        [Authorize]
        public async Task<IActionResult> RemoveUserQuestion(int question_id)
        {
            var userId = HeaderExtensions.GetUserIdFromTokenHeader(HttpContext);

            if (userId == String.Empty)
            {
                return BadRequest("Get User's Questions Fail !");
            }

            await _userQuestionServices.RemoveUserQuestionAsync(userId, question_id);
            return Ok("Remove User Question Successfully !");
        }

        [HttpPost("{question_id}")]
        public async Task<IActionResult> AddQuestionToQuestionBag(int question_id)
        {
            var user_id = HeaderExtensions.GetUserIdFromTokenHeader(HttpContext);
            await _userQuestionServices.AddQuestionToQuestionBagAsync(question_id, user_id);
            return Ok("");
        }
    }
}
