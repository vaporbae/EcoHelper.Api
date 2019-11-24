namespace EcoHelper.Application.BaseVersion.Queries.GetAnswerDetails
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Answer.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class GetBaseVersionDetailsQuery : IRequest<GetBaseVersionDetailResponse>
    {
        public IdRequest Data { get; set; }

        public GetBaseVersionDetailsQuery(IdRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<GetBaseVersionDetailsQuery, GetBaseVersionDetailResponse>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<GetBaseVersionDetailResponse> Handle(GetBaseVersionDetailsQuery request, CancellationToken cancellationToken)
            {
                IdRequest data = request.Data;

                var entity = await _uow.BaseVersionsRepository.GetByIdAsync(data.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(EcoHelper.Domain.Entities.BaseVersion), data.Id);
                }

                return GetBaseVersionDetailResponse.Create(entity);
            }
        }
    }
}
