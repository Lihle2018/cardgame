namespace cardgame.Models
{
    public class GameResult
    {
        public int GameId { get; set; }
        public List<Player> Players { get; set; }
        public List<Player> PlayersInTie { get; set; }
        public Player Winner { get; set; }
        public int Score { get; set; }
        public DateTime DatePlayed { get; set; }
    }
}
