using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Datas.Enums;
using bentran1vn.question.src.Repositories.PublicQuestion;
using bentran1vn.question.src.Repositories.UserQuestion;
using bentran1vn.question.src.Requests.UserQuestion;

namespace bentran1vn.question.src.Services.UserQuestion
{
    public class UserQuestionServices : IUserQuestionServices
    {
        private readonly IUserQuestionRepository _userQuestionRepository;
        public readonly IPublicQuestionRepository _publicQuestionRepository;
        public UserQuestionServices(IUserQuestionRepository userQuestionRepository,
            IPublicQuestionRepository publicQuestionRepository) 
        {
            _userQuestionRepository = userQuestionRepository;
            _publicQuestionRepository = publicQuestionRepository;
        }
        public async Task AddingNewUserQuestionAsync(AddNewQuestionModel model, string userId)
        {
            try
            {
                //await _publicQuestionRepository.AddNewUserQuestionAsync(model, userId);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adding Public Question To User Question Bag
        /// </summary>
        /// <param name="question_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddQuestionToQuestionBagAsync(int question_id, string user_id)
        {
            try
            {
                //Finding in Public 
                var publicQuestion = await _publicQuestionRepository.GetPublicQuestion(question_id);
                if (publicQuestion == null)
                {
                    throw new Exception("Errors in Adding Question");
                }
                if(publicQuestion.UserQuestions.UserId == user_id)
                {
                    throw new Exception("Already Exist in User Question Bag");
                }
                var userQuestion = new UserQuestions()
                {
                    UserId = user_id,
                    Content = publicQuestion.UserQuestions.Content,
                    IsPublic = IsPublic.NotMine,
                };
                await _userQuestionRepository.AddNewUserQuestionAsync(userQuestion);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserQuestions>> GetAllUserQuestionAsync(string userId, int page, int num_of_ele)
        {
            try
            {
                var result = await _userQuestionRepository.GetAllUserQuestionAsync(userId, page, num_of_ele);
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
