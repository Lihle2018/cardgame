using cardgame.Models;
using cardgame.Services.Interfaces;
using cardgame.Utils;
using System.Text.Json;

namespace cardgame.Services
{
    public class GameHistoryService : IGameHistoryService
    {
        private const string FilePath = "game_history.json";
        private List<GameResult> _games = new List<GameResult>();

        public GameHistoryService()
        {
            LoadFromFile();
        }

        public void AddGame(GameResult game)
        {
            _games.Add(game);
            SaveToFile();
        }


        public int GetNextGameId()
        {
            return _games.Count + 1;
        }

        public List<GameResult> GetAllGames()
        {
            return _games;
        }

        public GameResult GetTopScorer()
        {
            return _games.OrderByDescending(g => g.Score).FirstOrDefault();
        }

        private void SaveToFile()
        {
            try
            {
                var json = JsonSerializer.Serialize(_games, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Printer.PrintError("Failed to save game history: " + ex.Message);
            }
        }

        private void LoadFromFile()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    _games = JsonSerializer.Deserialize<List<GameResult>>(json) ?? new List<GameResult>();
                }
            }
            catch (Exception ex)
            {
                Printer.PrintError("Failed to load game history: " + ex.Message);
            }
        }
    }
}
