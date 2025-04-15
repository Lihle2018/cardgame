using cardgame.Models;
using cardgame.Services.Interfaces;
using cardgame.Utils;

namespace cardgame.Services
{
    public class GameFlowService(IGameService gameService, IGameHistoryService historyService) : IGameFlowService
    {
        public void StartNewGame()
        {
            List<Player> players = new List<Player>();
            var numPlayers = 6;

            bool wantsToNamePlayers = Validator.GetYesOrNo("Do you want to name any players? (y/n): ");

            if (wantsToNamePlayers)
            {
                var customPlayers = Validator.GetValidatedInt($"Enter number of players you want to name (1–{numPlayers}): ", 1, numPlayers);
                for (int i = 0; i < customPlayers; i++)
                {
                    string name = Validator.ValidatePlayerName($"Enter name for Player {i + 1}: ", 
                        players);
                    players.Add(new Player { Name = name });
                }

                if (customPlayers < numPlayers)
                {
                    AddComputerPlayers(players, numPlayers - customPlayers);
                }
            }
            else
            {
                AddComputerPlayers(players, numPlayers);
            }

            GameResult result = gameService.PlayGame(players);
            Printer.PrintLoader();
            Printer.PrintPlayers(players);
            Printer.PrintGameResults(result);
        }

        public void ViewGameHistory()
        {
            const int pageSize = 5;
            var results = historyService.GetAllGames();
            int totalPages = (int)Math.Ceiling((double)results.Count / pageSize);
            int currentPage = 1;

            while (true)
            {
                Printer.PrintGameHistoryPage(results, currentPage, pageSize);
                var topGame = historyService.GetTopScorer();
                if (topGame != null)
                {
                    Printer.PrintTopScorer(topGame);
                }

                Console.WriteLine($"Page {currentPage}/{totalPages}. (N)ext, (P)revious, (Q)uit");
                var input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.N && currentPage < totalPages)
                {
                    currentPage++;
                }
                else if (input == ConsoleKey.P && currentPage > 1)
                {
                    currentPage--;
                }
                else if (input == ConsoleKey.Q)
                {
                    break;
                }
            }
        }

        private void AddComputerPlayers(List<Player> players, int numPlayers)
        {
            for (int i = 1; i <= numPlayers; i++)
            {
                players.Add(new Player { Name = $"Computer {i}" });
            }
        }
    }
}

