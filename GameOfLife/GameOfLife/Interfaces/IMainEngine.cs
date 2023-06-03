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
        /// Method to inject objects into the Engine class.
        /// </summary>
        /// <param name="render">An object of the Render class.</param>
        /// <param name="file">An object of the FileIO class.</param>
        /// <param name="operations">An object of the FieldOperations class.</param>
        /// <param name="library">An object of the Library class.</param>
        /// <param name="rulesApplier">An object of the RulesApplier class.</param>
        /// <param name="inputController">An object of the InputController class.</param>
        /// <param name="userInterfaceFiller">An object of the UserInterfaceFiller class.</param>
        void Inject(IRenderer render, IFileIO file, IFieldOperations operations, ILibrary library, IRulesApplier rulesApplier, IInputController inputController,
            IUserInterfaceFiller userInterfaceFiller, IAuxiliaryEngine auxiliaryEngine, IMenuNavigator menuNavigator);

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
