using Application.Core;
using Application.Entities;
using Application.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class Finish
    {
        public class Command : IRequest<Result<Unit>>
        {
            public FinishGame FinishGame { get; set; }
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
                var game = await _context.Games.FindAsync(request.FinishGame.GameId);
                if (game == null) return null;

                var winner = await _context.Players.Where(p => p.Name == request.FinishGame.NameOfWinner).FirstOrDefaultAsync();
                if (winner == null) return null;

                game.NameOfWinner = request.FinishGame.NameOfWinner;
                game.MoveCount = winner.MoveCount;
                game.ResultInfo = request.FinishGame.ResultInfo;
                game.GameStatus = GameStatus.Finished.ToString();

                var firstPlayer = await _context.Players.Where(p => p.Name == game.FirstPlayerName).FirstOrDefaultAsync();
                var secondPlayer = await _context.Players.Where(p => p.Name == game.SecondPlayerName).FirstOrDefaultAsync();

                firstPlayer.Game = null;
                firstPlayer.IsReady = false;
                firstPlayer.IsGoing = false;
                firstPlayer.MoveCount = default(int);

                secondPlayer.Game = null;
                secondPlayer.IsReady = false;
                secondPlayer.IsGoing = false;
                secondPlayer.MoveCount = default(int);

                var result = await _context.SaveChangesAsync() > 0;
                if(result) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Error by finish game!");
            }
        }
    }
}