using Domain;
using MediatR;
using Persistence;

namespace Application.Handlers.Game
{
    public class Connect
    {
        public class Command : IRequest<Unit>
        {
            public Guid PlayerId { get; set; }
            public Guid GameId { get; set; }
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

                var secondUser = await _context.Players.FindAsync(request.PlayerId);

                if(secondUser == null) return Unit.Value;

                var secondUserField = new FieldDb();

                await _context.Fields.AddAsync(secondUserField);

                game.SecondPlayerField = secondUserField;
                game.SecondPlayerName = secondUser.Name;
                
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}