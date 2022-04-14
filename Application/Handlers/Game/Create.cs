using Application.Enums;
using Domain;
using MediatR;
using Persistence;

namespace Application.Handlers.Games
{
    public class Create
    {
        public class Command : IRequest<Unit>
        {
            public Guid PlayerId { get; set; }
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
                var player = await _context.Players.FindAsync(request.PlayerId);

                if (player == null) return Unit.Value;

                var field = new FieldDb();
                
                var game = new GameDb
                {
                    FirstPlayerField = field,
                    FirstPlayerName = player.Name,
                    GameStatus = GameStatus.NotReady.ToString(),
                    MoveCount = default(int),
                };

                await _context.Fields.AddAsync(field);
                await _context.Games.AddAsync(game);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}