using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Requests.UserQuestion;

namespace bentran1vn.question.src.Repositories.UserQuestion
{
    public interface IUserQuestionRepository
    {
        public Task AddNewUserQuestionAsync(AddNewQuestionModel model, string userId);
        public Task<IEnumerable<UserQuestions>> GetAllUserQuestionAsync(string userId);
        public Task<UserQuestions> GetUserQuestionContentAsync(string userId, int questionId);
        public Task RemoveUserQuestionAsync(UserQuestions questions);
    }
}
