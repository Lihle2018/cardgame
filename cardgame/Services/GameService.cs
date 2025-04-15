using cardgame.Models;
using cardgame.Services.Interfaces;

namespace cardgame.Services
{
    public class GameService(IDeckService deckService, IScoreService scoreService, IGameHistoryService historyService) : IGameService
    {
        public GameResult PlayGame(List<Player> players)
        {
            deckService.GenerateDeck();

            foreach (var player in players)
            {
                player.Hand.Clear();
                for (int i = 0; i < 5; i++)
                {
                    player.Hand.Add(deckService.DealCard());
                }
            }

            foreach (var player in players)
            {
                player.Score = scoreService.CalculateTotalScore(player);
            }

            var topScore = players.Max(p => p.Score);
            var topPlayers = players.Where(p => p.Score == topScore).ToList();

            Player winner = null;

            if (topPlayers.Count == 1)
            {
                winner = topPlayers.First();
            }
            else
            {
                var result = scoreService.BreakTie(topPlayers);
               if(result.Count == 1)
                {
                    winner = result.First();
                }
                else
                {
                    topPlayers = result;
                }
            }

            var gameResult = new GameResult
            {
                GameId = historyService.GetNextGameId(),
                Players = players.Select(p => p.Copy()).ToList(),
                Winner = winner,
                Score = winner?.Score??0,
                PlayersInTie = topPlayers.Count > 1 ? topPlayers : new List<Player>(),
                DatePlayed = DateTime.Now
            };

            historyService.AddGame(gameResult);

            return gameResult;
        }
    }
}
