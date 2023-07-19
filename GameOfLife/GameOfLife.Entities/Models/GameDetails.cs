using GameOfLife.Entities.Enums;

namespace GameOfLife.Entities.Models
{
    public class GameDetails
    {
        public bool ReadGeneration { get; set; }
        public bool IsGliderGunMode { get; set; }
        public bool IsMultipleGamesMode { get; set; }
        public bool IsSavedGameLoaded { get; set; }
        public bool InitializationFinished { get; set; }
        public int GliderGunType { get; set; }
        public int Delay { get; set; } = 1000;
        public bool IsGameOver { get; set; }
        public bool InvalidMenuInput { get; set; }
        public MenuType MenuType { get; set; }
    }
}
