using bentran1vn.question.src.Requests.QuestionAnswers;
using bentran1vn.question.src.Services.QuestionAnswer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bentran1vn.question.src.Controllers
{
    [Route("api/question_answer")]
    [ApiController]
    public class QuestionAnswerController : ControllerBase
    {
        private readonly IQuestionAnswerServices _questionAnswerServices;

        public QuestionAnswerController(IQuestionAnswerServices questionAnswerServices)
        {
            _questionAnswerServices = questionAnswerServices;
        }

        [HttpGet("answer_id")]
        [Authorize]
        public async Task<IActionResult> GetQuestionAnswer(int answer_id)
        {
            var result = await _questionAnswerServices.GetAnswerForQuestionAsync(answer_id);
            return Ok(result);
        }

        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetAllQuestionAnswer(string question_id) 
        {
            var questionIdInt = int.Parse(question_id);
            var result = await _questionAnswerServices.GetAnswersForQuestionAsync(questionIdInt);
            return Ok(result);
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> AddNewQuestionAnswer(AddNewQuestionAnswerModel model)
        {
            await _questionAnswerServices.AddAnswerForQuestionAsync(model);
            return Ok("Adding Answer For Question Successfully !");
        }

        [HttpDelete("{answer_id}")]
        [Authorize]
        public async Task<IActionResult> RemoveQuestionAnswer(int answer_id)
        {
            await _questionAnswerServices.RemoveAnswerForQuestionAsync(answer_id);
            return Ok("Removing Answer For Question Successfully !");
        }

        [HttpPut("")]
        [Authorize]
        public async Task<IActionResult> UpdateQuestionAnswer(UpdateQuestionAnswerModel model)
        {
            await _questionAnswerServices.UpdateAnswersForQuestionAsync(model);
            return Ok("Updating Answer Successfully !");
        }

    }
}
