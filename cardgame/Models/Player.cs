namespace cardgame.Models
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; } = new List<Card>();
        public int Score { get; set; }
        public int SuitScore { get; set; } // for tie-breaker


        public Player Copy()
        {
            var copiedPlayer = new Player
            {
                Name = Name,
                Score = Score,
                SuitScore = SuitScore,
                Hand = Hand
            };

            return copiedPlayer;
        }
        public override string ToString()
        {
            return string.Join(", ", Hand.Select(card => card.ToString()));
        }
    }

}
