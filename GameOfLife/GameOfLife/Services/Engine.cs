using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    /// <summary>
    /// The Engine class deals with different calculations that occur before and during the runtime,
    /// except rendering and applying the rules of the game.
    /// </summary>
    public class Engine : IEngine
    {
        private GameFieldModel _gameField;
        private MultipleGamesModel _multipleGames;
        private IFileIO _file;
        private IRender _render;
        private IFieldOperations _fieldOperations;
        private ILibrary _library;
        private IRulesApplier _rulesApplier;
        private IInputController _inputController;
        private bool _gameOver;
        public bool ReadGeneration { get; set; }
        public bool GliderGunMode { get; set; }
        public bool MultipleGamesMode { get; set; }
        public int GliderGunType { get; set; } = 0;
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
        public void Injection(IRender render, IFileIO file, IFieldOperations operations, ILibrary library,
            IRulesApplier rulesApplier, IInputController inputController)
        {
            _render = render;
            _file = file;
            _fieldOperations = operations;
            _library = library;
            _rulesApplier = rulesApplier;
            _inputController = inputController;
        }

        /// <summary>
        /// Method to start the game.
        /// </summary>
        public void StartGame(bool firstLaunch = true)
        {
            ConsoleKey fieldSizeChoice;
            if (firstLaunch)
            {
                _inputController.Injection(this, _file, _render, _fieldOperations, _library);
                _file.Injection(_render, _inputController, this);
                _render.Injection(_file);
                Console.CursorVisible = false;
                Console.SetWindowSize(170, 60);
            }

            while (true)
            {
                if (!GliderGunMode && !MultipleGamesMode)
                {
                    _render.MainMenuRender(_inputController.WrongInput, _file.FileReadingError, _file.NoSavedGames);
                    _inputController.WrongInput = false;
                    _file.NoSavedGames = false;
                    _file.FileReadingError = false;
                    fieldSizeChoice = Console.ReadKey(true).Key;
                    _gameField = _inputController.CheckInputMainMenu(fieldSizeChoice);
                    if (_inputController.CorrectKeyPressed && !MultipleGamesMode)
                    {
                        _inputController.CorrectKeyPressed = false;
                        RunGame();
                        break;
                    }
                    else if (fieldSizeChoice != ConsoleKey.G && fieldSizeChoice != ConsoleKey.F1 && fieldSizeChoice != ConsoleKey.M)
                    {
                        _inputController.WrongInput = true;
                    }
                }
                else if (GliderGunMode)
                {
                    _render.GliderGunModeRender(_inputController.WrongInput);
                    fieldSizeChoice = Console.ReadKey(true).Key;
                    _gameField = _inputController.CheckInputGliderGunMenu(fieldSizeChoice);
                    if (fieldSizeChoice == ConsoleKey.D1 || fieldSizeChoice == ConsoleKey.D2)
                    {
                        RunGame();
                        break;
                    }
                }
                else if (MultipleGamesMode)
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
                Console.Clear();
            }

            // To reset the fact of previous loading to avoid disruption of the game after restart.
            _file.FileLoaded = false;
            do
            {
                while (Console.KeyAvailable == false)
                {
                    Console.SetCursorPosition(0, 0);
                    RuntimeCalculations();
                    if (_gameOver)
                    {
                        break;
                    }

                    ReadGeneration = false;
                    Thread.Sleep(Delay);
                }

                if (!_gameOver)
                {
                    runTimeKeyPress = Console.ReadKey(true).Key;
                    _inputController.PauseGame(runTimeKeyPress);
                    _inputController.ChangeDelay(runTimeKeyPress);
                }
                else
                {
                    _gameOver = false;
                    break;
                }
            } while (runTimeKeyPress != ConsoleKey.Escape);

            _render.ExitMenuRender();
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
            Console.Clear();
            _render.BlankUIRender();
            _render.RenderField(_gameField);
            Thread.Sleep(2000);
            Console.Clear();
        }

        /// <summary>
        /// Method for calling methods that take care of all the necessary calculations during the runtime.
        /// </summary>
        private void RuntimeCalculations()
        {
            // Parameter for proper loading from file.
            int generationsAfterLoading = 1;
            // ReadGeneration ensures loading of a proper generation number when loading from the file.
            if (ReadGeneration)
            {
                generationsAfterLoading = 0;
                ReadGeneration = false;
            }

            // generationsAfterLoading ensure proper game field rendering.
            // Helps to avoid loading of the previous or next game state.
            if (generationsAfterLoading >= 1)
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
                _render.GameOverRender(_gameField.Generation);
                _gameOver = true;
            }
            else
            {
                _render.RuntimeUIRender(_gameField, Delay);
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
                _multipleGames.GamesToBeDisplayed.Clear();
                MultipleGamesMode = false;
            }

            Delay = 1000;
            Console.Clear();
            StartGame(false);
            RunGame();
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
                    if (gameField.GameField[xCoordinate, yCoordinate] == AliveCellSymbol)
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
            _multipleGames.TotalCellsAlive = 0;
            foreach (var field in _multipleGames.ListOfGames)
            {
                _gameField = field;
                CountAliveCells(_gameField);
                _multipleGames.TotalCellsAlive += _gameField.AliveCellsNumber;
            }
        }

        /// <summary>
        /// Main process of the game in the Multiple Games Mode.
        /// </summary>
        private void RunMultipleGames()
        {
            ConsoleKey runTimeKeyPress;
            ConsoleKey numberChoice;
            _multipleGames = new();
            _inputController.EnterMultipleGamesData(_multipleGames);
            _multipleGames.InitializeGames(_fieldOperations);
            _multipleGames = _inputController.MultipleGames;
            _render.MultipleGamesMenuRender();
            while (true)
            {
                numberChoice = Console.ReadKey(true).Key;
                if (_inputController.CheckInputMultipleGamesMenu(numberChoice))
                {
                    break;
                }
            }

            Console.Clear();
            do
            {
                while (Console.KeyAvailable == false)
                {
                    CountTotalAliveCells();
                    _render.MultipleGamesModeUIRender(Delay, _multipleGames.Generation, _multipleGames.NumberOfFieldsAlive, _multipleGames.TotalCellsAlive);
                    _multipleGames.Generation++;
                    FilterDeadFields();
                    MultipleGamesModeRuntimeCalculations();
                    Thread.Sleep(Delay);
                }

                runTimeKeyPress = Console.ReadKey(true).Key;
                _inputController.PauseGame(runTimeKeyPress, true);
                _inputController.ChangeDelay(runTimeKeyPress);
            } while (runTimeKeyPress != ConsoleKey.Escape);

            _render.ExitMenuRender();
            do
            {
                runTimeKeyPress = Console.ReadKey(true).Key;
                _inputController.CheckInputExitMenu(runTimeKeyPress);
            } while (runTimeKeyPress != ConsoleKey.Escape || runTimeKeyPress != ConsoleKey.R);
        }

        /// <summary>
        /// Method to perform necessary runtime calculations in the Multiple Games Mode.
        /// </summary>
        private void MultipleGamesModeRuntimeCalculations()
        {
            for (int gameNumber = 0; gameNumber < _multipleGames.TotalNumberOfGames; gameNumber++)
            {
                _gameField = _multipleGames.ListOfGames[gameNumber];
                _rulesApplier.IterateThroughGameFieldCells(_gameField, GliderGunMode);
                _rulesApplier.FieldRefresh(_gameField);
                CountAliveCells(_gameField);
                if (_gameField.AliveCellsNumber > 0)
                {
                    _multipleGames.ListOfGames[gameNumber] = _gameField;
                }
                else if (!_multipleGames.DeadFields.Contains(gameNumber))
                {
                    _multipleGames.NumberOfFieldsAlive--;
                    _multipleGames.DeadFields.Add(gameNumber);
                }
            }
        }

        /// <summary>
        /// Method to check for the fields where all the cells are dead and to remove them from rendering.
        /// </summary>
        private void FilterDeadFields()
        {
            Random random = new();
            for (int gameNumber = 0; gameNumber < _multipleGames.NumberOfGamesToBeDisplayed; gameNumber++)
            {
                if (CountAliveCells(_multipleGames.ListOfGames[_multipleGames.GamesToBeDisplayed[gameNumber]]) == 0)
                {
                    Console.WriteLine(FieldDeadPhrase);
                    _render.RenderField(_multipleGames.ListOfGames[_multipleGames.GamesToBeDisplayed[gameNumber]], true);
                    _multipleGames.GamesToBeDisplayed[gameNumber] = random.Next(0, _multipleGames.TotalNumberOfGames);
                }
                else
                {
                    _render.MultipleGamesModeGameTitleRender(_multipleGames.GamesToBeDisplayed[gameNumber], _multipleGames.ListOfGames[_multipleGames.GamesToBeDisplayed[gameNumber]].AliveCellsNumber);
                    _render.RenderField(_multipleGames.ListOfGames[_multipleGames.GamesToBeDisplayed[gameNumber]]);
                }
            }
        }
    }
}
