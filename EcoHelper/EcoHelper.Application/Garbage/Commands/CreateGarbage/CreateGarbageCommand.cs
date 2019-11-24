namespace EcoHelper.Application.Garbage.Commands.CreateGarbage
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Garbage.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using FluentValidation;
    using MediatR;
    using System.Linq;

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

                //ORYGINALNA BAZA NAJMANSKA
                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\m.kalisz\Downloads\Baza\bazaPudzianska.txt");

                for(int i=1;i<lines.Length;i++)
                {
                    if (i % 2 == 1)
                    {
                        var dumpsters = await _uow.DumpstersRepository.GetAllAsync();
                        var dumpster = dumpsters.Where(x => x.Name.Equals(lines[i - 1])).First();

                        var entityGarbage = new EcoHelper.Domain.Entities.Garbage
                        {
                            Name = lines[i],
                            DumpsterId = dumpster.Id
                        };

                        _uow.GarbagesRepository.Add(entityGarbage);

                        await _uow.SaveChangesAsync(cancellationToken);
                    }
                }


                

                return await Unit.Task;
            }
        }
    }
}
