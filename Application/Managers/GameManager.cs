using Application.Entities;
using Application.Enums;

namespace Application.Managers
{
    public class GameManager
    {
        private readonly FieldManager _fieldManager;
        public GameManager(FieldManager fieldManager)
        {
            _fieldManager = fieldManager;
        }

        public bool IsAddShipOnField(Ship ship, List<Cell> cells)
        {
            var listCells = cells;

            var field = _fieldManager.CreateField(listCells, null);

            var n = default(int);

            switch (ship.ShipRank)
            {
                case "One":
                    n = 1;
                    break;
                case "Two":
                    n = 2;
                    break;
                case "Three":
                    n = 3;
                    break;
                case "Four":
                    n = 4;
                    break;
            }

            var x = ship.StartPositionX;
            var y = ship.StartPositionY;

            for(int i = 0; i < n; i++)
            {
                if(ship.ShipDirection == "Horizontal")
                {
                    if(field[x, y + i].CellStatus == CellStatus.Busy.ToString() || 
                        field[x, y + i].CellStatus == CellStatus.Forbidden.ToString())
                    {
                        return false;
                    }
                }
                else 
                {
                    if(field[x - i, y].CellStatus == CellStatus.Busy.ToString() || 
                        field[x - i, y].CellStatus == CellStatus.Forbidden.ToString())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsNumberOfShipsExceed(List<Ship> ships)
        {
            var shipRank = ships[0].ShipRank;
            switch(shipRank)
            {
                case "One":
                    if(ships.Count == GameRules.SHIP_RANK_ONE_MAX_COUNT)
                    {
                        return true;
                    }
                    break;
                case "Two":
                    if (ships.Count == GameRules.SHIP_RANK_TWO_MAX_COUNT)
                    {
                        return true;
                    }
                    break;
                case "Three":
                    if (ships.Count == GameRules.SHIP_RANK_THREE_MAX_COUNT)
                    {
                        return true;
                    }
                    break;
                case "Four":
                    if (ships.Count == GameRules.SHIP_RANK_FOUR_MAX_COUNT)
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        public bool IsNumberOfShipsMax(List<Ship> ships)
        {
            if(ships.Count == GameRules.MAX_SHIPS_COUNT)
            {
                return true;
            }
            return false;
        }
    }
}