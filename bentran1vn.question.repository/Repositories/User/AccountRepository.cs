using bentran1vn.question.repository.Database;
using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Extensions;
using bentran1vn.question.src.Requests.UserRequests;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bentran1vn.question.src.Repositories.User
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        public AccountRepository(UserManager<Users> userManager,
            SignInManager<Users> signInManager, IConfiguration configuration) 
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> SignInAsync(SignInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync
                (model.Email, model.Password, false, true);
            if(result.Succeeded)
            {
                return string.Empty;
            }

            return JwtExtensions.CreateAccessToken(model)
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
