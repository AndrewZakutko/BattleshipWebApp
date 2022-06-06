using Application.Core;
using Application.Entities;
using Application.EntityHalpers;
using Application.Enums;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class Connect
    {
        public class Command : IRequest<Result<Game>>
        {
            public ConnectUser ConnectUser { get; set; }
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
                var game = await _context.Games.FindAsync(request.ConnectUser.GameId);
                if (game == null) return null;

                var secondPlayer = await _context.Players.Where(p => p.Name == request.ConnectUser.Name).FirstAsync();
                if (secondPlayer == null) return null;

                var secondPlayerField = new FieldDb();

                await _context.Fields.AddAsync(secondPlayerField);

                game.SecondPlayerField = secondPlayerField;
                game.SecondPlayerName = request.ConnectUser.Name;

                secondPlayer.Game = game;

                var sendGame = new Game();

                _mapper.Map(game, sendGame);
                var result = await _context.SaveChangesAsync() > 0;

                if (result) return Result<Game>.Success(sendGame);

                return Result<Game>.Failure("Failure to connect game");
            }
        }
    }
}