namespace EcoHelper.Domain.Entities
{
    using EcoHelper.Domain.Entities.Base;
    using System.Collections.Generic;

    public class Question : BaseEntity<int>
    {
        public string QuestionText { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
