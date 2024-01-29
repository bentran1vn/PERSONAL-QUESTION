namespace bentran1vn.question.repository.Datas.Entities
{
    public class UserQuestion
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }

        public ICollection<QuestionAnswers> QuestionAnswers { get; set; }

        public UserQuestion() 
        {
            QuestionAnswers = new List<QuestionAnswers>() { };
        }
    }
}
