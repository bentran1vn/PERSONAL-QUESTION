using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Datas.Entities;

namespace bentran1vn.question.src.Repositories.RefreshToken
{
    public interface IRefreshTokenRepository
    {
        public Task AddingRefreshToken(RefreshTokens refreshToken);
        public Task<bool> RefreshRefreshToken(string userId);
        public Task<IEnumerable<RefreshTokens>> GetRefreshTokens(string userId);
    }
}
