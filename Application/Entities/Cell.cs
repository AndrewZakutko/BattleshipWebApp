using Application.Enums;

namespace Application.Models
{
    public class Cell
    {
        public byte X { get; set; }
        public byte Y { get; set; }
        public CellStatus Status { get; set; }
    }
}