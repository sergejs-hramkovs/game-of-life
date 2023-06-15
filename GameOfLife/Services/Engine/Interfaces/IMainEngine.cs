using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// The Engine class starts, runs and restarts the game.
    /// </summary>
    public interface IMainEngine
    {
        bool ReadGeneration { get; set; }

        bool GliderGunMode { get; set; }

        bool MultipleGamesMode { get; set; }

        bool SavedGameLoaded { get; set; }

        bool InitializationFinished { get; set; }

        bool GameOver { get; set; }

        int GliderGunType { get; set; }

        int Delay { get; set; }

        MultipleGamesModel MultipleGames { get; set; }

        /// <summary>
        /// Method to start first launch initializations, menu rendering/processing and the main game process.
        /// </summary>
        /// <param name="firstLaunch">Parameter that shows if it is the first time launching the game or not, 'true' by default.</param>
        void StartGame(bool firstLaunch = true);

        /// <summary>
        /// Method that performs runtime processes of the game.
        /// </summary>
        void RunGame();

        /// <summary>
        /// Method to restart the game without rerunning the application.
        /// </summary>
        void RestartGame();
    }
}
