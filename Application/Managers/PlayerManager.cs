using Application.Enums;
using Application.Models;

namespace Application.Managers
{
    public class PlayerManager
    {
        private readonly FieldManager _fieldManager;
        private readonly ShipManager _shipManager;


        public PlayerManager()
        {
            _fieldManager = new FieldManager();
            _shipManager = new ShipManager();
        }

        public bool IsAddShipOnField(byte startPositionX, byte startPositionY, string shipRank, string shipDirection)
        {
            var newShip = new Ship
            {
                StartPositionX = startPositionX,
                StartPositionY = startPositionY,
                Direction = shipDirection,
                Rank = shipRank
            };
            
            if(CanArrageShip(startPositionX, startPositionY, shipRank, shipDirection))
            {
                var length = default(int);
                switch(shipRank)
                {
                    case "One":
                        length = 1;
                        break;
                    case "Two":
                        length = 2;
                        break;
                    case "Three":
                        length = 3;
                        break;
                    case "Four":
                        length = 4;
                        break;
                }
                List<Cell> shipCells = new List<Cell>();
                if(shipDirection == ShipDirection.Horizontal.ToString())
                {
                    for(byte i = 0; i < length; i++)
                    {
                        _fieldManager.Cells[startPositionX + i, startPositionY].Status = CellStatus.Busy;
                        shipCells.Add(new Cell{X = (byte)(startPositionX + i), Y = startPositionY, Status = CellStatus.Busy});
                    }
                }
                else
                {
                    for(byte i = 0; i < length; i++)
                    {
                        _fieldManager.Cells[startPositionX, startPositionY + i].Status = CellStatus.Busy;
                        shipCells.Add(new Cell{X = startPositionX, Y = (byte)(startPositionY + i), Status = CellStatus.Busy});
                    }
                }
                SetForbiddenCellsAroundShip(shipCells);
                return true;
            }
            return false;
        }
        public bool IsShoot(byte x, byte y)
        {
            if(_fieldManager.Cells[x,y].Status == CellStatus.Busy)
            {
                _fieldManager.Cells[x,y].Status = CellStatus.Destroyed;
                return true;
            }
            else
            {
                _fieldManager.Cells[x, y].Status = CellStatus.ShootWithoutHit;
                 return false;
            }
        }
        private bool CanArrageShip(byte startPositionX, byte startPositionY, string shipRank, string shipDirection)
        {
            if(startPositionX >= GameRules.FIELD_SIZE || startPositionY >= GameRules.FIELD_SIZE)
                return false;
            
            var startCell = _fieldManager.Cells[startPositionX, startPositionY];

            if(startCell.Status == CellStatus.Busy || startCell.Status == CellStatus.Forbidden)
                return false;
            
            if(shipDirection == ShipDirection.Horizontal.ToString())
                return CanArrageHorizontalShip(startCell, shipRank);
            else
                return CanArrageVerticalShip(startCell, shipRank);
        }

        private bool CanArrageHorizontalShip(Cell startPoint, string shipRank)
        {
            var length = default(int);
            switch(shipRank)
            {
                case "One":
                    length = 1;
                    break;
                case "Two":
                    length = 2;
                    break;
                case "Three":
                    length = 3;
                    break;
                case "Four":
                    length = 4;
                    break;
            }

            for(byte i = 0; i < length; i++)
            {
                if((startPoint.X + i) < GameRules.FIELD_SIZE)
                {
                    var checkPoint = _fieldManager.Cells[startPoint.X + i, startPoint.Y];
                    if(checkPoint.Status == CellStatus.Busy || checkPoint.Status == CellStatus.Forbidden)
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            return true;
        }
        private 
        bool CanArrageVerticalShip(Cell startPoint, string shipRank)
        {
            var length = default(int);
            switch(shipRank)
            {
                case "One":
                    length = 1;
                    break;
                case "Two":
                    length = 2;
                    break;
                case "Three":
                    length = 3;
                    break;
                case "Four":
                    length = 4;
                    break;
            }

            for(byte i = 0; i < length; i++)
            {
                if((startPoint.Y + i) < GameRules.FIELD_SIZE)
                {
                    var checkPoint = _fieldManager.Cells[startPoint.X, startPoint.Y + i];
                    if(checkPoint.Status == CellStatus.Busy || checkPoint.Status == CellStatus.Forbidden)
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            return true;
        }
        private void SetForbiddenCellsAroundShip(IEnumerable<Cell> cells)
        {
            foreach(var cell in cells)
            {
                for(int x = cell.X - 1; x < cell.X + 1; x++)
                {
                    for(int y = cell.Y - 1; y < cell.Y; y++)
                    {
                        if(IsCellExistAndNotShip(x, y))
                        {
                            _fieldManager.Cells[x, y].Status = CellStatus.Forbidden;
                        }
                    }
                }
            }
        }
        private bool IsCellExistAndNotShip(int x, int y)
        {
           if(x >= 0 && x < GameRules.FIELD_SIZE
                && y >= 0 && y < GameRules.FIELD_SIZE
                && _fieldManager.Cells[x, y].Status != CellStatus.Busy
                && _fieldManager.Cells[x, y].Status != CellStatus.Destroyed)
                return true;
            return false;
        }
    }
}