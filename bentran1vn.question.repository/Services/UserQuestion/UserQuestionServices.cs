using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Repositories.UserQuestion;
using bentran1vn.question.src.Requests.UserQuestion;

namespace bentran1vn.question.src.Services.UserQuestion
{
    public class UserQuestionServices : IUserQuestionServices
    {
        private readonly IUserQuestionRepository _userQuestionRepository;
        public UserQuestionServices(IUserQuestionRepository userQuestionRepository) 
        {
            _userQuestionRepository = userQuestionRepository;
        }
        public async Task AddingNewUserQuestionAsync(AddNewQuestionModel model, string userId)
        {
            try
            {
                await _userQuestionRepository.AddNewUserQuestionAsync(model, userId);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserQuestions>> GetAllUserQuestionAsync(string userId)
        {
            try
            {
                var result = await _userQuestionRepository.GetAllUserQuestionAsync(userId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserQuestions> GetUserQuestionAsync(string userId, int questionId)
        {
            try
            {
                var result = await _userQuestionRepository.GetUserQuestionContentAsync(userId, questionId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveUserQuestionAsync(string userId, int questionId)
        {
            try
            {
                var question = await _userQuestionRepository.GetUserQuestionContentAsync(userId, questionId);
                await _userQuestionRepository.RemoveUserQuestionAsync(question);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
