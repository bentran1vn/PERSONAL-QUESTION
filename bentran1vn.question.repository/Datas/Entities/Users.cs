using bentran1vn.question.src.Datas.Entities;
using bentran1vn.question.src.Datas.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bentran1vn.question.repository.Datas.Entities
{
    public class Users: IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public LiveState State { get; set; }
        public ICollection<RefreshTokens> RefreshTokens { get; set; }
        public ICollection<UserQuestions> UserQuestions { get; set; }

        public Users() 
        {
            RefreshTokens = new List<RefreshTokens>() { };
            UserQuestions = new List<UserQuestions>() { };
            State = LiveState.Active;
        }
    }
}
