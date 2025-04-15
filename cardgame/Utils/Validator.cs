using cardgame.Models;

namespace cardgame.Utils
{
    public class Validator
    {
        public static int GetValidatedInt(string prompt, int min, int max)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out value))
                {
                    Printer.PrintError("Please enter a valid number.");
                    continue;
                }

                if (value < min || value > max)
                {
                    Printer.PrintError($"Value must be between {min} and {max}.");
                    continue;
                }

                return value;
            }
        }

        public static string ValidatePlayerName(string prompt, List<Player> existingPlayers = null)
        {
            while (true)
            {
                Console.Write(prompt);
                string name = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Printer.PrintError("Name cannot be empty or whitespace.");
                    continue;
                }

                if (name.Length < 3)
                {
                    Printer.PrintError("Name must be at least 3 characters long.");
                    continue;
                }

                if (existingPlayers != null &&  existingPlayers.Any(p =>string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)))
                {
                    Printer.PrintError($"Name '{name}' is already taken. Please choose another.");
                    continue;
                }

                return name;
            }
        }

        public static bool GetYesOrNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                ConsoleKey key = Console.ReadKey(true).Key;
                Console.WriteLine();

                if (key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (key == ConsoleKey.N)
                {
                    return false;
                }
                else
                {
                    Printer.PrintError("Invalid input. Press 'Y' for yes or 'N' for no.");
                }
            }
        }
    }
}
