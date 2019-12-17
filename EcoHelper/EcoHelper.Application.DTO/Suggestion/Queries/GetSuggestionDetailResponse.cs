namespace EcoHelper.Application.DTO.Suggestion.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using EcoHelper.Application.DTO.Answer.Commands;

    public class GetSuggestionDetailResponse
    {
        public int Id { get; set; }
        public string Dumpster { get; set; }
        public string Garbage { get; set; }

        public static Expression<Func<Domain.Entities.Suggestion, GetSuggestionDetailResponse>> Projection
        {
            get
            {
                return Suggestion => new GetSuggestionDetailResponse
                {
                    Id = Suggestion.Id,
                    Dumpster = Suggestion.Dumpster,
                    Garbage = Suggestion.Garbage
                };
            }
        }

        public static GetSuggestionDetailResponse Create(Domain.Entities.Suggestion Suggestion)
        {
            return Projection.Compile().Invoke(Suggestion);
        }
    }
}
