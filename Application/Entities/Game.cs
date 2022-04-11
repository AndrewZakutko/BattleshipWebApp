using Application.Enums;

namespace Application.Models
{
    public class Game
    {
        public Player FirstPlayer { get; set; }
        public Field FirstPlayerField { get; set; }
        public Player SecondPlayer { get; set; }
        public Field SecondPlayerField { get; set; }
        public Player Winner { get; set; }
        public GameStatus Status { get; set; }
        public int MoveCount { get; set; }
        public string ResultInfo { get; set; }
    }
}