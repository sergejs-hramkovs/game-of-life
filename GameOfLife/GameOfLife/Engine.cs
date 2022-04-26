using GameOfLife.Interfaces;
using static GameOfLife.StringConstants;

namespace GameOfLife
{
    public class Engine : IEngine
    {
        private int _length;
        private int _width;
        private int _delay = 1000;
        private string[,] _gameField;
        private bool _wrongInput = false;
        private bool _loaded = false;
        private bool _readGeneration = false;
        private bool _gliderGunMode = false;
        private bool _resetGeneration = false;
        private bool _correctKeyPressed = false;
        private bool _gameOver = false;
        private int _generation = 1;
        private IFileIO _file;
        private IRender _render;
        private IField _field;
        private ILibrary _library;
        private IRulesApplier _rulesApplier;
        private IEngine _engine;
        private Tuple<string[,], int, int, int> _returnValues;

        /// <summary>
        /// Initiate field size choice.
        /// </summary>
        public void StartGame(IRender render, IFileIO file, IField field, ILibrary library, IRulesApplier rulesApplier, IEngine engine)
        {
            ConsoleKeyInfo fieldSizeChoice;
            _render = render;
            _file = file;
            _field = field;
            _library = library;
            _rulesApplier = rulesApplier;
            _engine = engine;

            Console.CursorVisible = false;
            Console.SetWindowSize(170, 55);

            while (true)
            {
                if (!_gliderGunMode)
                {
                    _render.FieldSizeMenuRender(_wrongInput, _file.FileReadingError);
                    _wrongInput = false;
                    _file.FileReadingError = false;
                    fieldSizeChoice = Console.ReadKey(true);
                    CheckInputMainMenu(fieldSizeChoice);

                    if (_correctKeyPressed)
                    {
                        _correctKeyPressed = false;
                        break;
                    }
                    else
                    {
                        if (fieldSizeChoice.Key != ConsoleKey.G && fieldSizeChoice.Key != ConsoleKey.F1)
                        {
                            _wrongInput = true;
                        }
                    }
                }
                else
                {
                    _render.GliderGunMenuRender();
                    fieldSizeChoice = Console.ReadKey(true);
                    CheckInputGliderGunMenu(fieldSizeChoice);
                    if (fieldSizeChoice.Key == ConsoleKey.D1)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Method to change the time delay if LeftArrow or RightArrow keys are pressed.
        /// </summary>
        /// <param name="timeDelay">Time delay in miliseconds between each generation.</param>
        /// <param name="keyPressed">Parameters which stores Left and Right Arrow key presses.</param>
        /// <returns>Returns changed time delay.</returns>
        private int ChangeDelay(int timeDelay, ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (timeDelay <= 100 && timeDelay > 10)
                    {
                        timeDelay -= 10;
                    }
                    else if (timeDelay > 100)
                    {
                        timeDelay -= 100;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (timeDelay < 2000)
                    {
                        if (timeDelay < 100)
                        {
                            timeDelay += 10;
                        }
                        else
                        {
                            timeDelay += 100;
                        }
                    }
                    break;
            }
            return timeDelay;
        }

        /// <summary>
        /// Method to pause the game by pressing the Spacebar.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores Spacebar key press.</param>
        private void PauseGame(ConsoleKeyInfo keyPressed)
        {
            ConsoleKeyInfo saveKey;

            if (keyPressed.Key == ConsoleKey.Spacebar)
            {
                _render.PauseMenuRender();
                saveKey = Console.ReadKey(true);

                switch (saveKey.Key)
                {
                    case ConsoleKey.S:
                        _file.SaveGameFieldToFile(_returnValues.Item1, _returnValues.Item2, _returnValues.Item3, _returnValues.Item4);
                        Console.WriteLine(SuccessfullySavedPhrase);
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.R:
                        RestartGame();
                        break;

                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        /// <summary>
        /// Main process of the game.
        /// </summary>
        public void RunGame()
        {
            ConsoleKeyInfo cki;

            if (!_loaded)
            {
                _gameField = _field.CreateField(_library, _engine, _rulesApplier, _render, _length, _width);
            }
            InitialCalculations(_gameField, _loaded, _gliderGunMode);
            _loaded = false;

            do
            {
                while (Console.KeyAvailable == false)
                {
                    Console.SetCursorPosition(0, 0);
                    _returnValues = RuntimeCalculations(_delay, _gliderGunMode, _resetGeneration, _readGeneration);
                    if (_gameOver)
                    {
                        break;
                    }
                    _readGeneration = false;
                    if (_resetGeneration)
                    {
                        _resetGeneration = false;
                    }
                    Thread.Sleep(_delay);
                }
                if (!_gameOver)
                {
                    cki = Console.ReadKey(true);
                    PauseGame(cki);
                    _delay = ChangeDelay(_delay, cki);
                }
                else
                {
                    _gameOver = false;
                    break;
                }
            } while (cki.Key != ConsoleKey.Escape);

            _render.ExitMenuRender();
            cki = Console.ReadKey(true);

            if (cki.Key == ConsoleKey.R)
            {
                RestartGame();
            }
            else if (cki.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Method to count the current number of alive cells on the field.
        /// </summary>
        /// <param name="gameField">An array of the game field cells.</param>
        /// <returns>Returns the number of alive cells currently in the gamefield array.</returns>
        public int CountAliveCells(string[,] gameField)
        {
            int aliveCellCount = 0;

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    if (gameField[i, j] == AliveCellSymbol)
                    {
                        aliveCellCount++;
                    }
                }
            }
            return aliveCellCount;
        }

        /// <summary>
        /// Method to restart the game without rerunning the application.
        /// </summary>
        private void RestartGame()
        {
            _gliderGunMode = false;
            _resetGeneration = true;
            _delay = 1000;
            Console.Clear();
            _field = new Field();
            StartGame(_render, _file, _field, _library, _rulesApplier, _engine);
            RunGame();
        }

        /// <summary>
        /// Method to check user input in the main menu.
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        private void CheckInputMainMenu(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                    _length = 3;
                    _width = 3;
                    _correctKeyPressed = true;
                    break;

                case ConsoleKey.D2:
                    _length = 5;
                    _width = 5;
                    _correctKeyPressed = true;
                    break;

                case ConsoleKey.D3:
                    _length = 10;
                    _width = 10;
                    _correctKeyPressed = true;
                    break;

                case ConsoleKey.D4:
                    _length = 20;
                    _width = 20;
                    _correctKeyPressed = true;
                    break;

                case ConsoleKey.D5:
                    _length = 75;
                    _width = 40;
                    _correctKeyPressed = true;
                    break;

                case ConsoleKey.D6:
                    EnterFieldDimensions(_wrongInput);
                    _correctKeyPressed = true;
                    break;

                case ConsoleKey.L:
                    _gameField = _file.LoadGameFieldFromFile();
                    if (!_file.FileReadingError)
                    {
                        _generation = _file.Generation;
                        _loaded = true;
                        _readGeneration = true;
                        _correctKeyPressed = true;
                    }
                    break;

                case ConsoleKey.G:
                    _gliderGunMode = true;
                    break;

                case ConsoleKey.F1:
                    _render.PrintRules();
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;

                default:
                    _length = 10;
                    _width = 10;
                    break;
            }
        }

        /// <summary>
        /// Method to check user input in the glider gun menu,
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        private void CheckInputGliderGunMenu(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                    _length = 40;
                    _width = 30;
                    break;

                case ConsoleKey.G:
                    Console.Clear();
                    _gliderGunMode = false;
                    break;

                default:
                    Console.WriteLine(WrongInputPhrase);
                    break;
            }
        }

        /// <summary>
        /// Method to process user input field dimensions.
        /// </summary>
        /// <param name="wrongInput">Parameter that represent if there had been wrong input.</param>
        private void EnterFieldDimensions(bool wrongInput)
        {
            while (true)
            {
                if (wrongInput)
                {
                    Console.Clear();
                    Console.WriteLine(WrongInputPhrase);
                }
                Console.Write(EnterLengthPhrase);
                if (int.TryParse(Console.ReadLine(), out _length) && _length > 0)
                {
                    Console.Write(EnterWidthPhrase);
                    if (int.TryParse(Console.ReadLine(), out _width) && _width > 0)
                    {
                        break;
                    }
                    else
                    {
                        wrongInput = true;
                    }
                }
                else
                {
                    wrongInput = true;
                }
            }
        }

        /// <summary>
        /// Method for creating, rendering and populating the field upon starting the game.
        /// </summary>
        /// <param name="inputField">An array of a gamefield.</param>
        /// <param name="loaded">Boolean parameter that represents whether the field was loaded from the file.</param>
        /// <param name="gliderGunMode">Parameter to show whether the glider gun mode is on.</param>
        private void InitialCalculations(string[,] inputField, bool loaded, bool gliderGunMode)
        {
            _gameField = inputField;

            if (!loaded)
            {
                _gameField = _field.CreateField(_library, _engine, _rulesApplier, _render, inputField.GetLength(0), inputField.GetLength(1));
                Console.Clear();
                _render.RenderField(_gameField);
                _gameField = _field.PopulateField(gliderGunMode);
            }
            Console.Clear();
        }

        /// <summary>
        /// Method for calling methods that take care of all the necessary calculations during the runtime.
        /// </summary>
        /// <param name="delay">Delay between generations in miliseconds</param>
        /// <param name="gliderGunMode">Parameter to enable the Glider Gun mode with dead borders rules.</param>
        /// <param name="resetGeneration">Parameter to rest the number of generation after restart.</param>
        /// <returns>Returns a tuple containing an array of the game field, number of alive and dead cells and the generation number.</returns>
        private Tuple<string[,], int, int, int> RuntimeCalculations(int delay, bool gliderGunMode, bool resetGeneration, bool readGeneration)
        {
            int generationsAfterLoading = 1;
            int aliveCells = 0;
            int deadCells = 0;

            if (resetGeneration)
            {
                _generation = 1;
            }
            if (readGeneration)
            {
                _generation = _file.Generation;
                generationsAfterLoading = 0;
                readGeneration = false;
            }
            if (generationsAfterLoading >= 1)
            {
                _rulesApplier.DetermineCellsDestiny(_gameField, gliderGunMode);
                _gameField = _rulesApplier.FieldRefresh(_gameField);
                aliveCells = _engine.CountAliveCells(_gameField);
                deadCells = _gameField.GetLength(0) * _gameField.GetLength(1) - aliveCells;
            }
            if (generationsAfterLoading == 0)
            {
                generationsAfterLoading++;
                aliveCells = _engine.CountAliveCells(_gameField);
                deadCells = _gameField.GetLength(0) * _gameField.GetLength(1) - aliveCells;
            }
            if (aliveCells == 0)
            {
                _render.GameOverRender(_generation);
                _gameOver = true;
            }
            else
            {
                _render.RuntimeUIRender(aliveCells, deadCells, _generation, delay);
            }
            _returnValues = new Tuple<string[,], int, int, int>(_gameField, aliveCells, deadCells, _generation);
            _generation++;
            _render.RenderField(_gameField);
            return _returnValues;
        }
    }
}
