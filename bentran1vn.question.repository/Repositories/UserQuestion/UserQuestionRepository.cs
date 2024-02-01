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

        public async Task AddNewUserQuestionAsync(UserQuestions user_question)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await context.Set<UserQuestions>().AddAsync(user_question);
                    if (result == null)
                    {
                        throw new Exception("Fail to Adding New Question");
                    }
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<UserQuestions>> GetAllUserQuestionAsync(string userId, int page, int num_of_ele)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await context.Set<UserQuestions>()
                        .Where(ques => ques.UserId == userId)
                        .Skip(num_of_ele * page).Take(num_of_ele)
                        .ToListAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<UserQuestions> GetUserQuestionContentAsync(string userId, int questionId)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await context.Set<UserQuestions>()
                        .Where(ques => ques.UserId == userId && ques.Id == questionId)
                        .Include(ques => ques.User)
                        .Include(ques => ques.PublicQuestion)
                        .FirstOrDefaultAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task RemoveUserQuestionAsync(UserQuestions questions)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = context.Set<UserQuestions>().Remove(questions);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task UpdateUserQuestionAsync(UserQuestions questions)
        {
            using(var context = new AppDbContext())
            {
                try
                {
                    context.Set<UserQuestions>().Update(questions);
                    await context.SaveChangesAsync();
                } catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
    