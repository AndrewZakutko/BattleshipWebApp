using Application.Core;
using Application.Entities;
using Application.Enums;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.GameHandlers
{
    public class Connect
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ConnectUser ConnectUser { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var game = await _context.Games.FindAsync(request.ConnectUser.GameId);

                if(game == null) return null;

                var secondPlayer = await _context.Players.Where(p => p.Name == request.ConnectUser.Name).FirstAsync();

                if(secondPlayer == null) return null;

                var secondPlayerField = new FieldDb();

                await _context.Fields.AddAsync(secondPlayerField);

                game.SecondPlayerField = secondPlayerField;
                game.SecondPlayerName = request.ConnectUser.Name;
                game.GameStatus = GameStatus.Started.ToString();

                secondPlayer.Game = game;
                
                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failure to connect game");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}