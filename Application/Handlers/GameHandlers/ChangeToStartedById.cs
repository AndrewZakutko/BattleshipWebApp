using Application.Core;
using Application.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class ChangeToStartedById
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid GameId { get; set; }
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
                var game = await _context.Games.Where(x => x.Id == request.GameId).FirstOrDefaultAsync();
                if (game == null) return null;

                game.GameStatus = GameStatus.Started.ToString();
                var result = await _context.SaveChangesAsync() > 0;

                if (result) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Error is save changes");
            }
        }

    }
}
