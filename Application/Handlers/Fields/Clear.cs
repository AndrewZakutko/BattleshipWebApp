using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.Fields
{
    public class Clear
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
                var listCellShips = await _context.CellShips.Where(x => x.Field.Id == request.Id).ToListAsync();

                _context.CellShips.RemoveRange(listCellShips);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Problem to delete cellShips");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
