using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Services.QuestionAnswer;

namespace bentran1vn.question.src.Repositories.QuestionAnswer
{
    public interface IQuestionAnswerRepository
    {
        public Task AddAnswerAsync(QuestionAnswers answer);
        public Task RemoveAnswerAsync(QuestionAnswers answer);
        public Task UpdateAnswersAsync(QuestionAnswers answer);
        public Task<IEnumerable<QuestionAnswers>> GetAllAnswerForQuestionAsync(int questionId);
        public Task<QuestionAnswers> GetAnswerAsync(int answerId);
    }
}
