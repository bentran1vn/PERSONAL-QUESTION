using bentran1vn.question.src.Datas.Enums;
using System.ComponentModel.DataAnnotations;

namespace bentran1vn.question.src.Requests.QuestionAnswers
{
    public class AddNewQuestionAnswerModel
    {
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        public AnswerType Type { get; set; }
    }
}
