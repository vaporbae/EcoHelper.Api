namespace EcoHelper.Application.BaseVersion.Commands.CreateBaseVersion
{
    using EcoHelper.Application.DTO.BaseVersion.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using FluentValidation;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateBaseVersionCommand : IRequest
    {
        public CreateBaseVersionRequest Data { get; set; }

        public CreateBaseVersionCommand(CreateBaseVersionRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<CreateBaseVersionCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(CreateBaseVersionCommand request, CancellationToken cancellationToken)
            {
                CreateBaseVersionRequest data = request.Data;

                var vResult = await new CreateBaseVersionCommandValidator(_uow).ValidateAsync(data, cancellationToken);

                if (!vResult.IsValid)
                {
                    throw new ValidationException(vResult.Errors);
                }


                var entityBaseVersion = new EcoHelper.Domain.Entities.BaseVersion
                {
                    Ver = data.Ver
                };

                _uow.BaseVersionsRepository.Add(entityBaseVersion);

                await _uow.SaveChangesAsync(cancellationToken);

                return await Unit.Task;
            }
        }
    }
}
