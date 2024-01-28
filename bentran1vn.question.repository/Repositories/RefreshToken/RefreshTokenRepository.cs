using bentran1vn.question.repository.Database;
using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Datas.Entities;
using Microsoft.EntityFrameworkCore;

namespace bentran1vn.question.src.Repositories.RefreshToken
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public RefreshTokenRepository() {}

        public async Task AddingRefreshToken(RefreshTokens refreshToken)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await context.Set<RefreshTokens>().AddAsync(refreshToken);
                    await context.SaveChangesAsync();
                    if(result == null)
                    {
                        throw new Exception("Can not adding new refresh token !");
                    }
                    //ValueTask || Task
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error adding refresh token of User have id {refreshToken.UserId}: {ex.Message}");
                }
            }
        }

        public async Task<IEnumerable<RefreshTokens>> GetRefreshTokens(string userId)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var refreshToken = await context.Set<RefreshTokens>().Where(refreshToken => refreshToken.UserId == userId).ToListAsync();
                    return refreshToken;
                    throw new Exception("Can not find refresh tokens !");
                    
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving refresh tokens of User have id {userId}: {ex.Message}");
                }
            }
        }

        public Task<bool> RefreshRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
