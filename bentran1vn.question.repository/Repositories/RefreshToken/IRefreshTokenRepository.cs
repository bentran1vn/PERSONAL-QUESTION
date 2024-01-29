using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Datas.Entities;

namespace bentran1vn.question.src.Repositories.RefreshToken
{
    public interface IRefreshTokenRepository
    {
        public Task AddingRefreshTokenAsync(RefreshTokens refreshToken);
        public Task<RefreshTokens> GetRefreshTokenAsync(string token);
        public Task<IEnumerable<RefreshTokens>> GetRefreshTokensAsync(string userId);
        public Task RemovingRefreshTokenAsync(RefreshTokens refreshToken);
    }
}
