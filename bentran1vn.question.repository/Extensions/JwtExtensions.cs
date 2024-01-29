using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Datas.Entities;
using bentran1vn.question.src.Requests.UserRequests;
using bentran1vn.question.src.Respones.Account;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace bentran1vn.question.src.Extensions
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public RefreshTokens RefreshToken { get; set; }
    }

    public class ClaimModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
    public class JwtExtensions
    {
        public static TokenModel CreateRefreshAndAccessToken(string userId, DateTime exp, DateTime iat)
        {
            var issuedAt = iat;
            var expireDay = exp;

            if ((!exp.Equals(default(DateTime)) && exp != DateTime.MinValue) && (!iat.Equals(default(DateTime)) && iat != DateTime.MinValue))
            {
                issuedAt = iat;
                expireDay = exp;
            }
            else
            {
                issuedAt = DateTime.UtcNow;
                expireDay = DateTime.UtcNow.AddDays(10);
            }
            var accessToken = JwtExtensions.CreateAccessToken(userId);
            var refreshToken = JwtExtensions.CreateRefreshToken(userId, expireDay, issuedAt);

            var result = new TokenModel()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return result;
        }
        public static RefreshTokens CreateRefreshToken(string userId, DateTime exp, DateTime iat)
        {
            var token = CreateRandomToken();

            return new RefreshTokens
            {
                UserId = userId,
                Expires = exp,
                IssuedAt = iat,
                IsActive = true,
                Token = token,
            };
        }
        public static string CreateAccessToken(string userId)  
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
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var secret = configuration.GetValue<string>("JWT:Secret");
            var key = Encoding.UTF8.GetBytes(secret);
            var authenKey = new SymmetricSecurityKey(key);
            var credential = new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256);

            //Creating Token Payload
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });
            
            //Creating Token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = configuration.GetValue<string>("JWT:ValidAudience"),
                Issuer = configuration.GetValue<string>("JWT:ValidIssuer"),
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credential,
            };

            var token = tokenHadler.CreateToken(tokenDescriptor);

            return tokenHadler.WriteToken(token);
        }
        public static string CreateRandomToken()
        {
            var random = new Random();
            var randomBytes = new byte[64];
            random.NextBytes(randomBytes);
            var token = Convert.ToBase64String(randomBytes);
            return token;
        }
        public static Dictionary<string, string> DecodeJwt(string jwt)
        {
            var tokenHadler = new JwtSecurityTokenHandler();

            var jsonToken = tokenHadler.ReadToken(jwt) as JwtSecurityToken;

            var claims = jsonToken?.Claims;

            Dictionary<string, string> claimMap = new Dictionary<string, string>();

            foreach (var item in claims)
            {
                claimMap.Add(item.Type, item.Value);
            }

            if (claimMap.Count == 0 || claimMap == null) 
            {
                throw new Exception("Claim Empty !");
            }
            return claimMap;
        }
    }
}
