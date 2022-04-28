using Domain;

namespace Application.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid? FirstPlayerFieldId { get; set; }
        public Guid? SecondPlayerFieldId { get; set; }
        public string? FirstPlayerName { get; set; }
        public string? SecondPlayerName { get; set; }
        public string? NameOfWinner { get; set; }
        public string GameStatus { get; set; }
        public int MoveCount { get; set; }
        public string? ResultInfo { get; set; }
    }
}