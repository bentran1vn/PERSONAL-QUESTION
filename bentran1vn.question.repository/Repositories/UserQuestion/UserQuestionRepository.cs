using bentran1vn.question.repository.Database;
using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Requests.UserQuestion;
using Microsoft.EntityFrameworkCore;

namespace bentran1vn.question.src.Repositories.UserQuestion
{
    public class UserQuestionRepository : IUserQuestionRepository
    {
        public async Task AddNewUserQuestionAsync(AddNewQuestionModel model, string userId)
        {
            using(var context = new AppDbContext())
            {
                try
                {
                    var question = new UserQuestions()
                    {
                        Content = model.Content,
                        UserId = userId,
                    };
                    var result = await context.Set<UserQuestions>().AddAsync(question);
                    if(result == null)
                    {
                        throw new Exception("Fail to Adding New Question");
                    }
                    await context.SaveChangesAsync();
                } catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<UserQuestions>> GetAllUserQuestionAsync(string userId)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await context.Set<UserQuestions>()
                        .Where(ques => ques.UserId == userId)
                        .ToListAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<UserQuestions> GetUserQuestionContentAsync(string userId, string questionId)
        {
            throw new NotImplementedException();
        }
    }
}
