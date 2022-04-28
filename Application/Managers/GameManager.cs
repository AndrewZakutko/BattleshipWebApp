using Application.Entities;
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
        
        public bool IsChangeGameStatusToFinished(Game game)
        {
            if(game.GameStatus != GameStatus.Finished.ToString())
            {
                game.GameStatus = GameStatus.Finished.ToString();
                return true;
            }
            return false;
        }
    }
}