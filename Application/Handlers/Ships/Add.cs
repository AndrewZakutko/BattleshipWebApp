using Application.Core;
using Application.Entities;
using Application.EntityHalpers;
using Application.Enums;
using Application.Managers;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.Ships
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
            private readonly IMapper _mapper;
            private readonly GameManager _gameManager;
            public Handler(DataContext context, IMapper mapper, GameManager gameManager)
            {
                _gameManager = gameManager;
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var ship = new ShipDb()
                {
                    StartPositionX = request.AddShip.StartPositionX,
                    StartPositionY = request.AddShip.StartPositionY,
                    Direction = request.AddShip.Direction,
                    Rank = request.AddShip.Rank
                };

                var checkShip = new Ship();
                _mapper.Map(ship, checkShip);

                var field = await _context.Fields.FindAsync(request.AddShip.FieldId);
                if (field == null) return null;

                var listCellShips = await _context.CellShips.Where(x => x.Field.Id == request.AddShip.FieldId).ToListAsync();

                var cells = new List<Cell>();
                
                if (listCellShips != null && listCellShips.Count != 0) 
                {
                    var listShipsDb = listCellShips.Select(x => x.Ship).ToList();

                    var listOfCountShips = new List<ShipDb>();

                    foreach (var sh in listShipsDb)
                    {
                        if (listOfCountShips.Count == 0)
                        {
                            listOfCountShips.Add(sh);
                        }
                        if (!listOfCountShips.Where(x => x.Id == sh.Id).Any())
                        {
                            listOfCountShips.Add(sh);
                        }
                    }

                    if (listOfCountShips != null && listOfCountShips.Count != 0)
                    {
                        var listShips = listOfCountShips.Select(x => new Ship()
                        {
                            Id = x.Id,
                            StartPositionX = x.StartPositionX,
                            StartPositionY = x.StartPositionY,
                            Direction = x.Direction,
                            Rank = x.Rank
                        }).ToList();

                        if (_gameManager.IsNumberOfShipsMax(listShips))
                        {
                            return Result<Unit>.Failure("Ship count on field is max!");
                        }
                    }

                    var listShipsByRankDb = listCellShips.Select(x => x.Ship).Where(x => x.Rank == ship.Rank).ToList();

                    var list = new List<ShipDb>();

                    foreach(var s in listShipsByRankDb)
                    {
                        if(list.Count == 0)
                        {
                            list.Add(s);
                        }
                        if (!list.Where(x => x.Id == s.Id).Any())
                        {
                            list.Add(s);
                        }
                    }

                    if(list != null && list.Count != 0)
                    {
                        var listShips = list.Select(x => new Ship()
                        {
                            Id = x.Id,
                            StartPositionX = x.StartPositionX,
                            StartPositionY = x.StartPositionY,
                            Direction = x.Direction,
                            Rank = x.Rank
                        }).ToList();

                        if (_gameManager.IsNumberOfShipsExceed(listShips))
                        {
                            return Result<Unit>.Failure("Ship rank is max count!");
                        }
                    }

                    foreach (var cellShip in listCellShips) {
                        var cellDb = await _context.Cells.FirstOrDefaultAsync(x => x.Id == cellShip.Cell.Id);
                        var c = new Cell();
                        _mapper.Map(cellDb, c);
                        cells.Add(c);
                    }

                    if(!_gameManager.IsAddShipOnField(checkShip, cells))
                    {
                        return Result<Unit>.Failure("It is not possible to add a ship to the field!");
                    }
                }
                
                await _context.Ships.AddAsync(ship);

                var n = default(int);

                switch (request.AddShip.Rank)
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

                if (request.AddShip.Direction == "Horizontal")
                {
                    for (int i = 0; i < n; i++)
                    {
                        var newShipCell = new CellDb()
                        {
                            Id = new Guid(),
                            X = request.AddShip.StartPositionX,
                            Y = request.AddShip.StartPositionY + i,
                            Status = CellStatus.Busy.ToString(),
                        };
                        await _context.Cells.AddAsync(newShipCell);
                        await _context.CellShips.AddAsync(new CellShipDb()
                        {
                            Cell = newShipCell,
                            Ship = ship,
                            Field = field
                        });
                    }
                }
                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        var newShipCell = new CellDb()
                        {
                            Id = new Guid(),
                            X = request.AddShip.StartPositionX - i,
                            Y = request.AddShip.StartPositionY,
                            Status = CellStatus.Busy.ToString(),
                        };
                        await _context.Cells.AddAsync(newShipCell);
                        await _context.CellShips.AddAsync(new CellShipDb()
                        {
                            Cell = newShipCell,
                            Ship = ship,
                            Field = field
                        });
                    }
                }
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Error add a ship");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}