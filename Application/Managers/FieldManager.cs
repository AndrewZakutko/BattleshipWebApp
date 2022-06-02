using Application.Entities;
using Application.Enums;

namespace Application.Managers
{
    public class FieldManager
    {
        public Cell[,] Cells { get; set; } = new Cell[GameRules.FIELD_SIZE, GameRules.FIELD_SIZE];
        public FieldManager()
        {
        }

        public Cell[,] CreateField(List<Cell> cells, List<Shoot> shoots)
        {
            
            for (int i = 0; i < GameRules.FIELD_SIZE; i++)
            {
                for (int j = 0; j < GameRules.FIELD_SIZE; j++)
                {
                    Cells[i, j] = new Cell
                    {
                        Id = Guid.NewGuid(),
                        X = i,
                        Y = j,
                        CellStatus = CellStatus.None.ToString()
                    };
                }
            }

            foreach (var cell in cells)
            {
                Cells[cell.X, cell.Y].Id = cell.Id;   
                Cells[cell.X, cell.Y].CellStatus = cell.CellStatus;
            }

            SetForbiddenCells(cells);

            if(shoots != null || shoots != null && shoots.Count > 0)
            {
                foreach (var shoot in shoots)
                {
                    var x = shoot.X;
                    var y = shoot.Y;
                    if (Cells[x, y].CellStatus == CellStatus.Busy.ToString())
                    {
                        Cells[x, y].CellStatus = CellStatus.Destroyed.ToString();
                    }
                    if(Cells[x, y].CellStatus == CellStatus.Forbidden.ToString() ||
                        Cells[x, y].CellStatus == CellStatus.None.ToString())
                    {
                        Cells[x, y].CellStatus = CellStatus.ShootWithoutHit.ToString();
                    }
                }
            }

            return Cells;
        }

        public Cell[,] CreateEmptyField()
        {

            for (int i = 0; i < GameRules.FIELD_SIZE; i++)
            {
                for (int j = 0; j < GameRules.FIELD_SIZE; j++)
                {
                    Cells[i, j] = new Cell
                    {
                        Id = Guid.NewGuid(),
                        X = i,
                        Y = j,
                        CellStatus = CellStatus.None.ToString()
                    };
                }
            }
            return Cells;
        }

        private void SetForbiddenCells(List<Cell> cells)
        {
            foreach (var cell in cells)
            {
                for (int x = cell.X + 1; x >= cell.X - 1; x--)
                {
                    for(int y = cell.Y - 1; y <= cell.Y + 1; y++)
                    {
                        if(IsCellExistAndNotShip(x, y))
                        {
                            Cells[x, y].CellStatus = CellStatus.Forbidden.ToString();
                        }
                    }
                }
            }
        }
        private bool IsCellExistAndNotShip(int x, int y)
        {
            if (x >= 0 && x < GameRules.FIELD_SIZE
                && y >= 0 && y < GameRules.FIELD_SIZE
                && Cells[x, y].CellStatus != CellStatus.Busy.ToString()
                && Cells[x, y].CellStatus != CellStatus.Destroyed.ToString())
                return true;
            return false;
        }
    }
}