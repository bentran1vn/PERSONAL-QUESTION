using bentran1vn.question.src.Datas.Entities;
using Microsoft.AspNetCore.Identity;

namespace bentran1vn.question.repository.Datas.Entities
{
    public class Users: IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public ICollection<RefreshTokens> RefreshTokens { get; set; }

        public Users() 
        {
            RefreshTokens = new List<RefreshTokens>() { };
        }
    }
}
