namespace Domain
{
    public class FieldDb
    {
        public Guid Id { get; set; }
        public List<CellShipDb> CellShips { get; set; } = new();
    }
}