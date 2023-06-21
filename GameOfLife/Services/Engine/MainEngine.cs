using GameOfLife.Entities.Models;
using GameOfLife.Interfaces;
using GameOfLife.Views;

namespace Services.Engine
{
    [Serializable]
    public class MainEngine : IMainEngine
    {
        private readonly IInputProcessorService _inputProcessorService;
        private readonly IGameFieldService _gameFieldService;
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly IRenderingService _renderingService;
        private GameModel _game;

        public MainEngine(
            IInputProcessorService inputProcessorService,
            IGameFieldService gameFieldService,
            IUserInterfaceService userInterfaceService,
            IRenderingService renderingService)
        {
            _inputProcessorService = inputProcessorService;
            _gameFieldService = gameFieldService;
            _userInterfaceService = userInterfaceService;
            _renderingService = renderingService;
            _game = new GameModel();
        }

        public void StartGame(bool firstLaunch = true)
        {
            Console.Clear();
            _game = new GameModel();

            if (firstLaunch)
            {
                Console.CursorVisible = false;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            // Move this somewhere later.
            do
            {
                _renderingService.RenderMenu(MenuViews.MainMenu, wrongInput: _inputProcessorService.WrongInput, clearScreen: true, noSavedGames: false);
                _inputProcessorService.HandleInputMainMenu(_game);
            } while (_inputProcessorService.WrongInput);

            RunGame();
        }

        public void RunGame()
        {
            Console.Clear();

            ConsoleKey runTimeKeyPress;
            var firstTimeRendering = true;
            _game.GameDetails.IsGameOver = false;
            _game.GameDetails.InitializationFinished = true;

            do
            {
                while (!Console.KeyAvailable)
                {
                    if (firstTimeRendering)
                    {
                        _userInterfaceService.CreateRuntimeView(_game);
                        firstTimeRendering = false;
                        Thread.Sleep(_game.GameDetails.Delay);
                    }

                    _gameFieldService.PerformRuntimeCalculations(_game);
                    _userInterfaceService.CreateRuntimeView(_game);
                    if (!_game.GameDetails.IsMultipleGamesMode && _game.MultipleGamesField.ListOfGames[0].AliveCellsNumber == 0)
                    {
                        _game.GameDetails.IsGameOver = true;
                        break;
                    }

                    Thread.Sleep(_game.GameDetails.Delay);
                }

                if (_game.GameDetails.IsGameOver)
                {
                    break;
                }

                runTimeKeyPress = _inputProcessorService.ProcessRuntimeKeypress(_game);
            } while (runTimeKeyPress != ConsoleKey.Escape);

            // Move this somewhere later.
            _renderingService.RenderMenu(MenuViews.ExitMenu, clearScreen: false, gameOver: _game.GameDetails.IsGameOver);
            do
            {
                runTimeKeyPress = Console.ReadKey(true).Key;
                _inputProcessorService.HandleInputExitMenu(runTimeKeyPress);
            } while (runTimeKeyPress != ConsoleKey.Escape || runTimeKeyPress != ConsoleKey.R);
        }

        //public void RestartGame()
        //{
        //    GliderGunMode = false;
        //    InitializationFinished = false;
        //    if (MultipleGamesMode)
        //    {
        //        MultipleGames.GamesToBeDisplayed.Clear();
        //        MultipleGamesMode = false;
        //        MultipleGames.ListOfGames.Clear();
        //        MultipleGames.AliveFields.Clear();
        //    }

        //    SavedGameLoaded = false;
        //    Delay = 1000;
        //    Console.Clear();
        //    StartGame(false);
        //}
    }
}