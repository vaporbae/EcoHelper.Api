namespace EcoHelper.Application.Answer.Queries.GetAnswers
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Answer.Queries;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using static EcoHelper.Application.DTO.Answer.Queries.GetAnswerListResponse;

    public class GetAnswersQuery : IRequest<GetAnswerListResponse>
    {
        public class Handler : IRequestHandler<GetAnswersQuery, GetAnswerListResponse>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<GetAnswerListResponse> Handle(GetAnswersQuery request, CancellationToken cancellationToken)
            {
                return new GetAnswerListResponse
                {
                    Answers = await _uow.AnswersRepository.ProjectTo<AnswerLookupModel>(_mapper, cancellationToken)
                };
            }
        }
    }
}
