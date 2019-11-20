namespace EcoHelper.Application.Question.Queries.GetQuestions
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using EcoHelper.Application.DTO.Question.Queries;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;
    using static EcoHelper.Application.DTO.Question.Queries.GetQuestionListResponse;

    public class GetQuestionsQuery : IRequest<GetQuestionListResponse>
    {
        public class Handler : IRequestHandler<GetQuestionsQuery, GetQuestionListResponse>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<GetQuestionListResponse> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
            {
                return new GetQuestionListResponse
                {
                    Questions = await _uow.QuestionsRepository.ProjectTo<QuestionLookupModel>(_mapper, cancellationToken)
                };
            }
        }
    }
}
