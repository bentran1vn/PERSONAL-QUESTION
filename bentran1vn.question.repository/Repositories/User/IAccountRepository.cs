using bentran1vn.question.src.Requests.UserRequests;
using Microsoft.AspNetCore.Identity;

namespace bentran1vn.question.src.Repositories.User
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);

        public Task<string> SignInAsync(SignInModel model);
    }
}
