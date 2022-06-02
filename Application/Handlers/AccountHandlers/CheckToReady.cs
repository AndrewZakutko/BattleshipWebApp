﻿using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.AccountHandlers
{
    public class CheckToReady
    {
        public class Command : IRequest<Result<bool>>
        {
            public string Name { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var player = await _context.Players.Where(x => x.Name == request.Name).FirstOrDefaultAsync();
                if (player == null) return Result<bool>.Failure("Error to take a player");

                return Result<bool>.Success(player.IsReady);
            }
        }
    }
}
