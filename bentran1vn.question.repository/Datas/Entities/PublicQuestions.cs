using bentran1vn.question.src.Datas.Enums;

namespace bentran1vn.question.repository.Datas.Entities
{
    public class PublicQuestions
    {
        public int Id { get; set; }
        public int UserQuestionId { get; set; }
        public UserQuestions UserQuestions { get; set; }
        public LiveState State { get; set; }
        public PublicQuestions()
        {
            State = LiveState.Active;
        }
    }
}
