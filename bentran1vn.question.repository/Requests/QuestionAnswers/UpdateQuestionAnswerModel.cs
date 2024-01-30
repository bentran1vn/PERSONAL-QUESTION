using bentran1vn.question.src.Datas.Enums;
using System.ComponentModel.DataAnnotations;

namespace bentran1vn.question.src.Requests.QuestionAnswers
{
    public class UpdateQuestionAnswerModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        public AnswerType Type { get; set; }
    }
}
