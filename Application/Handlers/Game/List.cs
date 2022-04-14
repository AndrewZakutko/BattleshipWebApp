using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.Game
{
    public class List
    {
        public class Query : IRequest<List<GameDb>>
        {

        }

        public class Handler : IRequestHandler<Query, List<GameDb>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<GameDb>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Games.ToListAsync();
            }
        }
    }
}