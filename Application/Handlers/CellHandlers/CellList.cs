using Application.Core;
using Application.Entities;
using Application.Managers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
namespace Application.Handlers.CellHandlers
{
    public class CellList
    {
        public class Command : IRequest<Result<List<Cell>>>
        {
            public Guid FieldId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<List<Cell>>>
        {
            private readonly DataContext _context;
            private readonly FieldManager _fieldManager;

            public Handler(DataContext context, FieldManager fieldManager)
            {
                _context = context;
                _fieldManager = fieldManager;
            }

            public async Task<Result<List<Cell>>> Handle(Command request, CancellationToken cancellationToken)
            {
                var listCells = new List<Cell>();

                var field = new Cell[GameRules.FIELD_SIZE, GameRules.FIELD_SIZE];

                var listCellShips = await _context.CellShips.Where(x => x.Field.Id == request.FieldId).ToListAsync();

                if(listCellShips == null || listCellShips.Count == 0)
                {
                    field = _fieldManager.CreateEmptyField();
                    for (int i = 0; i < GameRules.FIELD_SIZE; i++)
                    {
                        for (int j = 0; j < GameRules.FIELD_SIZE; j++)
                        {
                            listCells.Add(field[i, j]);
                        }
                    }
                    return Result<List<Cell>>.Success(listCells);
                }

                var listCellsDb = listCellShips.Select(x => x.Cell).ToList();

                var cells = listCellsDb.Select(x => new Cell()
                {
                    Id = x.Id,
                    X = x.X,
                    Y = x.Y,
                    CellStatus = x.CellStatus
                }).ToList();

                var listShootsDb = await _context.Shoots.Where(x => x.FieldId == request.FieldId).ToListAsync();

                var listShoots = listShootsDb.Select(x => new Shoot()
                {
                    Id = x.Id,
                    FieldId = x.FieldId,
                    X = x.X,
                    Y = x.Y
                }).ToList();

                field = _fieldManager.CreateField(cells, listShoots);

                for (int i = 0; i < GameRules.FIELD_SIZE; i++)
                {
                    for (int j = 0; j < GameRules.FIELD_SIZE; j++)
                    {
                        listCells.Add(field[i, j]);
                    }
                }

                return Result<List<Cell>>.Success(listCells);
            }
        }
    }
}
