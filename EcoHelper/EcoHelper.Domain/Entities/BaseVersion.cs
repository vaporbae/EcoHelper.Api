namespace EcoHelper.Domain.Entities
{
    using EcoHelper.Domain.Entities.Base;

    public class BaseVersion : BaseEntity<int>
    {
        public string Ver { get; set; }
    }
}
