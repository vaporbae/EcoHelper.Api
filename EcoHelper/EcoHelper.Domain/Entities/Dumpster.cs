namespace EcoHelper.Domain.Entities
{
    using EcoHelper.Domain.Entities.Base;
    using System.Collections.Generic;

    public class Dumpster : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<Garbage> Garbages { get; set; }
        public ICollection<InterestingFact> InterestingFacts { get; set; }
    }
}
