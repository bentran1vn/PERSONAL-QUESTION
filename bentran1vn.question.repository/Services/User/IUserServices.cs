using bentran1vn.question.src.Requests.UserRequests;
using bentran1vn.question.src.Respones.Account;
using Microsoft.AspNetCore.Identity;

namespace bentran1vn.question.src.Services.User
{
    public interface IUserServices
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);

        public Task<SignInRespones> SignInAsync(SignInModel model);

        //public Task<ValueType> CreateAccessToken(RefreshTokens token);
    }
}
