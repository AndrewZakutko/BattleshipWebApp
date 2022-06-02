using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class CellShipDb
    {
        public Guid Id { get; set; }
        public virtual CellDb? Cell { get; set; }
        public virtual ShipDb? Ship { get; set; }
        public virtual FieldDb? Field { get; set; }
    }
}