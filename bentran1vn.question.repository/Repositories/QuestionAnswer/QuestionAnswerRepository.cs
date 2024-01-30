using bentran1vn.question.repository.Database;
using bentran1vn.question.repository.Datas.Entities;
using Microsoft.EntityFrameworkCore;

namespace bentran1vn.question.src.Repositories.QuestionAnswer
{
    public class QuestionAnswerRepository : IQuestionAnswerRepository
    {
        public async Task AddAnswerAsync(QuestionAnswers answer)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    await context.Set<QuestionAnswers>().AddAsync(answer);
                    await context.SaveChangesAsync();
                } catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            };
        }

        public async Task<IEnumerable<QuestionAnswers>> GetAllAnswerForQuestionAsync(int questionId)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await context.Set<QuestionAnswers>()
                        .Where(ans => ans.UserQuestionId == questionId)
                        .ToListAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            };
        }

        public async Task<QuestionAnswers> GetAnswerAsync(int answerId)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await context.Set<QuestionAnswers>().FirstOrDefaultAsync(x => x.Id == answerId);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            };
        }

        public async Task RemoveAnswerAsync(QuestionAnswers answer)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    context.Set<QuestionAnswers>().Remove(answer);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            };
        }

        public async Task UpdateAnswersAsync(QuestionAnswers answer)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    context.Set<QuestionAnswers>().Update(answer);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            };
        }
    }
}
