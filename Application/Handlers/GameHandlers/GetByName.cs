using Application.Core;
using Application.Entities;
using Application.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class GetByName
    {
        public class Command : IRequest<Result<Game>>
        {
            public string Name { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Game>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Game>> Handle(Command request, CancellationToken cancellationToken)
            {
                var gameDb = await _context.Games.Where(x => x.FirstPlayerName == request.Name
                    && x.Status != GameStatus.Finished.ToString()
                    || x.SecondPlayerName == request.Name
                    && x.Status != GameStatus.Finished.ToString()).FirstOrDefaultAsync();

                var game = new Game();
                
                if(gameDb == null)
                {
                    return Result<Game>.Success(game); 
                }

                _mapper.Map(gameDb, game);
                return Result<Game>.Success(game);
            }
        }
    }
}
