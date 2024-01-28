using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Extensions;
using bentran1vn.question.src.Repositories.RefreshToken;
using bentran1vn.question.src.Repositories.User;
using bentran1vn.question.src.Requests.UserRequests;
using bentran1vn.question.src.Respones.Account;
using Microsoft.AspNetCore.Identity;

namespace bentran1vn.question.src.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        public UserServices(UserManager<Users> userManager,
            SignInManager<Users> signInManager, IConfiguration configuration,
            IAccountRepository accountRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _accountRepository = accountRepository;
            _refreshTokenRepository = refreshTokenRepository;
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
                var refreshTokens = await _refreshTokenRepository.GetRefreshTokens(userTask.Result.Id);
                if (signInTask.Result.Succeeded)
                {
                    if (refreshTokens == null || !refreshTokens.Any())
                    {
                        var accessToken = JwtExtensions.CreateAccessToken(userTask.Result);
                        var refreshToken = JwtExtensions.CreateRefreshToken(userTask.Result);
                        await _refreshTokenRepository.AddingRefreshToken(refreshToken);
                        var respone = new SignInRespones()
                        {
                            AccessToken = accessToken,
                            RefreshToken = refreshToken.Token
                        };
                        return respone;
                    }
                }
                throw new Exception("Login Fail !");
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
