using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Requests.QuestionAnswers;
using bentran1vn.question.src.Respones.QuestionAnswer;

namespace bentran1vn.question.src.Services.QuestionAnswer
{
    public interface IQuestionAnswerServices
    {
        public Task AddAnswerForQuestionAsync(AddNewQuestionAnswerModel model);
        public Task RemoveAnswerForQuestionAsync(int answerId);
        public Task UpdateAnswersForQuestionAsync(UpdateQuestionAnswerModel model);
        public Task<IEnumerable<GetAnswerRespone>> GetAnswersForQuestionAsync(int questionId);
        public Task<GetAnswerRespone> GetAnswerForQuestionAsync(int answerId);
    }
}
