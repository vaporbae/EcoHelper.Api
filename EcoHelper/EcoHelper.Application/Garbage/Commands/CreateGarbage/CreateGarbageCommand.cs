namespace EcoHelper.Application.Garbage.Commands.CreateGarbage
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Garbage.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using FluentValidation;
    using MediatR;

    public class CreateGarbageCommand : IRequest
    {
        public CreateGarbageRequest Data { get; set; }

        public CreateGarbageCommand(CreateGarbageRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<CreateGarbageCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(CreateGarbageCommand request, CancellationToken cancellationToken)
            {
                CreateGarbageRequest data = request.Data;

                var vResult = await new CreateGarbageCommandValidator(_uow).ValidateAsync(data, cancellationToken);

                if (!vResult.IsValid)
                {
                    throw new ValidationException(vResult.Errors);
                }


                var entityGarbage = new EcoHelper.Domain.Entities.Garbage
                {
                    Name = data.Name,
                    DumpsterId = data.DumpsterId
                };

                _uow.GarbagesRepository.Add(entityGarbage);

                await _uow.SaveChangesAsync(cancellationToken);

                return await Unit.Task;
            }
        }
    }
}
