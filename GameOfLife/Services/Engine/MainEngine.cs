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
            var inputDetails = _game.InputDetails;

            if (firstLaunch)
            {
                Console.CursorVisible = false;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            // Move this somewhere later.
            do
            {
                _renderingService.RenderMenu(MenuViews.MainMenu, wrongInput: inputDetails.WrongInput, clearScreen: true, noSavedGames: false);
                _inputProcessorService.HandleInputMainMenu(_game);
            } while (inputDetails.WrongInput);

            RunGame();
        }

        public void RunGame()
        {
            Console.Clear();

            ConsoleKey runTimeKeyPress;
            var firstTimeRendering = true;
            var gameDetails = _game.GameDetails;
            gameDetails.IsGameOver = false;
            gameDetails.InitializationFinished = true;

            do
            {
                while (!Console.KeyAvailable)
                {
                    if (firstTimeRendering)
                    {
                        _userInterfaceService.CreateRuntimeView(_game);
                        firstTimeRendering = false;
                        Thread.Sleep(gameDetails.Delay);
                    }

                    _gameFieldService.PerformRuntimeCalculations(_game);
                    _userInterfaceService.CreateRuntimeView(_game);
                    if (!gameDetails.IsMultipleGamesMode && _game.MultipleGamesField.ListOfGames[0].AliveCellsNumber == 0)
                    {
                        gameDetails.IsGameOver = true;
                        break;
                    }

                    Thread.Sleep(gameDetails.Delay);
                }

                if (gameDetails.IsGameOver)
                {
                    break;
                }

                runTimeKeyPress = _inputProcessorService.ProcessRuntimeKeypress(_game);
            } while (runTimeKeyPress != ConsoleKey.Escape);

            // Move this somewhere later.
            _renderingService.RenderMenu(MenuViews.ExitMenu, clearScreen: false, gameOver: gameDetails.IsGameOver);
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