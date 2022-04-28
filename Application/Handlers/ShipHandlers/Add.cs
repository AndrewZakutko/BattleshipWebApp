using Application.Core;
using Application.Entities;
using Application.Enums;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.ShipHandlers
{
    public class Add
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AddShip AddShip { get; set; }
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
                var ship = new ShipDb() {
                    Id = new Guid(),
                    StartPositionX = request.AddShip.StartPositionX,
                    StartPositionY = request.AddShip.StartPositionY,
                    ShipDirection = request.AddShip.Direction,
                    ShipRank = request.AddShip.Rank
                };

                await _context.Ships.AddAsync(ship);
                
                var field = await _context.Fields.FindAsync(request.AddShip.FieldId);

                if(field == null) return null;

                var n = default(int);

                switch(request.AddShip.Rank)
                {
                    case "One":
                        n = 1;
                        break;
                    case "Two":
                        n = 2;
                        break;
                    case "Three":
                        n = 3;
                        break;
                    case "Four":
                        n = 4;
                        break;
                }

                if(request.AddShip.Direction == "Horizontal")
                {
                    for(int i = 0; i < n; i++) {
                        var newShipCell = new CellDb() {
                            Id = new Guid(),
                            X = request.AddShip.StartPositionX + i,
                            Y = request.AddShip.StartPositionY,
                            CellStatus = CellStatus.Busy.ToString(),
                        };
                        await _context.Cells.AddAsync(newShipCell);
                        await _context.CellShips.AddAsync(new CellShipDb() {
                            Cell = newShipCell,
                            Ship = ship,
                            Field = field
                        });
                    }
                }
                else
                {
                   for(int i = 0; i < n; i++) {
                        var newShipCell = new CellDb() {
                            Id = new Guid(),
                            X = request.AddShip.StartPositionX,
                            Y = request.AddShip.StartPositionY + i,
                            CellStatus = CellStatus.Busy.ToString(),
                        };
                        await _context.Cells.AddAsync(newShipCell);
                        await _context.CellShips.AddAsync(new CellShipDb() {
                            Cell = newShipCell,
                            Ship = ship,
                            Field = field
                        });
                    } 
                }
                var result = await _context.SaveChangesAsync() > 0;

                if(result) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Error add ship");
            }
        }
    }
}