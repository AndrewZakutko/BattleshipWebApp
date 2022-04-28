using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class CellShipDb
    {
        public Guid Id { get; set; }
        public CellDb? Cell { get; set; }
        public Guid? CellId { get; set; }
        public ShipDb? Ship { get; set; }
        public Guid? ShipId { get; set; }
        public FieldDb? Field { get; set; }
        public Guid? FieldId { get; set; }
    }
}