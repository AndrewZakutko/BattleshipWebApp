using Application.Core;
using Application.Entities;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.ShipHandlers
{
    public class List
    {
        public class Query : IRequest<Result<List<Ship>>> { }

        public class Handler : IRequestHandler<Query, Result<List<Ship>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<Ship>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var shipList = await _context.Ships.ToListAsync();

                var list = new List<Ship>();

                foreach (var ship in shipList)
                {
                    var s = new Ship();
                    _mapper.Map(ship, s);
                    list.Add(s);
                }

                return Result<List<Ship>>.Success(list);
            }
        }
    }
}