using Application.Core;
using Application.Enums;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.FieldHandlers
{
    public class CheckCountOfShipCellsAlive
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
                var listShips = await _context.CellShips.Where(x => x.Field.Id == request.Id 
                    && x.Cell.CellStatus != CellStatus.Destroyed.ToString()).Select(x => x.Ship).ToListAsync();

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
