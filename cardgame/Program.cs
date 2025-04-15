using cardgame.Utils;
using cardgame.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var historyService = new GameHistoryService();
        var deckService = new DeckService();
        var scoreService = new ScoreService();
        var gameService = new GameService(deckService,scoreService,historyService);
        var gameFlowManager = new GameFlowService(gameService, historyService);

        while (true)
        {
            try
            {
                Console.Clear();
                Printer.PrintGameRules();
                Printer.PrintMainMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        gameFlowManager.StartNewGame();
                        break;
                    case "2":
                        gameFlowManager.ViewGameHistory();
                        break;
                    case "3":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Printer.PrintError("Invalid choice. Please select 1, 2 or 3.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Printer.PrintError(ex.Message);
            }

            Console.WriteLine("\nPress Enter to return to the menu...");
            Console.ReadLine();
        }
    }
}

