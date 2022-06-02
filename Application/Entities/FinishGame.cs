namespace Application.Entities
{
    public class FinishGame
    {
        public Guid GameId { get; set; }
        public string NameOfWinner { get; set; }
        public string ResultInfo { get; set; }
    }
}
