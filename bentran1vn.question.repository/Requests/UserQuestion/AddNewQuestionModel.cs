using bentran1vn.question.repository.Datas.Entities;
using System.ComponentModel.DataAnnotations;

namespace bentran1vn.question.src.Requests.UserQuestion
{
    public class AddNewQuestionModel
    {
        [Required]
        public string Content { get; set; }
    }
}
