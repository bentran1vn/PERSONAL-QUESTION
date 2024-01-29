using bentran1vn.question.src.Extensions;
using bentran1vn.question.src.Repositories.RefreshToken;
using bentran1vn.question.src.Repositories.User;
using bentran1vn.question.src.Requests.Account;
using bentran1vn.question.src.Respones.Account;

namespace bentran1vn.question.src.Services.RefreshToken
{
    public class RefreshTokenServices : IRefreshTokenServices
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAccountRepository _accountTokenRepository;
        public RefreshTokenServices(IRefreshTokenRepository refreshTokenRepository, IAccountRepository accountTokenRepository) 
        {
            _refreshTokenRepository = refreshTokenRepository;
            _accountTokenRepository = accountTokenRepository;
        }
        public async Task<SignInRespones> refreshAccessTokenAsync(RefreshTokenModel model)
        {
            try
            {
                var token = await _refreshTokenRepository.GetRefreshTokenAsync(model.refreshToken);
                if (token == null)
                {
                    throw new Exception("Can not find refresh token !");
                }
                else
                {
                    if (token.IssuedAt > DateTime.UtcNow || DateTime.UtcNow > token.Expires)
                    {
                        throw new Exception("Invalid refresh token !");
                    }
                    await _refreshTokenRepository.RemovingRefreshTokenAsync(token);
                    var result = JwtExtensions.CreateRefreshAndAccessToken(token.UserId, token.Expires, token.IssuedAt);
                    await _refreshTokenRepository.AddingRefreshTokenAsync(result.RefreshToken);
                    return new SignInRespones()
                    {
                        AccessToken = result.AccessToken,
                        RefreshToken = result.RefreshToken.Token
                    };
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //get refresh Token
            //CheckVar valid || iat nhỏ hơn
            //Đúng
            //Xóa Token cũ trong DB
            //Thêm 1 Token mới có exp, iat y chang
            //Kí 2 thằng mới
            //Var
            //Không có quyền nha cu
        }
    }
}
