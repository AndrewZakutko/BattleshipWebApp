namespace Application.Entities
{
    public class AddShip
    {
        public Guid FieldId { get; set; }
        public byte StartPositionX { get; set; }
        public byte StartPositionY { get; set; }
        public string Direction { get; set; }
        public string Rank { get; set; }
    }
}