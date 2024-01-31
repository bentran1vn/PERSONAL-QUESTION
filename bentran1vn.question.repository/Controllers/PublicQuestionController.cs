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

        [HttpPost("public")]
        [Authorize]
        public async Task<IActionResult> PublicUserQuesion(int questionId)
        {
            var userId = HeaderExtensions.GetUserIdFromTokenHeader(HttpContext);
            await _publicQuestionServices.PublicUserQuestion(questionId, userId);
            return Ok("Public User Question Successfully !");
        }

        [HttpPost("unpublic")]
        public async Task<IActionResult> UnPublicUserQuesion(int questionId)
        {
            await _publicQuestionServices.UnPublicUserQuestion(questionId);
            return Ok("Public User Question Successfully !");
        }
    }
}
