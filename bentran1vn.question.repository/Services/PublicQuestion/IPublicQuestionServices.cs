namespace bentran1vn.question.src.Services.PublicQuestion
{
    public interface IPublicQuestionServices
    {
        public Task PublicUserQuestion(int questionId, string userId);
        public Task UnPublicUserQuestion(int questionId);
    }
}
