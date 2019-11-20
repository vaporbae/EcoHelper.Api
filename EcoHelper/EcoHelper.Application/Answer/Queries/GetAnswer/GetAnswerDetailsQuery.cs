﻿namespace EcoHelper.Application.Answer.Queries.GetAnswerDetails
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Answer.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class GetAnswerDetailsQuery : IRequest<GetAnswerDetailResponse>
    {
        public IdRequest Data { get; set; }

        public GetAnswerDetailsQuery(IdRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<GetAnswerDetailsQuery, GetAnswerDetailResponse>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<GetAnswerDetailResponse> Handle(GetAnswerDetailsQuery request, CancellationToken cancellationToken)
            {
                IdRequest data = request.Data;

                var entity = await _uow.AnswersRepository.GetByIdAsync(data.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(EcoHelper.Domain.Entities.Answer), data.Id);
                }

                return GetAnswerDetailResponse.Create(entity);
            }
        }
    }
}
