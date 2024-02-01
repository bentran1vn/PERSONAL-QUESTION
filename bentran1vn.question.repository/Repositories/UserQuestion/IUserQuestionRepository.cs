using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Requests.UserQuestion;

namespace bentran1vn.question.src.Repositories.UserQuestion
{
    public interface IUserQuestionRepository
    {
        public Task AddNewUserQuestionAsync(AddNewQuestionModel model, string userId);
        public Task AddNewUserQuestionAsync(UserQuestions user_question);
        public Task<IEnumerable<UserQuestions>> GetAllUserQuestionAsync(string userId, int page, int num_of_ele);
        public Task<UserQuestions> GetUserQuestionContentAsync(string userId, int questionId);
        public Task RemoveUserQuestionAsync(UserQuestions questions);
        public Task UpdateUserQuestionAsync(UserQuestions questions);
    }
}
