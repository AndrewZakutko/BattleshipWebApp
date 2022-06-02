using Application.Entities;

namespace Application.Managers
{
    public class ShootManager
    {
        public ShootManager()
        {
        }

        public bool IsHit(List<Cell> cells, int x, int y)
        {
            if(cells.Where(c => c.X == x && c.Y == y).Any())
            {
                return true;
            }
            return false;
        }
    }
}
