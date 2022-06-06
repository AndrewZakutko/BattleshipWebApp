using Application.Core;
using Application.Entities;
using Application.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class HistoryList
    {
        public class Command : IRequest<Result<List<Game>>>
        {
            public string Name { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<List<Game>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<Game>>> Handle(Command request, CancellationToken cancellationToken)
            {
                var historyListGames = await _context.Games.Where(x => x.FirstPlayerName == request.Name
                    || x.SecondPlayerName == request.Name).ToListAsync();
                if (historyListGames == null) return null;

                var list = new List<Game>();

                foreach(var game in historyListGames)
                {
                    if(game.Status != GameStatus.Started.ToString())
                    {
                        var g = new Game();
                        _mapper.Map(game, g);
                        list.Add(g);
                    }
                }

                return Result<List<Game>>.Success(list);
            }
        }
    }
}
