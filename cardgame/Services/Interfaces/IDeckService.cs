using cardgame.Models;

namespace cardgame.Services.Interfaces
{
    public interface IDeckService
    {
        Card DealCard();
        void GenerateDeck();
    }
}