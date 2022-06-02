namespace Application.Entities
{
    public class Shoot
    {
        public Guid Id { get; set; }
        public Guid FieldId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
