using bentran1vn.question.repository.Database;
using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Datas.Enums;
using Microsoft.EntityFrameworkCore;

namespace bentran1vn.question.src.Repositories.PublicQuestion
{
    public class PublicQuestionRepository : IPublicQuestionRepository
    {
        public async Task<IEnumerable<PublicQuestions>> GetAllPublicQuestions(int page, int num_of_question)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var publicQuestion = await context.Set<PublicQuestions>()
                        .Skip(page* num_of_question).Take(num_of_question).ToListAsync();
                    return publicQuestion;
                } 
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task<PublicQuestions> GetUserPublicQuestion(int questionId, string user_id)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var publicQuestion = await context.Set<PublicQuestions>()
                        .Where(x => x.Id == questionId 
                            && x.UserQuestions.UserId == user_id 
                            && x.State == LiveState.Active)
                        .Include(ques => ques.UserQuestions)
                        .FirstOrDefaultAsync();
                    return publicQuestion;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task<PublicQuestions> GetPublicQuestion(int user_question_Id)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var publicQuestion = await context.Set<PublicQuestions>()
                        .Include(x => x.UserQuestions)
                        .FirstOrDefaultAsync(x => x.UserQuestions.Id == user_question_Id);
                    return publicQuestion;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task PublicUserQuestion(PublicQuestions question)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    await context.Set<PublicQuestions>().AddAsync(question);
                    await context.SaveChangesAsync();
                } catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task UpdatePublicQuestionAsync(PublicQuestions question)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    context.Set<PublicQuestions>().Update(question);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
