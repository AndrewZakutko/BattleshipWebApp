namespace Application.Entities
{
    public class CellShip
    {
        public Guid Id { get; set; }
        public Guid CellId { get; set; }
        public Guid ShipId { get; set; }
        public Guid FieldId { get; set; }
    }
}