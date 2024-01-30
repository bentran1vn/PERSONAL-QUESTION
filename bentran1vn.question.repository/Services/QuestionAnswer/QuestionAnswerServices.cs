
using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Repositories.QuestionAnswer;
using bentran1vn.question.src.Requests.QuestionAnswers;
using bentran1vn.question.src.Respones.QuestionAnswer;

namespace bentran1vn.question.src.Services.QuestionAnswer
{
    public class QuestionAnswerServices : IQuestionAnswerServices
    {
        private readonly IQuestionAnswerRepository _questionAnswerRepository;
        public QuestionAnswerServices(IQuestionAnswerRepository questionAnswerRepository) 
        {
            _questionAnswerRepository = questionAnswerRepository;
        }
        public async Task AddAnswerForQuestionAsync(AddNewQuestionAnswerModel model)
        {
            try
            {
                var answer = new QuestionAnswers()
                {
                    Content = model.Content,
                    UserQuestionId = model.QuestionId,
                    AnswerType = model.Type,
                };
                await _questionAnswerRepository.AddAnswerAsync(answer);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetAnswerRespone> GetAnswerForQuestionAsync(int answerId)
        {
            try
            {
                var result = await _questionAnswerRepository.GetAnswerAsync(answerId);
                if (result == null)
                {
                    throw new Exception("Answers Empty !");
                }
                var handledResult = new GetAnswerRespone()
                {
                    Id = result.Id,
                    Type = result.AnswerType,
                    Content = result.Content
                };
                return handledResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetAnswerRespone>> GetAnswersForQuestionAsync(int questionId)
        {
            try
            {
                var result = await _questionAnswerRepository.GetAllAnswerForQuestionAsync(questionId);
                if(!result.Any())
                {
                    throw new Exception("Answers Empty !");
                }
                var handledResult = result.Select(x => new GetAnswerRespone()
                {
                    Id = x.Id,
                    Type = x.AnswerType,
                    Content = x.Content
                });

                return handledResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveAnswerForQuestionAsync(int answerId)
        {
            try
            {
                var answer = await _questionAnswerRepository.GetAnswerAsync(answerId);
                await _questionAnswerRepository.RemoveAnswerAsync(answer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAnswersForQuestionAsync(UpdateQuestionAnswerModel model)
        {
            try
            {
                var answer = await _questionAnswerRepository.GetAnswerAsync(model.Id);
                answer.AnswerType = model.Type;
                answer.Content = model.Content;
                await _questionAnswerRepository.UpdateAnswersAsync(answer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
