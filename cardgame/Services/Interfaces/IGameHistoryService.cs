using cardgame.Models;

namespace cardgame.Services.Interfaces
{
    public interface IGameHistoryService
    {
        void AddGame(GameResult game);
        List<GameResult> GetAllGames();
        int GetNextGameId();
        GameResult GetTopScorer();
    }
}