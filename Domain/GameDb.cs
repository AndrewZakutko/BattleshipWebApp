namespace Domain
{
    public class GameDb
    {
        public Guid Id { get; set; }
        public string? FirstPlayerName { get; set; }
        public string? SecondPlayerName { get; set; }
        public string? NameOfWinner { get; set; }
        public string? Status { get; set; }
        public int MoveCount { get; set; } = default(int);
        public string? ResultInfo { get; set; }
        public virtual FieldDb? FirstPlayerField { get; set; }
        public virtual FieldDb? SecondPlayerField { get; set; }
        public virtual List<PlayerDb> Players { get; set; }
    }
}