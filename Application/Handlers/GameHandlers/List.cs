using Application.Core;
using Application.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class List
    {
        public class Query : IRequest<Result<List<Game>>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<List<Game>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<Game>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var gameListDb = await _context.Games.ToListAsync();
                if (gameListDb == null) return null;

                var gameList = new List<Game>();

                foreach(var game in gameListDb)
                {
                    var g = new Game();
                    _mapper.Map(game, g);
                    gameList.Add(g);
                }

                return Result<List<Game>>.Success(gameList);
            }
        }
    }
}