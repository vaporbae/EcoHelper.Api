namespace EcoHelper.Domain.Entities
{
    using System.Collections.Generic;
    using EcoHelper.Domain.Entities.Base;

    public class Question : BaseEntity<int>
    {
        public string QuestionText { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
