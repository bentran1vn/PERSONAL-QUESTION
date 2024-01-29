using System.ComponentModel.DataAnnotations;

namespace bentran1vn.question.src.Requests.Account
{
    public class RefreshTokenModel
    {
        [Required]
        public string refreshToken { get; set; } = null!;
    }
}
