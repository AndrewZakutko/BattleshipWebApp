namespace Domain
{
    public class CellDb
    {
        public Guid Id { get; set; }
        public string? ShipDbId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string CellStatus { get; set; }
    }
}