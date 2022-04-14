namespace Domain
{
    public class ShipDb
    {
        public Guid Id { get; set; }
        public byte StartPositionX { get; set; }
        public byte StartPositionY { get; set; }
        public string ShipDirection { get; set; }
        public string ShipRank { get; set; }
        public ICollection<CellShipDb> CellShips { get; set; }
    }
}