using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Views;

namespace GameOfLife
{
    /// <summary>
    /// The Engine class starts, runs and restarts the game.
    /// </summary>
    [Serializable]
    public class MainEngine : IMainEngine
    {
        private IFileIO _file;
        private IRenderer _renderer;
        private IFieldOperations _fieldOperations;
        private ILibrary _library;
        private IRulesApplier _rulesApplier;
        private IInputController _inputController;
        private IUserInterfaceFiller _userInterfaceFiller;
        private IAuxiliaryEngine _auxiliaryEngine;
        private IMenuNavigator _menuNavigator;
        public MultipleGamesModel MultipleGames { get; set; }
        public bool ReadGeneration { get; set; }
        public bool GliderGunMode { get; set; }
        public bool MultipleGamesMode { get; set; }
        public bool SavedGameLoaded { get; set; }
        public bool InitializationFinished { get; set; }
        public int GliderGunType { get; set; }
        public int Delay { get; set; } = 1000;
        public bool GameOver { get; set; }

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
        public void Inject(IRenderer render, IFileIO file, IFieldOperations operations, ILibrary library,
            IRulesApplier rulesApplier, IInputController inputController, IUserInterfaceFiller userInterfaceFiller,
            IAuxiliaryEngine auxiliaryEngine, IMenuNavigator menuNavigator)
        {
            _renderer = render;
            _file = file;
            _fieldOperations = operations;
            _library = library;
            _rulesApplier = rulesApplier;
            _inputController = inputController;
            _userInterfaceFiller = userInterfaceFiller;
            _auxiliaryEngine = auxiliaryEngine;
            _menuNavigator = menuNavigator;
        }

        /// <summary>
        /// Method to perform object injections and to prepare the console window during the first launch of the game.
        /// </summary>
        private void InitializeParameters()
        {
            _inputController.Inject(this, _userInterfaceFiller, _file, _renderer, _fieldOperations, _library, _menuNavigator);
            _file.Inject(_inputController, this, _menuNavigator);
            _menuNavigator.Inject(_renderer, _inputController, this, _fieldOperations, _file, _userInterfaceFiller);
            _auxiliaryEngine.Inject(this, _rulesApplier, _renderer, _userInterfaceFiller);
            _renderer.Inject(this);
            Console.CursorVisible = false;
            Console.SetWindowSize(175, 61);
        }

        /// <summary>
        /// Method to start first launch initializations, menu rendering/processing and the main game process.
        /// </summary>
        /// <param name="firstLaunch">Parameter that shows if it is the first time launching the game or not.</param>
        public void StartGame(bool firstLaunch = true)
        {
            Console.Clear();
            MultipleGames = new();
            if (firstLaunch)
            {
                InitializeParameters();
            }

            _menuNavigator.NavigateMenu(MenuViews.MainMenu, _inputController.HandleInputMainMenu);
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

                runTimeKeyPress = _inputController.ReadKeyRuntime(MultipleGamesMode); // Checks for Spacebar or Arrows presses.
            } while (runTimeKeyPress != ConsoleKey.Escape);

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