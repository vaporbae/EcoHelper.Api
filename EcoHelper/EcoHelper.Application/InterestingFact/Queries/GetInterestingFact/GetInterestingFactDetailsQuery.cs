namespace EcoHelper.Application.InterestingFact.Queries.GetInterestingFactDetails
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.InterestingFact.Queries;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class GetInterestingFactDetailsQuery : IRequest<GetInterestingFactDetailResponse>
    {
        public IdRequest Data { get; set; }

        public GetInterestingFactDetailsQuery(IdRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<GetInterestingFactDetailsQuery, GetInterestingFactDetailResponse>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<GetInterestingFactDetailResponse> Handle(GetInterestingFactDetailsQuery request, CancellationToken cancellationToken)
            {
                IdRequest data = request.Data;

                var entity = await _uow.InterestingFactsRepository.GetByIdAsync(data.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(EcoHelper.Domain.Entities.InterestingFact), data.Id);
                }

                return GetInterestingFactDetailResponse.Create(entity);
            }
        }
    }
}
