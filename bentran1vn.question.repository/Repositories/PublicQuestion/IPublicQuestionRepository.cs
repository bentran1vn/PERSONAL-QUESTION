using bentran1vn.question.repository.Datas.Entities;

namespace bentran1vn.question.src.Repositories.PublicQuestion
{
    public interface IPublicQuestionRepository
    {
        public Task PublicUserQuestion(PublicQuestions question);
        public Task UpdatePublicQuestionAsync(PublicQuestions question);
        public Task<PublicQuestions> GetUserPublicQuestion(int questionId, string user_id);
        public Task<PublicQuestions> GetPublicQuestion(int user_question_Id);
        public Task<IEnumerable<PublicQuestions>> GetAllPublicQuestions(int page, int num_of_question);
    }
}
