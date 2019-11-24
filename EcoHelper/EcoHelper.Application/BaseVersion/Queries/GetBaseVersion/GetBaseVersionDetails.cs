namespace EcoHelper.Application.BaseVersion.Queries.GetAnswerDetails
{
    using System.Linq;
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

                var entities = await _uow.BaseVersionsRepository.GetAllAsync();
                var entities2 = entities.ToList();
                Domain.Entities.BaseVersion entity = null;
                if (entities.Count() > 0)
                {
                    entity = entities2[entities.Count() - 1];
                }

                if (entity == null)
                {
                    throw new NotFoundException(nameof(EcoHelper.Domain.Entities.BaseVersion), data.Id);
                }

                return GetBaseVersionDetailResponse.Create(entity);
            }
        }
    }
}
