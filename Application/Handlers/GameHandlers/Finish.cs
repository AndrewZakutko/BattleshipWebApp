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

                if(game == null) return null;

                var winner = await _context.Players.Where(p => p.Name == request.Game.NameOfWinner).FirstAsync();

                if(winner == null) return null;

                var firstPlayer = await _context.Players.Where(p => p.Name == request.Game.FirstPlayerName).FirstAsync();
                var secondPlayer = await _context.Players.Where(p => p.Name == request.Game.SecondPlayerName).FirstAsync();

                firstPlayer.Game = null;
                secondPlayer.Game = null;
                
                game.NameOfWinner = request.Game.NameOfWinner;
                game.MoveCount = request.Game.MoveCount;
                game.ResultInfo = request.Game.ResultInfo;
                game.GameStatus = request.Game.GameStatus;

                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failure to finish the game");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}