namespace Application.Entities
{
    public class Ship
    {
        public Guid Id { get; set; }
        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }
        public string Direction { get; set; }
        public string Rank { get; set; }
    }
}

