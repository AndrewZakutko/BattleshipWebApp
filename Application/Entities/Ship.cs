using Application.Enums;

namespace Application.Models
{
    public class Ship
    {
        public byte StartPositionX { get; set; }
        public byte StartPositionY { get; set; }
        public string Direction { get; set; }
        public string Rank { get; set; }
    }
}