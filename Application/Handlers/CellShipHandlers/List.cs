using Application.Core;
using Application.Entities;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.CellShipHandlers
{
    public class List
    {
        public class Query : IRequest<Result<List<CellShip>>> { }

        public class Handler : IRequestHandler<Query, Result<List<CellShip>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<CellShip>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var cellShipsList = await _context.CellShips.ToListAsync();
                var list = new List<CellShip>();
                foreach(var cellShip in cellShipsList)
                {
                    var cs = new CellShip();
                    _mapper.Map(cellShip, cs);
                    list.Add(cs);
                }
                return Result<List<CellShip>>.Success(list);
            }
        }
    }
}