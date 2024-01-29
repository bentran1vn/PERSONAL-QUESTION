namespace bentran1vn.question.repository.Datas.Entities
{
    public class UserQuestions
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }

        public ICollection<QuestionAnswers> QuestionAnswers { get; set; }

        public UserQuestions() 
        {
            QuestionAnswers = new List<QuestionAnswers>() { };
        }
    }
}
