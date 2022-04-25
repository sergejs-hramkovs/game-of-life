using GameOfLife.Interfaces;
using static GameOfLife.StringConstants;

namespace GameOfLife
{
    public class Engine : IEngine
    {
        private int _length;
        private int _width;
        private int _delay = 1000;
        private int _generation;
        private string[,] _gameField;
        private ConsoleKeyInfo _cki;
        private ConsoleKeyInfo _saveKey;
        private ConsoleKeyInfo _fieldSizeChoice;
        private bool _wrongInput = false;
        private bool _loaded = false;
        private bool _readGeneration = false;
        private bool _gliderGunMode = false;
        private bool _resetGeneration = false;
        private bool _correctKeyPressed = false;
        private IFileIO _file;
        private IRender _render;
        private IField _field;
        private ILibrary _library;
        private IRulesApplier _rulesApplier;
        private IEngine _engine;
        private Tuple<string[,], int, int, int> _renderReturnValues;

        /// <summary>
        /// Initiate field size choice.
        /// </summary>
        public void StartGame(IRender render, IFileIO file, IField field, ILibrary library, IRulesApplier rulesApplier, IEngine engine)
        {
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
                    _fieldSizeChoice = Console.ReadKey(true);
                    CheckInputMainMenu(_fieldSizeChoice);

                    if (_correctKeyPressed)
                    {
                        _correctKeyPressed = false;
                        break;
                    }
                    else
                    {
                        if (_fieldSizeChoice.Key != ConsoleKey.G && _fieldSizeChoice.Key != ConsoleKey.F1)
                        {
                            _wrongInput = true;
                        }
                    }
                }
                else
                {
                    _render.GliderGunMenuRender();
                    _fieldSizeChoice = Console.ReadKey(true);
                    CheckInputGliderGunMenu(_fieldSizeChoice);
                    if (_fieldSizeChoice.Key == ConsoleKey.D1)
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
            if (keyPressed.Key == ConsoleKey.Spacebar)
            {
                _render.PauseMenuRender();
                _saveKey = Console.ReadKey(true);

                switch (_saveKey.Key)
                {
                    case ConsoleKey.S:
                        _file.SaveGameFieldToFile(_renderReturnValues.Item1, _renderReturnValues.Item2, _renderReturnValues.Item3, _renderReturnValues.Item4);
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
            if (!_loaded)
            {
                _gameField = _field.CreateField(_library, _engine, _rulesApplier, _render, _length, _width);
            }
            _render.InitialRender(_field, _engine, _rulesApplier, _library, _gameField, _loaded, _gliderGunMode);
            _loaded = false;

            do
            {
                while (Console.KeyAvailable == false)
                {
                    Console.SetCursorPosition(0, 0);
                    _renderReturnValues = _render.RuntimeRender(_delay, _gliderGunMode, _resetGeneration, _readGeneration, _generation);
                    _readGeneration = false;
                    if (_resetGeneration)
                    {
                        _resetGeneration = false;
                    }
                    Thread.Sleep(_delay);
                }
                _cki = Console.ReadKey(true);
                PauseGame(_cki);
                _delay = ChangeDelay(_delay, _cki);
            } while (_cki.Key != ConsoleKey.Escape);

            _render.ExitMenuRender();
            _cki = Console.ReadKey(true);
            if (_cki.Key == ConsoleKey.R)
            {
                RestartGame();
            }
            else if (_cki.Key == ConsoleKey.Escape)
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
                    Rules.PrintRules();
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
    }
}
