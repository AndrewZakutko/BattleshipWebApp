using Application.Enums;

namespace Application.Entities
{
    public class Ship
    {
        public Guid Id { get; set; }
        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }
        public string ShipDirection { get; set; }
        public string ShipRank { get; set; }
    }
}