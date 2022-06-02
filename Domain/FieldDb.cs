namespace Domain
{
    public class FieldDb
    {
        public Guid Id { get; set; }
        public virtual List<CellShipDb> CellShips { get; set; }
        public virtual List<GameDb> FirstPlayerGames { get; set; }
        public virtual List<GameDb> SecondPlayerGames { get; set; }
    }
}