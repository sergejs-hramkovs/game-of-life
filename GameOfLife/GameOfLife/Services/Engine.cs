using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Views;

namespace GameOfLife
{
    /// <summary>
    /// The Engine class deals with different calculations that occur before and during the runtime,
    /// except rendering and applying the rules of the game.
    /// </summary>
    [Serializable]
    public class Engine : IEngine
    {
        private GameFieldModel _gameField;
        private IFileIO _file;
        private IRenderer _render;
        private IFieldOperations _fieldOperations;
        private ILibrary _library;
        private IRulesApplier _rulesApplier;
        private IInputController _inputController;
        private IUserInterfaceFiller _userInterfaceFiller;
        private bool _gameOver;
        public MultipleGamesModel MultipleGames { get; set; }
        public bool ReadGeneration { get; set; }
        public bool GliderGunMode { get; set; }
        public bool MultipleGamesMode { get; set; }
        public bool MultipleGamesLoaded { get; set; }
        public int GliderGunType { get; set; }
        public int Delay { get; set; } = 1000;

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
        public void Injection(IRenderer render, IFileIO file, IFieldOperations operations, ILibrary library,
            IRulesApplier rulesApplier, IInputController inputController, IUserInterfaceFiller userInterfaceFiller)
        {
            _render = render;
            _file = file;
            _fieldOperations = operations;
            _library = library;
            _rulesApplier = rulesApplier;
            _inputController = inputController;
            _userInterfaceFiller = userInterfaceFiller;
        }

        /// <summary>
        /// Method to start the game.
        /// </summary>
        public void StartGame(bool firstLaunch = true)
        {
            ConsoleKey fieldSizeChoice;
            if (firstLaunch)
            {
                FirstLaunchInitialization();
            }

            while (true)
            {
                if (!GliderGunMode && !MultipleGamesMode)
                {
                    _render.MenuRenderer(MenuViews.MainMenu, _inputController.WrongInput, _file.FileReadingError, _file.NoSavedGames);
                    _inputController.WrongInput = false;
                    _file.NoSavedGames = false;
                    _file.FileReadingError = false;
                    fieldSizeChoice = Console.ReadKey(true).Key;
                    _gameField = _inputController.CheckInputMainMenu(fieldSizeChoice);
                    if (_inputController.CorrectKeyPressed && !MultipleGamesMode && !GliderGunMode)
                    {
                        _inputController.CorrectKeyPressed = false;
                        RunGame();
                        break;
                    }
                    else if (fieldSizeChoice != ConsoleKey.G && fieldSizeChoice != ConsoleKey.F1 && fieldSizeChoice != ConsoleKey.M &&
                        fieldSizeChoice != ConsoleKey.L)
                    {
                        _inputController.WrongInput = true;
                    }
                }
                else if (GliderGunMode)
                {
                    _render.MenuRenderer(MenuViews.GliderGunModeMenu, _inputController.WrongInput);
                    fieldSizeChoice = Console.ReadKey(true).Key;
                    _gameField = _inputController.CheckInputGliderGunMenu(fieldSizeChoice);
                    if (fieldSizeChoice == ConsoleKey.D1 || fieldSizeChoice == ConsoleKey.D2)
                    {
                        RunGame();
                        break;
                    }
                }
                else if (MultipleGamesMode && !_inputController.WrongInput)
                {
                    RunMultipleGames();
                    break;
                }
            }
        }

        /// <summary>
        /// Main process of the game.
        /// </summary>
        public void RunGame()
        {
            ConsoleKey runTimeKeyPress;
            if (!_file.FileLoaded && !MultipleGamesMode)
            {
                FirstRenderCalculations();
            }
            else
            {
                Console.Clear(); // To clear the screen after loading from a file. SetCursorPosition leaves a bit of UI.
            }

            _file.FileLoaded = false; // To reset the fact of previous loading to avoid disruption of the game after restart.
            do
            {
                while (!Console.KeyAvailable)
                {
                    RuntimeCalculations();
                    if (_gameOver)
                    {
                        break;
                    }

                    Thread.Sleep(Delay);
                }

                if (!_gameOver)
                {
                    runTimeKeyPress = _inputController.RuntimeKeyReader(); // Checks for Spacebar or Arrows presses.
                }
                else
                {
                    _gameOver = false;
                    break;
                }
            } while (runTimeKeyPress != ConsoleKey.Escape);

            _render.MenuRenderer(MenuViews.ExitMenu, clearScreen: false);
            do
            {
                runTimeKeyPress = Console.ReadKey(true).Key;
                _inputController.CheckInputExitMenu(runTimeKeyPress);
            } while (runTimeKeyPress != ConsoleKey.Escape || runTimeKeyPress != ConsoleKey.R);
        }

        /// <summary>
        /// Method for calling necessary methods for the first rendering.
        /// </summary>
        private void FirstRenderCalculations()
        {
            Console.Clear();
            _render.RenderField(_gameField);
            _fieldOperations.PopulateField(_gameField, GliderGunMode, GliderGunType);
            _render.MenuRenderer(MenuViews.BlankUI);
            _render.RenderField(_gameField);
            Thread.Sleep(2000);
            Console.Clear();
        }

        /// <summary>
        /// Method for calling methods that take care of all the necessary calculations during the runtime.
        /// </summary>
        private void RuntimeCalculations()
        {
            int generationsAfterLoading = 1; // Parameter for proper loading from file.
            Console.SetCursorPosition(0, 0);
            if (ReadGeneration) // ReadGeneration ensures loading of a proper generation number when loading from the file.
            {
                generationsAfterLoading = 0;
                ReadGeneration = false;
            }

            if (generationsAfterLoading >= 1) // Helps to avoid loading of the previous or next game state.
            {
                _rulesApplier.IterateThroughGameFieldCells(_gameField, GliderGunMode);
                _rulesApplier.FieldRefresh(_gameField);
                CountAliveCells(_gameField);
            }
            if (generationsAfterLoading == 0)
            {
                generationsAfterLoading++;
                CountAliveCells(_gameField);
            }

            if (_gameField.AliveCellsNumber == 0)
            {
                _userInterfaceFiller.GameOverUICreation(_gameField.Generation);
                _render.MenuRenderer(MenuViews.GameOverUI);
                _gameOver = true;
            }
            else
            {
                _userInterfaceFiller.SingleGameRuntimeUICreation(_gameField, Delay);
                _render.MenuRenderer(MenuViews.SingleGameUI, clearScreen: false);
                _gameField.Generation++;
            }

            if (_gameOver)
            {
                _render.RenderField(_gameField, true);
            }
            else
            {
                _render.RenderField(_gameField);
            }
        }

        /// <summary>
        /// Method to restart the game without rerunning the application.
        /// </summary>
        public void RestartGame()
        {
            GliderGunMode = false;
            if (MultipleGamesMode)
            {
                MultipleGames.GamesToBeDisplayed.Clear();
                MultipleGamesMode = false;
                _inputController.MultipleGames.ListOfGames = null;
                MultipleGames.ListOfGames = null;
            }

            MultipleGamesLoaded = false;
            Delay = 1000;
            Console.Clear();
            StartGame(false);
        }

        /// <summary>
        /// Method to count the current number of alive cells on the field.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <returns>Returns the number of alive cells on the field.</returns>
        public int CountAliveCells(GameFieldModel gameField)
        {
            gameField.AliveCellsNumber = 0;
            for (int xCoordinate = 0; xCoordinate < gameField.Length; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < gameField.Width; yCoordinate++)
                {
                    if (gameField.GameField[xCoordinate, yCoordinate] == StringConstants.AliveCellSymbol)
                    {
                        gameField.AliveCellsNumber++;
                    }
                }
            }

            return gameField.AliveCellsNumber;
        }

        /// <summary>
        /// Method to count total alive cells number across all the fields in the Multiple Games Mode.
        /// </summary>
        private void CountTotalAliveCells()
        {
            MultipleGames.TotalCellsAlive = 0;
            foreach (var field in MultipleGames.ListOfGames)
            {
                _gameField = field;
                CountAliveCells(_gameField);
                MultipleGames.TotalCellsAlive += _gameField.AliveCellsNumber;
            }
        }

        /// <summary>
        /// Main process of the game in the Multiple Games Mode.
        /// </summary>
        private void RunMultipleGames()
        {
            ConsoleKey runTimeKeyPress;
            MultipleGamesModeInitialCalculations();
            do
            {
                MultipleGamesModeCoreLoop();
                runTimeKeyPress = _inputController.RuntimeKeyReader(multipleGamesMode: true);
            } while (runTimeKeyPress != ConsoleKey.Escape);

            _render.MenuRenderer(MenuViews.ExitMenu, clearScreen: false);
            do
            {
                runTimeKeyPress = Console.ReadKey(true).Key;
                _inputController.CheckInputExitMenu(runTimeKeyPress);
            } while (runTimeKeyPress != ConsoleKey.Escape || runTimeKeyPress != ConsoleKey.R);
        }

        /// <summary>
        /// Method to perform all the necessary calculations and preparations upon starting the Multiple Games Mode.
        /// </summary>
        private void MultipleGamesModeInitialCalculations()
        {
            ConsoleKey numberChoice;
            ConsoleKey fieldsSizeChoice;
            if (!MultipleGamesLoaded)
            {
                MultipleGames = new();
                _render.MenuRenderer(MenuViews.MultipleGamesModeGamesQuantityMenu, clearScreen: true, newLine: false);
                _inputController.EnterNumberOfMultipleGames(MultipleGames);
                MultipleGames = _inputController.MultipleGames;
                _render.MenuRenderer(MenuViews.MultipleGamesModeFieldSizeChoiceMenu, clearScreen: true);
                fieldsSizeChoice = Console.ReadKey(true).Key;
                _inputController.CheckInputMultipleGamesMenuFieldSize(MultipleGames, fieldsSizeChoice);
                MultipleGames.InitializeGames(_fieldOperations);
                MultipleGames = _inputController.MultipleGames;
                _render.MenuRenderer(MenuViews.MultipleGamesModeMenu, clearScreen: true);
                while (true)
                {
                    numberChoice = Console.ReadKey(true).Key;
                    if (_inputController.CheckInputMultipleGamesMenu(numberChoice))
                    {
                        break;
                    }
                }
            }
            else
            {
                MultipleGames = _inputController.MultipleGames;
            }

            Console.Clear();
        }

        /// <summary>
        /// Method to perform necessary runtime calculations in the Multiple Games Mode.
        /// </summary>
        private void MultipleGamesModeRuntimeCalculations()
        {
            for (int gameNumber = 0; gameNumber < MultipleGames.TotalNumberOfGames; gameNumber++)
            {
                _gameField = MultipleGames.ListOfGames[gameNumber];
                _rulesApplier.IterateThroughGameFieldCells(_gameField);
                _rulesApplier.FieldRefresh(_gameField);
                CountAliveCells(_gameField);
                if (_gameField.AliveCellsNumber > 0)
                {
                    MultipleGames.ListOfGames[gameNumber] = _gameField;
                }
                else if (!MultipleGames.DeadFields.Contains(gameNumber))
                {
                    MultipleGames.NumberOfFieldsAlive--;
                    MultipleGames.DeadFields.Add(gameNumber);
                }
            }
        }

        /// <summary>
        /// Method to replace rendered dead fields with alive ones in the list of fields to be displayed.
        /// </summary>
        private void RemoveDeadFieldsFromRendering()
        {
            Random random = new();
            for (int rowNumber = 0; rowNumber < MultipleGames.NumberOfRows; rowNumber++)
            {
                for (int i = rowNumber * MultipleGames.NumberOfHorizontalFields; i < MultipleGames.NumberOfHorizontalFields + rowNumber * MultipleGames.NumberOfHorizontalFields; i++)
                {
                    if (MultipleGames.ListOfGames[MultipleGames.GamesToBeDisplayed[i]].AliveCellsNumber == 0)
                    {
                        MultipleGames.GamesToBeDisplayed[i] = random.Next(0, MultipleGames.TotalNumberOfGames);
                    }
                }
            }
        }

        /// <summary>
        /// The very runtime core in the Multiple Games Mode.
        /// </summary>
        private void MultipleGamesModeCoreLoop()
        {
            while (!Console.KeyAvailable)
            {
                Console.SetCursorPosition(0, 0);
                CountTotalAliveCells();
                _userInterfaceFiller.MultiGameRuntimeUICreation(Delay, MultipleGames);
                _render.MenuRenderer(MenuViews.MultiGameUI, clearScreen: false);
                MultipleGames.Generation++;
                _render.RenderGridOfFields(MultipleGames);
                RemoveDeadFieldsFromRendering();
                MultipleGamesModeRuntimeCalculations();
                Thread.Sleep(Delay);
            }
        }

        /// <summary>
        /// Method to perform object injections and to prepare the console window during the first launch of the games. 
        /// </summary>
        private void FirstLaunchInitialization()
        {
            _inputController.Injection(this, _userInterfaceFiller, _file, _render, _fieldOperations, _library);
            _file.Injection(_render, _inputController, this, _userInterfaceFiller);
            Console.CursorVisible = false;
            Console.SetWindowSize(175, 61);
        }
    }
}