namespace EcoHelper.Domain.Entities
{
    using EcoHelper.Domain.Entities.Base;

    public class Answer : BaseEntity<int>
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
