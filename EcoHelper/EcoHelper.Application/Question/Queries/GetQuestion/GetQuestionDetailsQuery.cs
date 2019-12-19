namespace EcoHelper.Application.Question.Queries.GetQuestionDetails
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Question.Queries;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class GetQuestionDetailsQuery : IRequest<GetQuestionDetailResponse>
    {
        public IdRequest Data { get; set; }

        public GetQuestionDetailsQuery(IdRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<GetQuestionDetailsQuery, GetQuestionDetailResponse>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<GetQuestionDetailResponse> Handle(GetQuestionDetailsQuery request, CancellationToken cancellationToken)
            {
                IdRequest data = request.Data;

                var entity = await _uow.QuestionsRepository.GetFirstAsync(x=>x.Id==data.Id,null,"Answers");

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.Question), data.Id);
                }

                return _mapper.Map<GetQuestionDetailResponse>(entity);
            }
        }
    }
}
