namespace EcoHelper.Application.DTO.Answer.Commands
{
    public class CreateAnswerRequest
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
