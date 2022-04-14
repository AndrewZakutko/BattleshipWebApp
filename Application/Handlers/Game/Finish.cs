using MediatR;
using Persistence;

namespace Application.Handlers.Game
{
    public class Finish
    {
        public class Command : IRequest<Unit>
        {
            public Guid GameId { get; set; }
            public Guid WinnerId { get; set; }
            public int MoveCount { get; set; }
            public string ResultInfo { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var game = await _context.Games.FindAsync(request.GameId);

                if(game == null) return Unit.Value;

                var winner = await _context.Players.FindAsync(request.WinnerId);

                if(winner == null) return Unit.Value;

                game.NameOfWinner = winner.Name;
                game.MoveCount = request.MoveCount;
                game.ResultInfo = request.ResultInfo;

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}