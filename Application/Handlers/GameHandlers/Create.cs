using Application.Core;
using Application.Entities;
using Application.Enums;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Player Player { get; set; }
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
                var player = await _context.Players.AnyAsync(p => p.Name == request.Player.Name);

                if (player == false) return Result<Unit>.Failure("Player not found");

                var field = new FieldDb();
                
                await _context.Fields.AddAsync(field);
                
                var game = new GameDb
                {
                    FirstPlayerFieldId = field.Id.ToString(),
                    FirstPlayerName = request.Player.Name,
                    GameStatus = GameStatus.NotReady.ToString(),
                    MoveCount = default(int),
                };

                await _context.Games.AddAsync(game);
                
                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failure to create game");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}