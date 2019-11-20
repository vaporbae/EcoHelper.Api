namespace EcoHelper.Application.DTO.Garbage.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using EcoHelper.Application.DTO.Garbage.Commands;

    public class GetGarbageDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DumpsterId { get; set; }

        public static Expression<Func<Domain.Entities.Garbage, GetGarbageDetailResponse>> Projection
        {
            get
            {
                return Garbage => new GetGarbageDetailResponse
                {
                    Id = Garbage.Id,
                    Name = Garbage.Name,
                    DumpsterId = Garbage.DumpsterId
                };
            }
        }

        public static GetGarbageDetailResponse Create(Domain.Entities.Garbage Garbage)
        {
            return Projection.Compile().Invoke(Garbage);
        }
    }
}
