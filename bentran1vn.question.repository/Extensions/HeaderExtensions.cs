using bentran1vn.question.repository.Datas.Entities;
using Microsoft.EntityFrameworkCore;

namespace bentran1vn.question.src.Extensions
{
    public class HeaderExtensions
    {
        public static string GetUserIdFromTokenHeader(HttpContext context)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                var accessToken = authorizationHeader.Substring("Bearer ".Length);

                var tokenPayLoad = JwtExtensions.DecodeJwt(accessToken);

                return tokenPayLoad["nameid"];
            }
            return String.Empty;
        }
    }
}
