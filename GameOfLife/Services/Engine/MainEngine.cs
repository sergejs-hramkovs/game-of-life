using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Views;

namespace Services.Engine
{
    /// <summary>
    /// The Engine class starts, runs and restarts the game.
    /// </summary>
    [Serializable]
    public class MainEngine : IMainEngine
    {
        private readonly IAuxiliaryEngine _auxiliaryEngine;
        private readonly IMenuNavigator _menuNavigator;

        public MultipleGamesModel MultipleGames { get; set; } = new MultipleGamesModel();
        public bool ReadGeneration { get; set; }
        public bool GliderGunMode { get; set; }
        public bool MultipleGamesMode { get; set; }
        public bool SavedGameLoaded { get; set; }
        public bool InitializationFinished { get; set; }
        public int GliderGunType { get; set; }
        public int Delay { get; set; } = 1000;
        public bool GameOver { get; set; }

        public MainEngine(
            IMenuNavigator menuNavigator,
            IAuxiliaryEngine auxiliaryEngine)
        {
            _menuNavigator = menuNavigator;
            _auxiliaryEngine = auxiliaryEngine;
        }

        /// <summary>
        /// Method to perform object injections and to prepare the console window during the first launch of the game.
        /// </summary>
        private void InitializeParameters()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Method to start first launch initializations, menu rendering/processing and the main game process.
        /// </summary>
        /// <param name="firstLaunch">Parameter that shows if it is the first time launching the game or not.</param>
        public void StartGame(bool firstLaunch = true)
        {
            Console.Clear();
            MultipleGames = new MultipleGamesModel();
            if (firstLaunch)
            {
                InitializeParameters();
            }

            _menuNavigator.NavigateMenu(MenuViews.MainMenu);
            RunGame();
        }

        /// <summary>
        /// Method that performs runtime processes of the game.
        /// </summary>
        public void RunGame()
        {
            ConsoleKey runTimeKeyPress;
            bool firstTimeRendering = true;
            GameOver = false;
            Console.Clear();
            InitializationFinished = true;
            do
            {
                while (!Console.KeyAvailable)
                {
                    if (firstTimeRendering) // To load proper state, when loading from a file.
                    {
                        _auxiliaryEngine.CreateRuntimeView();
                        firstTimeRendering = false;
                        Thread.Sleep(Delay);
                    }

                    _auxiliaryEngine.PerformRuntimeCalculations();
                    _auxiliaryEngine.CreateRuntimeView();
                    if (!MultipleGamesMode && MultipleGames.ListOfGames[0].AliveCellsNumber == 0)
                    {
                        GameOver = true;
                        break;
                    }

                    Thread.Sleep(Delay);
                }

                if (GameOver)
                {
                    break;
                }

                //runTimeKeyPress = _inputController.ReadKeyRuntime(MultipleGamesMode); // Checks for Spacebar or Arrows presses.
            } while (true/*runTimeKeyPress != ConsoleKey.Escape*/);

            _menuNavigator.NavigateExitMenu(GameOver);
        }

        /// <summary>
        /// Method to restart the game without rerunning the application.
        /// </summary>
        public void RestartGame()
        {
            GliderGunMode = false;
            InitializationFinished = false;
            if (MultipleGamesMode)
            {
                MultipleGames.GamesToBeDisplayed.Clear();
                MultipleGamesMode = false;
                MultipleGames.ListOfGames.Clear();
                MultipleGames.AliveFields.Clear();
            }

            SavedGameLoaded = false;
            Delay = 1000;
            Console.Clear();
            StartGame(false);
        }
    }
}