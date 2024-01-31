
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
        public async Task PublicUserQuestion(int questionId, string userId)
        {
            try
            {
                var question = await _userQuestionServices.GetUserQuestionAsync(userId, questionId);
                if (question == null || question.State == LiveState.InActive || question.IsPublic == IsPublic.True) 
                {
                    throw new Exception("Invalid Question !");
                }
                question.IsPublic = IsPublic.True;
                //Task updateQuesTask = _userQuestionRepository.UpdateUserQuestionAsync(question);
                //Task publicQuesTask = _publicQuestionRepository.PublicUserQuestion
                await _userQuestionRepository.UpdateUserQuestionAsync(question);
                await _publicQuestionRepository.PublicUserQuestion(new PublicQuestions()
                {
                    UserQuestionId = questionId,
                }
                );
                //await Task.WhenAll(updateQuesTask, publicQuesTask);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UnPublicUserQuestion(int questionId)
        {
            try
            {
                var question = await _publicQuestionRepository.GetPublicQuestion(questionId);
                question.State = LiveState.InActive;
                var userQuestion = question.UserQuestions;
                userQuestion.IsPublic = IsPublic.False;
                await _userQuestionRepository.UpdateUserQuestionAsync(userQuestion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
