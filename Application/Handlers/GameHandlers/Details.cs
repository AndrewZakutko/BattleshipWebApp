using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class Details
    {
        public class Query : IRequest<Result<GameDb>>
        {
            public Guid GameId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<GameDb>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<GameDb>> Handle(Query request, CancellationToken cancellationToken)
            {
                var game = await _context.Games.FindAsync(request.GameId);

                if(game == null) return Result<GameDb>.Failure("Game not found!");

                return Result<GameDb>.Success(game);
            }
        }
    }
}