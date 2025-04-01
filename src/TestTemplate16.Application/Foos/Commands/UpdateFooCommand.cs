using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestTemplate16.Common.Exceptions;
using TestTemplate16.Common.Interfaces;
using TestTemplate16.Core.Entities;

namespace TestTemplate16.Application.Foos.Commands;

public class UpdateFooCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public byte[] RowVersion { get; set; }

    private class UpdateFooCommandHandler : IRequestHandler<UpdateFooCommand, Unit>
    {
        private readonly IRepository<Foo> _repository;

        public UpdateFooCommandHandler(IRepository<Foo> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateFooCommand request, CancellationToken cancellationToken)
        {
            var foo = await _repository.GetSingleAsync(f => f.Id == request.Id);
            if (foo is null)
            {
                throw new EntityNotFoundException(nameof(Foo), request.Id);
            }
            foo.SetRowVersion(request.RowVersion);
            foo.Text = request.Text;
            return Unit.Value;
        }
    }
}
