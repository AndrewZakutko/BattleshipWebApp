using Application.Enums;

namespace Application.Entities
{
    public class Cell
    {
        public Guid Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string CellStatus { get; set; }
    }
}