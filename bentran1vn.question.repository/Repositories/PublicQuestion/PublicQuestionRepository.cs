using bentran1vn.question.repository.Database;
using bentran1vn.question.repository.Datas.Entities;

namespace bentran1vn.question.src.Repositories.PublicQuestion
{
    public class PublicQuestionRepository : IPublicQuestionRepository
    {
        public async Task PublicUserQuestion(PublicQuestions question)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    await context.Set<PublicQuestions>().AddAsync(question);
                    context.SaveChangesAsync();
                } catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
