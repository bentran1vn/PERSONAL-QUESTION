using bentran1vn.question.src.Datas.Enums;

namespace bentran1vn.question.repository.Datas.Entities
{
    public class QuestionAnswers
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserQuestionId { get; set; }
        public AnswerType AnswerType { get; set; }
        public UserQuestions UserQuestion { get; set; }

        public QuestionAnswers() 
        {
            AnswerType = AnswerType.False;
        }
    }
}
