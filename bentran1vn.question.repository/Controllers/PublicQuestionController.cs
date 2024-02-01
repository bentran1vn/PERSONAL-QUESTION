using bentran1vn.question.src.Extensions;
using bentran1vn.question.src.Services.PublicQuestion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bentran1vn.question.src.Controllers
{
    [Route("api/public_question")]
    [ApiController]
    public class PublicQuestionController : ControllerBase
    {
        private readonly IPublicQuestionServices _publicQuestionServices;
        public PublicQuestionController(IPublicQuestionServices publicQuestionServices) 
        {
            _publicQuestionServices = publicQuestionServices;
        }

        [HttpGet("")]
        //[Authorize]
        public async Task<IActionResult> GetAllPublicQuestion(int page, int num_of_question)
        {
            var result = await _publicQuestionServices.GetPublicQuestionsAsync(page, num_of_question);
            return Ok(result);
        }

        [HttpDelete("admin")]
        public async Task<IActionResult> UnPublicQuestion(int questionId)
        {
            await _publicQuestionServices.UnPublicUserQuestionAsync(questionId);
            return Ok("Public User Question Successfully !");
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> PublicUserQuesion(int questionId)
        {
            var userId = HeaderExtensions.GetUserIdFromTokenHeader(HttpContext);
            await _publicQuestionServices.PublicUserQuestionAsync(questionId, userId);
            return Ok("Public User Question Successfully !");
        }

        [HttpDelete("user")]
        public async Task<IActionResult> UnPublicUserQuesion(int questionId)
        {
            var userId = HeaderExtensions.GetUserIdFromTokenHeader(HttpContext);
            await _publicQuestionServices.UnPublicUserQuestionAsync(questionId, userId);
            return Ok("Public User Question Successfully !");
        }
    }
}
