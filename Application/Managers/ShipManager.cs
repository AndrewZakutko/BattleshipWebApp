using Application.Enums;
using Application.Models;

namespace Application.Managers
{
    public class ShipManager
    {
        private readonly Dictionary<ShipRank, byte> _shipsRulesCount = new Dictionary<ShipRank, byte>
        {
            { ShipRank.One, GameRules.SHIP_RANK_ONE_MAX_COUNT },
            { ShipRank.Two, GameRules.SHIP_RANK_TWO_MAX_COUNT },
            { ShipRank.Three, GameRules.SHIP_RANK_THREE_MAX_COUNT },
            { ShipRank.Four, GameRules.SHIP_RANK_FOUR_MAX_COUNT}
        };

        public Dictionary<ShipRank, byte> ShipsCount { get; set; }
        public ICollection<Ship> Ships { get; set; }
        public bool ShipsOnFieldIsFull 
        {
            get 
            { 
                if(Ships.Count >= GameRules.MAX_SHIPS_COUNT) 
                    return true;
                return false;
            } 
        }
        public ShipManager()
        {
            ShipsCount = new Dictionary<ShipRank, byte>
            {
                { ShipRank.One, 0 },
                { ShipRank.Two, 0 },
                { ShipRank.Three, 0 },
                { ShipRank.Four, 0 }
            };
        }
        
    }
}