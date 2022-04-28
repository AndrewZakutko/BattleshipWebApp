using Application.Core;
using Application.Entities;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.CellHandlers
{
    public class List
    {
        public class Query : IRequest<Result<List<Cell>>> { }

        public class Handler : IRequestHandler<Query, Result<List<Cell>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<Cell>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var cellList = await _context.Cells.ToListAsync();
                var list = new List<Cell>();
                foreach (var cell in cellList)
                {
                    var c = new Cell();
                    _mapper.Map(cell, c);
                    list.Add(c);
                }
                return Result<List<Cell>>.Success(list);
            }
        }
    }
}