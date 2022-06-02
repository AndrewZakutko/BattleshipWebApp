using Application.Core;
using Application.Entities;
using Application.Enums;
using Application.Managers;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.ShootHandlers
{
    public class TakeAShoot
    {
        public class Command : IRequest<Result<bool>>
        {
            public TakeShoot Shoot { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly DataContext _context;
            private readonly ShootManager _shootManager;

            public Handler(DataContext context, ShootManager shootManager)
            {
                _context = context;
                _shootManager = shootManager;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var firstPlayer = await _context.Players.Where(p => p.Name == request.Shoot.FirstPlayerName).FirstOrDefaultAsync();
                if (firstPlayer == null) return null;

                var secondPlayer = await _context.Players.Where(p => p.Name == request.Shoot.SecondPlayerName).FirstOrDefaultAsync();
                if (secondPlayer == null) return null;

                var shoot = new ShootDb()
                {
                    FieldId = request.Shoot.FieldId,
                    X = request.Shoot.X,
                    Y = request.Shoot.Y
                };

                if(shoot.X >= 0 && shoot.X < 10 && shoot.Y >= 0 && shoot.Y < 10)
                {
                    if(_context.Shoots.Where(x => x.FieldId == shoot.FieldId).Where(x => x.X == shoot.X && x.Y == shoot.Y)
                        .Any())
                    {
                        return Result<bool>.Failure("Error by take a shoot!");
                    }
                    await _context.Shoots.AddAsync(shoot);
                    var result = await _context.SaveChangesAsync() > 0;
                    if(result)
                    {
                        var cellsDb = await _context.CellShips.Where(ch => ch.Field.Id == request.Shoot.FieldId)
                            .Select(c => c.Cell).ToListAsync();

                        var cells = cellsDb.Select(c => new Cell()
                        {
                            Id = c.Id,
                            X = c.X,
                            Y = c.Y,
                            CellStatus = c.CellStatus
                        }).ToList();

                        if (_shootManager.IsHit(cells, request.Shoot.X, request.Shoot.Y))
                        {
                            var cell = cellsDb.Where(c => c.X == request.Shoot.X &&
                                c.Y == request.Shoot.Y).FirstOrDefault();
                            cell.CellStatus = CellStatus.Destroyed.ToString();
                            firstPlayer.IsGoing = true;
                            secondPlayer.IsGoing = false;
                            firstPlayer.MoveCount += 1;
                            await _context.SaveChangesAsync();
                            return Result<bool>.Success(true);
                        }
                        else
                        {
                            firstPlayer.IsGoing = false;
                            secondPlayer.IsGoing = true;
                            firstPlayer.MoveCount += 1;
                            await _context.SaveChangesAsync();
                            return Result<bool>.Success(false);
                        }
                    }
                }
                return Result<bool>.Failure("Error do a shoot!");
            }
        }
    }
}
