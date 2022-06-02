using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.FieldHandlers
{
    public class CheckCountOfShipCells
    {
        public class Command : IRequest<Result<int>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                var listShips = await _context.CellShips.Where(x => x.Field.Id == request.Id).Select(x => x.Ship).ToListAsync();

                var list = new List<ShipDb>();
                
                foreach (var s in listShips)
                {
                    if (list.Count == 0)
                    {
                        list.Add(s);
                    }
                    if (!list.Where(x => x.Id == s.Id).Any())
                    {
                        list.Add(s);
                    }
                }

                return Result<int>.Success(list.Count);
            }
        }
    }
}
