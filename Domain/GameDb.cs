namespace Domain
{
    public class GameDb
    {
        public Guid Id { get; set; }
        public string? FirstPlayerFieldId { get; set; }
        public string? SecondPlayerFieldId { get; set; }
        public string? FirstPlayerName { get; set; }
        public string? SecondPlayerName { get; set; }
        public string? NameOfWinner { get; set; }
        public string GameStatus { get; set; }
        public int MoveCount { get; set; }
        public string? ResultInfo { get; set; }
    }
}