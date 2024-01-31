using bentran1vn.question.repository.Database;
using bentran1vn.question.repository.Datas.Entities;
using Microsoft.EntityFrameworkCore;

namespace bentran1vn.question.src.Repositories.PublicQuestion
{
    public class PublicQuestionRepository : IPublicQuestionRepository
    {
        public async Task<PublicQuestions> GetPublicQuestion(int questionId)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var publicQuestion = await context.Set<PublicQuestions>()
                        .Where(x => x.Id == questionId)
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
        public async Task UpdatePublicQuestion(PublicQuestions question)
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
