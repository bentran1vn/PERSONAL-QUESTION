using bentran1vn.question.src.Requests.Account;
using bentran1vn.question.src.Requests.UserRequests;
using bentran1vn.question.src.Respones.Account;
using Microsoft.AspNetCore.Identity;

namespace bentran1vn.question.src.Services.User
{
    public interface IUserServices
    {
        public Task<SignInRespones> SignUpAsync(SignUpModel model);

        public Task<SignInRespones> SignInAsync(SignInModel model);
        public Task SignOutAsync(RefreshTokenModel model);
    }
}
