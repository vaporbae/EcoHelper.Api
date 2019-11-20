namespace EcoHelper.Application.User.Queries.GetUsers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.User;
    using EcoHelper.Application.DTO.User.Queries;

    public class GetUsersQuery : IRequest<GetUsersListResponse>
    {
        public class Handler : IRequestHandler<GetUsersQuery, GetUsersListResponse>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<GetUsersListResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                return new GetUsersListResponse
                {
                    Users = await _uow.UsersRepository.ProjectTo<UserLookupModel>(_mapper, cancellationToken)
                };
            }
        }
    }
}

