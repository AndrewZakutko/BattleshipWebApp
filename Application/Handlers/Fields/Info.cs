using Application.Core;
using Application.Enums;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.Fields
{
    public class Info
    {
        public class Command : IRequest<Result<string>>
        {
            public Guid FieldId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var info = String.Empty;

                var fieldDb = await _context.Fields.FindAsync(request.FieldId);

                var listShipsDb = await _context.CellShips.Where(x => x.Field.Id == request.FieldId 
                    && x.Cell.Status != CellStatus.Destroyed.ToString()).Select(x => x.Ship).ToListAsync();

                var list = new List<ShipDb>();

                foreach (var s in listShipsDb)
                {
                    if (list.Count == 0)
                    {
                        list.Add(s);
                    }
                    if (!list.Where(x => x.Id == s.Id).Any())
                    {
                        list.Add(s);
                    }
                }

                info = $"Count of ships on field: {list.Count}";

                var shipsRankOne = list.Where(x => x.Rank == ShipRank.One.ToString()).ToList();
                var shipsRankTwo = list.Where(x => x.Rank == ShipRank.Two.ToString()).ToList();
                var shipsRankThree = list.Where(x => x.Rank == ShipRank.Three.ToString()).ToList();
                var shipsRankFour = list.Where(x => x.Rank == ShipRank.Four.ToString()).ToList();

                if(shipsRankOne.Count > 0)
                {
                    info += $", count of ships with rank one : {shipsRankOne.Count}";
                }

                if(shipsRankTwo.Count > 0)
                {
                    info += $", count of ships with rank two : {shipsRankTwo.Count}";
                }

                if(shipsRankThree.Count > 0)
                {
                    info += $", count of ships with rank three : {shipsRankThree.Count}";
                }

                if(shipsRankFour.Count > 0)
                {
                    info += $", count of ships with rank four : {shipsRankFour.Count}";
                }

                return Result<string>.Success(info);
            }
        }
    }
}
