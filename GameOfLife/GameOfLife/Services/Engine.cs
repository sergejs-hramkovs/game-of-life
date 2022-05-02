using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    public class Engine : IEngine
    {
        private int _delay = 1000;
        private int _gliderGunType = 0;
        private int _numberOfFieldsAlive;
        private int _totalCellsAlive;
        private bool _readGeneration = false;
        private bool _gliderGunMode = false;
        private bool _multipleGamesMode = false;
        private bool _gameOver = false;
        private List<int> _gamesToBeDisplayed = new();
        private List<GameFieldModel> _listOfGames = new();
        private List<int> _deadFields = new();
        private GameFieldModel _gameField;
        private IFileIO _file;
        private IRender _render;
        private IFieldOperations _fieldOperations;
        private ILibrary _library;
        private IRulesApplier _rulesApplier;
        private IEngine _engine;
        private IInputController _inputController;
        public bool ReadGeneration
        {
            get => _readGeneration;
            set => _readGeneration = value;
        }
        public bool GliderGunMode
        {
            get => _gliderGunMode;
            set => _gliderGunMode = value;
        }
        public bool MultipleGamesMode
        {
            get => _multipleGamesMode;
            set => _multipleGamesMode = value;
        }
        public int GliderGunType
        {
            get => _gliderGunType;
            set => _gliderGunType = value;
        }
        public int Delay
        {
            get => _delay;
            set => _delay = value;
        }
        public List<int> GamesToBeDisplayed
        {
            get => _gamesToBeDisplayed;
            set => _gamesToBeDisplayed = value;
        }
        public List<GameFieldModel> ListOfGames
        {
            get => _listOfGames;
            set => _listOfGames = value;
        }

        public void Injection(IRender render, IFileIO file, IFieldOperations operations, ILibrary library, IRulesApplier rulesApplier,
            IEngine engine, IInputController inputController)
        {
            _render = render;
            _file = file;
            _fieldOperations = operations;
            _library = library;
            _rulesApplier = rulesApplier;
            _engine = engine;
            _inputController = inputController;
        }

        /// <summary>
        /// Initiate field size choice.
        /// </summary>
        public void StartGame(bool firstLaunch = true)
        {
            ConsoleKey fieldSizeChoice;

            if (firstLaunch)
            {
                _inputController.Injection(_engine, _file, _render, _fieldOperations, _library);
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
            _fieldOperations.PopulateField(_gameField, _gliderGunMode, _gliderGunType);
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
                _rulesApplier.DetermineCellsDestiny(_gameField, GliderGunMode);
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
            MultipleGamesMode = false;
            GamesToBeDisplayed.Clear();
            Delay = 1000;
            Console.Clear();
            StartGame(false);
            RunGame();
        }

        /// <summary>
        /// Method to count the current number of alive cells on the field.
        /// </summary>
        public int CountAliveCells(GameFieldModel gameField)
        {
            gameField.AliveCellsNumber = 0;

            for (int i = 0; i < gameField.Length; i++)
            {
                for (int j = 0; j < gameField.Width; j++)
                {
                    if (gameField.GameField[i, j] == AliveCellSymbol)
                    {
                        gameField.AliveCellsNumber++;
                    }
                }
            }
            return gameField.AliveCellsNumber;
        }

        /// <summary>
        /// Method to count total alive cells number across all the fields in the multiple games mode.
        /// </summary>
        private void CountTotalAliveCells()
        {
            _totalCellsAlive = 0;

            foreach (var field in ListOfGames)
            {
                _gameField = field;
                CountAliveCells(_gameField);
                _totalCellsAlive += _gameField.AliveCellsNumber;
            }
        }

        /// <summary>
        /// Method to run and display multiple games simultaneously.
        /// </summary>
        private void RunMultipleGames()
        {
            ConsoleKey runTimeKeyPress;
            ConsoleKey numberChoice;

            MultipleGameFieldsInitialization();
            _render.MultipleGamesMenuRender();
            numberChoice = Console.ReadKey(true).Key;
            _inputController.CheckInputMultipleGamesMenu(numberChoice);
            Console.Clear();

            do
            {
                while (Console.KeyAvailable == false)
                {
                    CountTotalAliveCells();
                    _render.MultipleGamesModeUIRender(Delay, _gameField.Generation, _numberOfFieldsAlive, _totalCellsAlive);
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
        /// Method to perform necessary runtime calculations in the multiple games mode.
        /// </summary>
        private void MultipleGamesModeRuntimeCalculations()
        {
            for (int i = 0; i < ListOfGames.Count; i++)
            {
                _gameField = ListOfGames[i];
                _rulesApplier.DetermineCellsDestiny(_gameField, GliderGunMode);
                _rulesApplier.FieldRefresh(_gameField);
                CountAliveCells(_gameField);
                _gameField.Generation++;
                if (_gameField.AliveCellsNumber > 0)
                {
                    ListOfGames[i] = _gameField;
                }
                else if (!_deadFields.Contains(i))
                {
                    _numberOfFieldsAlive--;
                    _deadFields.Add(i);
                }
            }
        }

        /// <summary>
        /// Method to create a list of the game fields and to seed them automatically and randomly.
        /// </summary>
        private void MultipleGameFieldsInitialization()
        {
            for (int i = 0; i < 1000; i++)
            {
                ListOfGames.Add(new(10, 10));
                ListOfGames[i] = _fieldOperations.RandomSeeding(ListOfGames[i]);
            }
            _numberOfFieldsAlive = ListOfGames.Count;
        }

        /// <summary>
        /// Method to check for the fields where all the cells are dead and to remove them from rendering.
        /// </summary>
        private void FilterDeadFields()
        {
            Random random = new();

            for (int i = 0; i < GamesToBeDisplayed.Count; i++)
            {
                if (CountAliveCells(ListOfGames[GamesToBeDisplayed[i]]) == 0)
                {
                    Console.WriteLine(FieldDeadPhrase);
                    _render.RenderField(ListOfGames[GamesToBeDisplayed[i]], true);
                    GamesToBeDisplayed[i] = random.Next(0, ListOfGames.Count);
                }
                else
                {
                    _render.MultipleGamesModeGameTitleRender(GamesToBeDisplayed[i], ListOfGames[GamesToBeDisplayed[i]].AliveCellsNumber);
                    _render.RenderField(ListOfGames[GamesToBeDisplayed[i]]);
                }
            }
        }
    }
}
