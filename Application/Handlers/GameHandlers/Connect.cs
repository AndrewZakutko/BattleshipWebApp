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

                if(game == null) return Result<Unit>.Failure("Game is null!");

                var secondPlayer = await _context.Players.AnyAsync(p => p.Name == request.ConnectUser.Name);

                if(secondPlayer == false) return Result<Unit>.Failure("Second player not found!");;

                var secondPlayerField = new FieldDb();

                await _context.Fields.AddAsync(secondPlayerField);

                game.SecondPlayerFieldId = secondPlayerField.Id.ToString();
                game.SecondPlayerName = request.ConnectUser.Name;
                game.GameStatus = GameStatus.Started.ToString();
                
                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failure to connect game");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}