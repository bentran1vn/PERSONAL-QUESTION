using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Extensions;
using bentran1vn.question.src.Repositories.RefreshToken;
using bentran1vn.question.src.Repositories.User;
using bentran1vn.question.src.Requests.Account;
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
                if (signInTask.Result.Succeeded)
                {
                    var tokenModel = JwtExtensions.CreateRefreshAndAccessToken(userTask.Result, DateTime.MinValue, DateTime.MinValue);
                    await _refreshTokenRepository.AddingRefreshTokenAsync(tokenModel.RefreshToken);
                    var respone = new SignInRespones()
                    {
                        AccessToken = tokenModel.AccessToken,
                        RefreshToken = tokenModel.RefreshToken.Token
                    };
                    return respone;
                }
                throw new Exception("Login Fail !");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SignOutAsync(RefreshTokenModel model)
        {
            try
            {
                var token = await _refreshTokenRepository.GetRefreshTokenAsync(model.refreshToken);
                if (token == null)
                {
                    throw new Exception("Can not find refresh token !");
                }
                await _refreshTokenRepository.RemovingRefreshTokenAsync(token);
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
