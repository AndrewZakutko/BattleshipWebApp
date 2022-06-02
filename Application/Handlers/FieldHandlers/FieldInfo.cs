using Application.Core;
using Application.Enums;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.FieldHandlers
{
    public class FieldInfo
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

                var field = await _context.Fields.FindAsync(request.FieldId);

                var listShips = await _context.CellShips.Where(x => x.Field.Id == request.FieldId 
                    && x.Cell.CellStatus != CellStatus.Destroyed.ToString()).Select(x => x.Ship).ToListAsync();

                var list = new List<ShipDb>();

                foreach (var s in listShips)
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

                var shipsRankOne = list.Where(x => x.ShipRank == ShipRank.One.ToString()).ToList();
                var shipsRankTwo = list.Where(x => x.ShipRank == ShipRank.Two.ToString()).ToList();
                var shipsRankThree = list.Where(x => x.ShipRank == ShipRank.Three.ToString()).ToList();
                var shipsRankFour = list.Where(x => x.ShipRank == ShipRank.Four.ToString()).ToList();

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
