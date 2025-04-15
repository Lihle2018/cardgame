using cardgame.Enums;
using cardgame.Models;

namespace cardgame.Utils
{
    public class Printer
    {
        public static void PrintGameRules()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" WELCOME TO CARD GAME");
            Console.WriteLine(new string('-', 50));
            Console.ResetColor();

            Console.WriteLine(" GAME RULES:");
            Console.WriteLine(" Play with 6 players (human or computer). Want to personalize it? You can name your players too!");
            Console.WriteLine(" Each player gets 5 cards from 2 standard 52-card decks (104 cards total).");
            Console.WriteLine(" Card values:");
            Console.WriteLine("    - Number cards (2-10) = Face value");
            Console.WriteLine("    - J = 11, Q = 12, K = 13, A = 11");
            Console.WriteLine(" Winner is the player with the highest total card value.");
            Console.WriteLine(" Tiebreaker rules:");
            Console.WriteLine("    - Suit values: Diamonds = 1, Hearts = 2, Spades = 3, Clubs = 4");
            Console.WriteLine("    - Suit Score = Multiply all 5 card suit values together");
            Console.WriteLine("    - Highest suit score wins the tie");
            Console.WriteLine("    - If multiple players have the SAME suit score:");
            Console.WriteLine("        - They become co-winners");
            Console.WriteLine("        - All tied players share the victory");
            Console.WriteLine("        - (Yep, this one's my own house rule — the brief didn’t say, so I got creative!)");
            Console.WriteLine(new string('-', 50));
        }

        public static void PrintLoader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  Dealing cards and calculating scores...\n");

            for (int i = 0; i < 3; i++)
            {
                Console.Write("  [---] [---] [---] [---]");
                Thread.Sleep(300);
                Console.Write("\r");
                Console.Write("  [ / ] [ | ] [ \\ ] [---]");
                Thread.Sleep(300);
                Console.Write("\r");
                Console.Write("  [---] [---] [---] [---]");
                Thread.Sleep(300);
                Console.Write("\r");
                Console.Write("  [ | ] [ \\ ] [---] [ / ]");
                Thread.Sleep(300);
                Console.Write("\r");
            }
            Console.ResetColor();
            Console.WriteLine("\n\n  Game complete! Here are the results:\n");
            Thread.Sleep(1000);

            Console.Clear();
        }

        public static void PrintMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nMAIN MENU");
            Console.WriteLine("1. Start New Game");
            Console.WriteLine("2. View Previous Games");
            Console.WriteLine("3. Exit");
            Console.ResetColor();
            Console.Write("> Select an option: ");
        }

        public static void PrintPlayers(List<Player> players)
        {
            Console.WriteLine("\nPlayers and their Cards:\n");

            foreach (var player in players)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string cards = string.Join(", ", player.Hand.Select(c => $"{c.Rank} {GetSuitName(c.Suit)}"));
                Console.Write($"{player.Name,-15}");
                Console.ResetColor();

                Console.Write("Cards: ");

                Console.Write(cards);
                Console.WriteLine();
            }
        }

        public static void PrintTopScorer(GameResult topGame)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nHighest Ranking Player");
            Console.ResetColor();

            Console.WriteLine($"Player: {topGame.Winner.Name}");
            Console.WriteLine($"Score : {topGame.Winner.Score}");
            Console.WriteLine($"Game  : {topGame.GameId} on {topGame.DatePlayed}");
        }


        public static void PrintGameResults(GameResult result)
        {
            Console.WriteLine("\n Game Results:\n");

            Console.WriteLine(
                $"{"Name",-20} | {"Score",-5} | {"Suit Score",-10}");
            Console.WriteLine(new string('-', 50));

            var players = result.Players;
            var winner = result.Winner;
            foreach (var player in players)
            {
                Console.WriteLine(
                    $"{player.Name,-20} | {player.Score,-5} | {player.SuitScore,-10}");
            }

            Console.WriteLine(new string('-', 50));

            var tiedPlayers = result.PlayersInTie;
            if (tiedPlayers.Any())
            {

                Console.WriteLine("\n Players in a Tie:\n");
                Console.WriteLine($"{"Name",-20} | {"Score",-5} | {"Suit Score",-10}");
                Console.WriteLine(new string('-', 50));

                foreach (var player in tiedPlayers.OrderByDescending(p => p.SuitScore))
                {
                    Console.WriteLine($"{player.Name,-20} | {player.Score,-5} | {player.SuitScore,-10}");
                }

                Console.WriteLine(new string('-', 50));

                if (result.Winner == null)
                {

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    var coWinners = tiedPlayers
                        .Where(p => p.SuitScore == tiedPlayers.Max(x => x.SuitScore))
                        .ToList();

                    Console.WriteLine("\n  The tie could not be broken!");
                    Console.WriteLine($"  There are {coWinners.Count} co-winners:");
                    Console.ResetColor();

                    foreach (var w in coWinners)
                    {
                        Console.WriteLine($" {w.Name} (Score: {w.Score}, Suit Score: {w.SuitScore})");
                    }
                }

                else
                {
                    Console.WriteLine($"\n Winner (tie-break by suit score): {winner.Name} with {winner.Score} points and suit score of {winner.SuitScore}!\n");
                }
            }
            else
            {
                Console.WriteLine($"\nWinner: {winner.Name} with {winner.Score} points!\n");
            }

        }
        public static void PrintGameHistoryPage(List<GameResult> results, int pageNumber, int pageSize)
        {
            Console.Clear();
            Console.WriteLine("\n Game History (Page " + pageNumber + "):\n");

            if (results == null || results.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No previous games found.\n");
                Console.ResetColor();
                return;
            }

            var pagedResults = results
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            Console.WriteLine("┌───────┬──────────────┬────────────┐");
            Console.WriteLine("│ Game# │ Winner       │ Score      │");
            Console.WriteLine("├───────┼──────────────┼────────────┤");

            foreach (var result in pagedResults)
            {
                Console.WriteLine($"│ {result.GameId,-5} │ {result.Winner.Name,-12} │ {result.Winner.Score,-10} │");
                Console.WriteLine($"│       │ Players:     │            │");

                foreach (var player in result.Players)
                {
                    Console.WriteLine($"│       │  - {player.Name,-10}│            │");
                }

                Console.WriteLine("├───────┼──────────────┼────────────┤");
            }

            Console.WriteLine("└───────┴──────────────┴────────────┘\n");
        }



        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ERROR]: {message}\n");
            Console.ResetColor();
        }

        private static string GetSuitName(Suit suit)
        {
            return suit switch
            {
                Suit.Clubs => "Clubs",
                Suit.Diamonds => "Diamonds",
                Suit.Hearts => "Hearts",
                Suit.Spades => "Spades",
                _ => "Joker"
            };
        }

    }
}
