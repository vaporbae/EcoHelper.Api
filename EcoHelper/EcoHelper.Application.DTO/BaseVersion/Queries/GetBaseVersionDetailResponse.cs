namespace EcoHelper.Application.DTO.Answer.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using EcoHelper.Application.DTO.BaseVersion.Commands;

    public class GetBaseVersionDetailResponse
    {
        public int Id { get; set; }
        public string Ver { get; set; }

        public static Expression<Func<Domain.Entities.BaseVersion, GetBaseVersionDetailResponse>> Projection
        {
            get
            {
                return BaseVersion => new GetBaseVersionDetailResponse
                {
                    Id = BaseVersion.Id,
                    Ver = BaseVersion.Ver.ToString()
                };
            }
        }

        public static GetBaseVersionDetailResponse Create(Domain.Entities.BaseVersion baseVersion)
        {
            return Projection.Compile().Invoke(baseVersion);
        }
    }
}
