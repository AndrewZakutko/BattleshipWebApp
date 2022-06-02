using Application.Core;
using MediatR;
using Persistence;

namespace Application.Handlers.ShipHandlers
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var ship = await _context.Ships.FindAsync(request.Id);
                if(ship == null) return null;

                _context.Remove(ship);
                var result = await _context.SaveChangesAsync() > 0;
                if (result) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Failed to delete ship");
            }
        }
    }
}