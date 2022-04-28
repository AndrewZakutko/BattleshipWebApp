using Application.Core;
using Application.Entities;
using Application.Enums;
using Application.Models;
using Domain;
using MediatR;
using Persistence;

namespace Application.Handlers.ShipHandlers
{
    public class Add
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AddShip Ship { get; set; }
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
                    StartPositionX = request.Ship.StartPositionX,
                    StartPositionY = request.Ship.StartPositionY,
                    ShipDirection = request.Ship.Direction,
                    ShipRank = request.Ship.Rank
                };

                await _context.Ships.AddAsync(ship);
                await _context.SaveChangesAsync();
                
                var field = await _context.Fields.FindAsync(request.Ship.FieldId);
                var cells = new List<CellDb>();

                var n = default(int);

                switch(request.Ship.Rank)
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

                if(request.Ship.Direction == "Horizontal")
                {
                    for(int i = 0; i < n; i++) {
                        var newShip = new CellDb() {
                            ShipDbId = ship.Id.ToString(),
                            X = request.Ship.StartPositionX + n,
                            Y = request.Ship.StartPositionY,
                            CellStatus = CellStatus.Busy.ToString(),
                        };
                        cells.Add(newShip);
                        field.CellShips.Add(new CellShipDb() {
                            CellDbId = newShip.Id.ToString(),
                        });
                    }
                }
                else
                {
                   for(int i = 0; i < n; i++) {
                        var newShip = new CellDb() {
                            ShipDbId = ship.Id.ToString(),
                            X = request.Ship.StartPositionX,
                            Y = request.Ship.StartPositionY + n,
                            CellStatus = CellStatus.Busy.ToString(),
                        };
                        cells.Add(newShip);
                        field.CellShips.Add(new CellShipDb() {
                            CellDbId = newShip.Id.ToString(),
                        });
                    } 
                }

                await _context.Cells.AddRangeAsync(cells);
                var result = await _context.SaveChangesAsync() > 0;

                if(result) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Error add ship");
            }
        }
    }
}