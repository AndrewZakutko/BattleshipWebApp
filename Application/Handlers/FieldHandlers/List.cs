using Application.Core;
using Application.Entities;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.FieldHandlers
{
    public class List
    {
        public class Query : IRequest<Result<List<Field>>> { }

        public class Handler : IRequestHandler<Query, Result<List<Field>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<Field>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var fieldList = await _context.Fields.ToListAsync();
                var list = new List<Field>();
                foreach (var field in fieldList)
                {
                    var f = new Field();
                    _mapper.Map(field, f);
                    list.Add(f);
                }
                return Result<List<Field>>.Success(list);
            }
        }
    }
}