namespace Domain
{
    public class ShipDb
    {
        public Guid Id { get; set; }
        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }
        public string Direction { get; set; }
        public string Rank { get; set; }
        public virtual List<CellShipDb> CellShips { get; set; }
    }
}