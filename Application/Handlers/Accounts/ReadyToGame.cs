using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.Accounts
{
    public class ReadyToGame
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Name { get; set; }
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
                var player = await _context.Players.Where(x => x.Name == request.Name).FirstOrDefaultAsync();
                if (player == null) return null;

                player.IsReady = true;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Error by change player status!");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
