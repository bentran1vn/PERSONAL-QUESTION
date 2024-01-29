using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Requests.UserQuestion;

namespace bentran1vn.question.src.Services.UserQuestion
{
    public interface IUserQuestionServices
    {
        public Task AddingNewUserQuestionAsync(AddNewQuestionModel model, string userId);

        public Task<IEnumerable<UserQuestions>> GetAllUserQuestionAsync(string userId);

        public Task<UserQuestions> GetUserQuestionAsync(string userId, int questionId);

        public Task RemoveUserQuestionAsync(string userId, int questionId);
    }
}
