using Application.Core;
using Application.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class Finish
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Game Game { get; set; }
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
                var game = await _context.Games.FindAsync(request.Game.Id);

                if(game == null) return Result<Unit>.Failure("Game not found!");

                var winner = await _context.Players.AnyAsync(p => p.Name == request.Game.NameOfWinner);

                if(winner == false) return Result<Unit>.Failure("Player name not found!");

                game.NameOfWinner = request.Game.NameOfWinner;
                game.MoveCount = request.Game.MoveCount;
                game.ResultInfo = request.Game.ResultInfo;

                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failure to finish the game");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}