namespace Domain
{
    public class ShipDb
    {
        public Guid Id { get; set; }
        public byte startPositionX { get; set; }
        public byte startPositionY { get; set; }
        public string ShipDirection { get; set; }
        public string ShipRank { get; set; }
        public ICollection<CellShipDb> CellShips { get; set; }
    }
}