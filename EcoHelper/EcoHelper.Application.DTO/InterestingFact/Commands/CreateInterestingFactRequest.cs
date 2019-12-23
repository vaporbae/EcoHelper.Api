namespace EcoHelper.Application.DTO.InterestingFact.Commands
{
    public class CreateInterestingFactRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? DumpsterId { get; set; }
    }
}
