namespace EcoHelper.Application.DTO.Dumpster.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using EcoHelper.Application.DTO.Garbage.Commands;
    using EcoHelper.Application.DTO.Garbage.Queries;

    public class GetDumpsterDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Domain.Entities.Garbage> Garbages { get; set; }
        public IList<Domain.Entities.InterestingFact> InterestingFacts { get; set; }

        public static Expression<Func<Domain.Entities.Dumpster, GetDumpsterDetailResponse>> Projection
        {
            get
            {
                return Dumpster => new GetDumpsterDetailResponse
                {
                    Id = Dumpster.Id,
                    Name = Dumpster.Name,
                    Garbages = Dumpster.Garbages.ToList(),
                    InterestingFacts = Dumpster.InterestingFacts.ToList()
                };
            }
        }

        public static GetDumpsterDetailResponse Create(Domain.Entities.Dumpster Dumpster)
        {
            return Projection.Compile().Invoke(Dumpster);
        }
    }
}
