namespace EcoHelper.Domain.Entities
{
    using EcoHelper.Domain.Entities.Base;

    public class InterestingFact : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? DumpsterId { get; set; }
        
        public virtual Dumpster Dumpster { get; set; }
    }
}
