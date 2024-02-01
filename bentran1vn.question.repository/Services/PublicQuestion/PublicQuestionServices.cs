
using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Datas.Enums;
using bentran1vn.question.src.Repositories.PublicQuestion;
using bentran1vn.question.src.Repositories.UserQuestion;
using bentran1vn.question.src.Services.UserQuestion;

namespace bentran1vn.question.src.Services.PublicQuestion
{
    public class PublicQuestionServices : IPublicQuestionServices
    {
        private readonly IPublicQuestionRepository _publicQuestionRepository;
        private readonly IUserQuestionRepository _userQuestionRepository;
        private readonly IUserQuestionServices _userQuestionServices;

        public PublicQuestionServices(IPublicQuestionRepository publicQuestionRepository,
            IUserQuestionServices userQuestionServices, IUserQuestionRepository userQuestionRepository)
        {
            _publicQuestionRepository = publicQuestionRepository;
            _userQuestionServices = userQuestionServices;
            _userQuestionRepository = userQuestionRepository;
        }

        public async Task<IEnumerable<PublicQuestions>> GetPublicQuestionsAsync(int page, int num_of_question)
        {
            try
            {
                var question = await _publicQuestionRepository.GetAllPublicQuestions(page, num_of_question);
                return question;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task PublicUserQuestionAsync(int questionId, string userId)
        {
            try
            {
                var question = await _userQuestionServices.GetUserQuestionAsync(userId, questionId);
                if (question == null || question.State == LiveState.InActive) 
                {
                    throw new Exception("Invalid Question !");
                }
                if(question.IsPublic == IsPublic.True)
                {
                    throw new Exception("Already Public Question !");
                }
                question.IsPublic = IsPublic.True;
                Task updateQuesTask = _userQuestionRepository.UpdateUserQuestionAsync(question);
                Task publicQuesTask = _publicQuestionRepository.PublicUserQuestion(
                    new PublicQuestions()
                {
                    UserQuestionId = questionId,
                });
                await Task.WhenAll(updateQuesTask, publicQuesTask);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UnPublicUserQuestionAsync(int questionId, string userId)
        {
            try
            {
                var question = await _publicQuestionRepository.GetUserPublicQuestion(questionId, userId);
                question.State = LiveState.InActive;
                Task updatePub =  _publicQuestionRepository.UpdatePublicQuestionAsync(question);
                var userQuestion = question.UserQuestions;
                userQuestion.IsPublic = IsPublic.False;
                Task updateUserPub = _userQuestionRepository.UpdateUserQuestionAsync(userQuestion);
                await Task.WhenAll(updatePub, updateUserPub);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UnPublicUserQuestionAsync(int user_question_Id)
        {
            try
            {
                var question = await _publicQuestionRepository.GetPublicQuestion(user_question_Id);
                question.State = LiveState.InActive;
                Task upPub = _publicQuestionRepository.UpdatePublicQuestionAsync(question);
                var userQuestion = question.UserQuestions;
                userQuestion.IsPublic = IsPublic.False;
                Task upUserPub = _userQuestionRepository.UpdateUserQuestionAsync(userQuestion);
                await Task.WhenAll(upPub, upUserPub);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
