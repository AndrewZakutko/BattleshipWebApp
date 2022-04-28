using Application.Core;
using Application.Entities;
using AutoMapper;
using Domain;
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
                var gameList = await _context.Games.ToListAsync();
                var list = new List<Game>();
                foreach(var game in gameList)
                {
                    var g = new Game();
                    _mapper.Map(game, g);
                    list.Add(g);
                }

                if (gameList == null) return Result<List<Game>>.Failure("Game list is null!");

                return Result<List<Game>>.Success(list);
            }
        }
    }
}