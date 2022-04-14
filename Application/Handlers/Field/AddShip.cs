using MediatR;
using Persistence;

namespace Application.Handlers.Field
{
    public class AddShip
    {
        public class Command : IRequest<Unit>
        {
            public Guid FieldId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}