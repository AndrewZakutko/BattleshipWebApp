using Application.Enums;
using Application.Models;

namespace Application.Managers
{
    public class FieldManager
    {
        public Cell[,] Cells { get; set; }
        
        public FieldManager()
        {
            Cells = new Cell[GameRules.FIELD_SIZE, GameRules.FIELD_SIZE];

            for(byte x = 0; x < GameRules.FIELD_SIZE; x++)
            {
                for(byte y = 0; y < GameRules.FIELD_SIZE; y++)
                {
                    Cells[x, y] = new Cell
                    {
                        X = x,
                        Y = y,
                        Status = CellStatus.None
                    };
                }
            }
        }
        public void ClearField()
        {
            for(byte x = 0; x < GameRules.FIELD_SIZE; x++)
            {
                for(byte y = 0; y < GameRules.FIELD_SIZE; y++)
                {
                    Cells[x, y] = new Cell
                    {
                        X = x,
                        Y = y,
                        Status = CellStatus.None
                    };
                }
            }
        }
    }
}