using Application.Core;
using Application.Entities;
using Application.Enums;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class Create
    {
        public class Command : IRequest<Result<Game>>
        {
            public Player Player { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Game>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Game>> Handle(Command request, CancellationToken cancellationToken)
            {
                var player = await _context.Players.Where(p => p.Name == request.Player.Name).FirstAsync();
                if (player == null) return null;

                var isGameAvailable = await _context.Games.Where(g => g.FirstPlayerName == request.Player.Name
                    && g.GameStatus == GameStatus.Started.ToString()
                    || g.SecondPlayerName == request.Player.Name
                    && g.GameStatus == GameStatus.Started.ToString()).AnyAsync();
                if (isGameAvailable == true) return null;

                var field = new FieldDb();

                await _context.Fields.AddAsync(field);

                var newGame = new GameDb
                {
                    FirstPlayerField = field,
                    FirstPlayerName = request.Player.Name,
                    GameStatus = GameStatus.NotReady.ToString(),
                    MoveCount = default(int),
                };

                await _context.Games.AddAsync(newGame);

                player.Game = newGame;

                var sendGame = new Game();

                _mapper.Map(newGame, sendGame);
                var result = await _context.SaveChangesAsync() > 0;

                if (result) return Result<Game>.Success(sendGame);

                return Result<Game>.Failure("Failure to create game");
            }
        }
    }
}