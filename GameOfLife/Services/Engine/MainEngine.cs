using GameOfLife.Entities.Models;
using GameOfLife.Interfaces;
using GameOfLife.Views;

namespace Services.Engine
{
    [Serializable]
    public class MainEngine : IMainEngine
    {
        private readonly IMenuNavigator _menuNavigator;
        private readonly IInputProcessorService _inputProcessorService;
        private readonly IGameFieldService _gameFieldService;
        private readonly IUIService _userInterfaceService;
        private GameModel _game;

        public MainEngine(
            IMenuNavigator menuNavigator,
            IInputProcessorService inputProcessorService,
            IGameFieldService gameFieldService,
            IUIService userInterfaceService)
        {
            _menuNavigator = menuNavigator;
            _inputProcessorService = inputProcessorService;
            _game = new GameModel();
            _gameFieldService = gameFieldService;
            _userInterfaceService = userInterfaceService;
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

            _menuNavigator.NavigateMenu(MenuViews.MainMenu);
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

            _menuNavigator.NavigateExitMenu(_game.GameDetails.IsGameOver);
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