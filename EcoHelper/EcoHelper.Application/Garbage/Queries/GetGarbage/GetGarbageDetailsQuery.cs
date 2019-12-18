namespace EcoHelper.Application.Garbage.Queries.GetGarbageDetails
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Garbage.Queries;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class GetGarbageDetailsQuery : IRequest<GetGarbageDetailResponse>
    {
        public IdRequest Data { get; set; }

        public GetGarbageDetailsQuery(IdRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<GetGarbageDetailsQuery, GetGarbageDetailResponse>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<GetGarbageDetailResponse> Handle(GetGarbageDetailsQuery request, CancellationToken cancellationToken)
            {
                IdRequest data = request.Data;

                var entity = await _uow.GarbagesRepository.GetByIdAsync(data.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.Garbage), data.Id);
                }

                return _mapper.Map<GetGarbageDetailResponse>(entity);
            }
        }
    }
}
