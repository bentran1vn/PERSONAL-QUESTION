using bentran1vn.question.src.Requests.UserRequests;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bentran1vn.question.src.Extensions
{
    public class JwtExtensions
    {
        public static string CreateAccessToken(SignInModel model)
        {

            /*
             * //Creating Clam B1
            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Email, model.Email),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //};

            //Creating Token Payload B1
            //var tokenPayLoad = new JwtSecurityToken(
            //    issuer: "",
            //    audience: "",
            //    claims: claims,
            //    expires: DateTime.UtcNow.AddMinutes(15),
            //    signingCredentials: credential
            //);
             */
            //Creating handle JwtToken
            var tokenHadler = new JwtSecurityTokenHandler();

            //Creating Secret
            var configuration = new ConfigurationBuilder()
                .Build();
            var secret = configuration.GetValue<string>("JWT:Secret");
            //var secret = configuration.Ge
            var key = Encoding.UTF8.GetBytes(secret);
            var authenKey = new SymmetricSecurityKey(key);
            var credential = new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256);

            //Creating Token Payload
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            //Creating Token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = configuration.GetValue<string>("JWT:ValidAudience"),
                Issuer = configuration.GetValue<string>("JWT:ValidIssuer"),
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = credential,
            };

            var token = tokenHadler.CreateToken(tokenDescriptor);

            return tokenHadler.WriteToken(token);
        }
    }
}
