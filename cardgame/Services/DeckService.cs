using cardgame.Enums;
using cardgame.Models;
using cardgame.Services.Interfaces;

namespace cardgame.Services
{
    public class DeckService : IDeckService
    {
        private Stack<Card> _deck;
        public DeckService()
        {
            _deck = new Stack<Card>();
        }

        public void GenerateDeck()
        {
            var cards = new List<Card>();

            for (int i = 0; i < 2; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    {
                            cards.Add(new Card(suit, rank));
                    }
                }
            }

            Shuffle(cards);

            _deck = new Stack<Card>(cards);
        }

        public Card DealCard()
        {
            if (_deck.Count == 0)
                throw new InvalidOperationException("Deck is empty.");
            return _deck.Pop();
        }

        private void Shuffle(List<Card> cards)
        {
            int n = cards.Count;
            var _random = new Random();
            while (n > 1)
            {
                int k = _random.Next(n--);
                (cards[n], cards[k]) = (cards[k], cards[n]);
            }
        }

    }
}
