using bentran1vn.question.repository.Datas.Entities;

namespace bentran1vn.question.src.Services.PublicQuestion
{
    public interface IPublicQuestionServices
    {
        public Task PublicUserQuestionAsync(int questionId, string userId);
        public Task UnPublicUserQuestionAsync(int questionId, string userId);
        public Task UnPublicUserQuestionAsync(int questionId);
        public Task<IEnumerable<PublicQuestions>> GetPublicQuestionsAsync(int page, int num_of_question);
    }
}
