namespace Domain
{
    public class FieldDb
    {
        public Guid Id { get; set; }
        public ICollection<CellShipDb> CellShips { get; set; }
    }
}