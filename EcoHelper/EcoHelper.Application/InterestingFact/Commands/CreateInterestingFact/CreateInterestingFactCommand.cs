namespace EcoHelper.Application.InterestingFact.Commands.CreateInterestingFact
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.InterestingFact.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using FluentValidation;
    using MediatR;

    public class CreateInterestingFactCommand : IRequest
    {
        public CreateInterestingFactRequest Data { get; set; }

        public CreateInterestingFactCommand(CreateInterestingFactRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<CreateInterestingFactCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(CreateInterestingFactCommand request, CancellationToken cancellationToken)
            {
                CreateInterestingFactRequest data = request.Data;

                var vResult = await new CreateInterestingFactCommandValidator(_uow).ValidateAsync(data, cancellationToken);

                if (!vResult.IsValid)
                {
                    throw new ValidationException(vResult.Errors);
                }


                var entityInterestingFact = new EcoHelper.Domain.Entities.InterestingFact
                {
                    Title = data.Title,
                    Description = data.Description,
                    DumpsterId = data.DumpsterId
                };

                _uow.InterestingFactsRepository.Add(entityInterestingFact);

                await _uow.SaveChangesAsync(cancellationToken);

                return await Unit.Task;
            }
        }
    }
}
