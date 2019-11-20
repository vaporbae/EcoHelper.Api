namespace EcoHelper.Domain.Entities
{
    using EcoHelper.Domain.Entities.Base;

    public class Garbage : BaseEntity<int>
    {
        public string Name { get; set; }
        public int DumpsterId { get; set; }

        public virtual Dumpster Dumpster { get; set; }
    }
}
