namespace EcoHelper.Application.DTO.InterestingFact.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using EcoHelper.Application.DTO.InterestingFact.Commands;

    public class GetInterestingFactDetailResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? DumpsterId { get; set; }

        public static Expression<Func<Domain.Entities.InterestingFact, GetInterestingFactDetailResponse>> Projection
        {
            get
            {
                return InterestingFact => new GetInterestingFactDetailResponse
                {
                    Id = InterestingFact.Id,
                    Title = InterestingFact.Title,
                    Description = InterestingFact.Description,
                    DumpsterId = InterestingFact.DumpsterId
                };
            }
        }

        public static GetInterestingFactDetailResponse Create(Domain.Entities.InterestingFact InterestingFact)
        {
            return Projection.Compile().Invoke(InterestingFact);
        }
    }
}
