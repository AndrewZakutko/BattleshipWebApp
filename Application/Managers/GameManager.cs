using Application.Enums;
using Application.Models;

namespace Application.Managers
{
    public class GameManager
    {
        public ICollection<Game> Games { get; set; }
        public GameManager()
        {
        }
        public bool IsCreateNewGame(Player player)
        {
            if(Games.Any(g => g.FirstPlayer.Name == player.Name))
            {
                return false;
            }
            var playerField = new Field();
            Games.Add(new Game { FirstPlayer = player, FirstPlayerField = playerField, Status = GameStatus.NotReady.ToString()});
            return true;
        }
        public bool IsConnectToGame(Player player, Game game)
        {
            if(game.SecondPlayer == null)
            {
                var playerField = new Field();
                game.SecondPlayer = player;
                game.SecondPlayerField = playerField;
                game.Status = GameStatus.Started.ToString();
                return true;
            }
            return false;
        }
        public bool IsChangeGameStatusToFinished(Game game)
        {
            if(game.Status != GameStatus.Finished.ToString())
            {
                game.Status = GameStatus.Finished.ToString();
                return true;
            }
            return false;
        }
    }
}