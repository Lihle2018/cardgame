using cardgame.Models;

namespace cardgame.Services.Interfaces
{
    public interface IScoreService
    {
        List<Player> BreakTie(List<Player> tiedPlayers);
        int CalculateTotalScore(Player player);
    }
}