using Application.Core;
using Application.Entities;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class List
    {
        public class Query : IRequest<Result<List<GameDb>>>
        {

        }   

        public class Handler : IRequestHandler<Query, Result<List<GameDb>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<GameDb>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var gameList = await _context.Games.ToListAsync();

                if(gameList == null) return Result<List<GameDb>>.Failure("Game list is null!");

                return Result<List<GameDb>>.Success(gameList);
            }
        }
    }
}