using Application.Enums;

namespace Application.Models
{
    public class Ship
    {
        public byte StartPositionX { get; set; }
        public byte StartPositionY { get; set; }
        public ShipDirection Direction { get; set; }
        public ShipRank Rank { get; set; }
    }
}