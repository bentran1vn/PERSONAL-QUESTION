using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Extensions;
using bentran1vn.question.src.Repositories.User;
using bentran1vn.question.src.Requests.UserRequests;
using bentran1vn.question.src.Respones.Account;
using Microsoft.AspNetCore.Identity;

namespace bentran1vn.question.src.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        public UserServices(UserManager<Users> userManager,
            SignInManager<Users> signInManager, IConfiguration configuration,
            IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<SignInRespones> SignInAsync(SignInModel model)
        {
            try
            {
                Task<Users> userTask = _accountRepository.GetUsersAsync(model.Email);
                Task<SignInResult> signInTask = _signInManager.PasswordSignInAsync
                    (model.Email, model.Password, false, true);

                await Task.WhenAll(userTask, signInTask);

                if (signInTask.Result.Succeeded)
                {
                    await Console.Out.WriteLineAsync("haha: " + userTask.Result);
                    var respone = new SignInRespones()
                    {
                        AccessToken = JwtExtensions.CreateAccessToken(userTask.Result),
                        RefreshToken = JwtExtensions.CreateRefreshToken(userTask.Result).Token,
                    };
                    return respone;
                }
                throw new Exception("");
            } catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("haha: " + ex.Message);
                throw new Exception("");
            }
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new Users()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            return await _userManager.CreateAsync(user, model.Password);
        }
    }
}
