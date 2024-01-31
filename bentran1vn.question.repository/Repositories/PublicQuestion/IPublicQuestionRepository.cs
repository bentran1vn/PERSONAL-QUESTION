using bentran1vn.question.repository.Datas.Entities;

namespace bentran1vn.question.src.Repositories.PublicQuestion
{
    public interface IPublicQuestionRepository
    {
        public Task PublicUserQuestion(PublicQuestions question);
        public Task UpdatePublicQuestion(PublicQuestions question);
        public Task<PublicQuestions> GetPublicQuestion(int questionId);
    }
}
