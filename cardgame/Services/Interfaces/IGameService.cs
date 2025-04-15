using cardgame.Models;

namespace cardgame.Services.Interfaces
{
    public interface IGameService
    {
        GameResult PlayGame(List<Player> players);
    }
}