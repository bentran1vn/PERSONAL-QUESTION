using bentran1vn.question.src.Requests.Account;
using bentran1vn.question.src.Respones.Account;

namespace bentran1vn.question.src.Services.RefreshToken
{
    public interface IRefreshTokenServices
    {
        public Task<SignInRespones> refreshAccessTokenAsync(RefreshTokenModel model);
    }
}
