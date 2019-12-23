namespace EcoHelper.Application.User.Queries.GetUserDetails
{
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.User.Queries;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetUserDetailsQuery : IRequest<GetUserDetailResponse>
    {
        public IdRequest Data { get; set; }

        public GetUserDetailsQuery(IdRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<GetUserDetailsQuery, GetUserDetailResponse>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<GetUserDetailResponse> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
            {
                IdRequest data = request.Data;

                var entity = await _uow.UsersRepository.GetByIdAsync(data.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.User), data.Id);
                }

                return GetUserDetailResponse.Create(entity);
            }
        }
    }
}
