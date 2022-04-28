using Application.Core;
using Application.Entities;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class Details
    {
        public class Query : IRequest<Result<Game>>
        {
            public Guid GameId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Game>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Game>> Handle(Query request, CancellationToken cancellationToken)
            {
                var game = await _context.Games.FindAsync(request.GameId);

                var g = new Game();

                _mapper.Map(game, g);

                if (game == null) return Result<Game>.Failure("Game not found!");

                return Result<Game>.Success(g);
            }
        }
    }
}