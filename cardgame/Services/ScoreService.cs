using cardgame.Enums;
using cardgame.Models;
using cardgame.Services.Interfaces;

namespace cardgame.Services
{
    public class ScoreService : IScoreService
    {
        public int CalculateTotalScore(Player player)
        {
            return player.Hand.Sum(card => GetRankValue(card.Rank));
        }
        public List<Player> BreakTie(List<Player> tiedPlayers)
        {
            foreach (var player in tiedPlayers)
            {
                player.SuitScore = player.Hand
                    .Select(card => GetSuitValue(card.Suit))
                    .Aggregate(1, (a, b) => a * b);
            }
            var maxSuitScore = tiedPlayers.Max(p => p.SuitScore);
            var winners = tiedPlayers.Where(p => p.SuitScore == maxSuitScore).ToList();
            return winners;
        }

        private int GetSuitValue(Suit suit)
        {
            return (int)suit;
        }

        private int GetRankValue(Rank rank)
        {
            return (int)rank;
        }
    }
}
