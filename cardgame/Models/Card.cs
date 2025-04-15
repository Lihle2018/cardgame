using cardgame.Enums;

namespace cardgame.Models
{
    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }

}
