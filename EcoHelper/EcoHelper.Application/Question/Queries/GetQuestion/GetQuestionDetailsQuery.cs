﻿namespace EcoHelper.Application.Question.Queries.GetQuestionDetails
{
    using System.Threading;
    using System.Threading.Tasks;
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

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<GetQuestionDetailResponse> Handle(GetQuestionDetailsQuery request, CancellationToken cancellationToken)
            {
                IdRequest data = request.Data;

                var entity = await _uow.QuestionsRepository.GetByIdAsync(data.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(EcoHelper.Domain.Entities.Question), data.Id);
                }

                return GetQuestionDetailResponse.Create(entity);
            }
        }
    }
}
