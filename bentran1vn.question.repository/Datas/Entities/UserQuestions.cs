using bentran1vn.question.src.Datas.Enums;

namespace bentran1vn.question.repository.Datas.Entities
{
    public class UserQuestions
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public LiveState State { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }
        public PublicQuestions PublicQuestion { get; set; }
        public ICollection<QuestionAnswers> QuestionAnswers { get; set; }
        public UserQuestions() 
        {
            QuestionAnswers = new List<QuestionAnswers>() { };
            State = LiveState.Active;
        }
    }
}
