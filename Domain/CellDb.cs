namespace Domain
{
    public class CellDb
    {
        public Guid Id { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
        public string CellStatus { get; set; }
        public ICollection<CellShipDb> CellShips { get; set; }
    }
}