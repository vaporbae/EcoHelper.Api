namespace EcoHelper.Application.Dumpster.Commands.CreateDumpster
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Dumpster.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using FluentValidation;
    using MediatR;

    public class CreateDumpsterCommand : IRequest
    {
        public CreateDumpsterRequest Data { get; set; }

        public CreateDumpsterCommand(CreateDumpsterRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<CreateDumpsterCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(CreateDumpsterCommand request, CancellationToken cancellationToken)
            {
                CreateDumpsterRequest data = request.Data;

                var vResult = await new CreateDumpsterCommandValidator(_uow).ValidateAsync(data, cancellationToken);

                if (!vResult.IsValid)
                {
                    throw new ValidationException(vResult.Errors);
                }


                var entityDumpster = new EcoHelper.Domain.Entities.Dumpster
                {
                     Name = data.Name,
                     InterestingFacts = new List<Domain.Entities.InterestingFact>(),
                     Garbages = new List<Domain.Entities.Garbage>()
                };

                _uow.DumpstersRepository.Add(entityDumpster);

                await _uow.SaveChangesAsync(cancellationToken);

                return await Unit.Task;
            }
        }
    }
}
