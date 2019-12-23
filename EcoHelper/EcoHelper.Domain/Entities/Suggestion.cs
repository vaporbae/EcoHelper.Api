namespace EcoHelper.Domain.Entities
{
    using EcoHelper.Domain.Entities.Base;

    public class Suggestion : BaseEntity<int>
    {
        public string Dumpster { get; set; }
        public string Garbage { get; set; }

    }
}
