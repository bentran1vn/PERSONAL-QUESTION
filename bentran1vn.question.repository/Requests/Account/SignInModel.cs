using System.ComponentModel.DataAnnotations;

namespace bentran1vn.question.src.Requests.UserRequests
{
    public class SignInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
