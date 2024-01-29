using bentran1vn.question.repository.Datas.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace bentran1vn.question.src.Datas.Entities
{
    public class RefreshTokens
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime IssuedAt { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }
    }
}
