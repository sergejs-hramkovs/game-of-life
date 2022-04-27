using GameOfLife.Interfaces;
using static GameOfLife.StringConstants;

namespace GameOfLife
{
    public class Engine : IEngine
    {
        private string[,] _gameField;
        private int _length;
        private int _width;
        private int _delay = 1000;
        private int _generation = 1;
        private int _gliderGunType = 0;
        private bool _wrongInput = false;
        private bool _loaded = false;
        private bool _readGeneration = false;
        private bool _gliderGunMode = false;
        private bool _resetGeneration = false;
        private bool _correctKeyPressed = false;
        private bool _gameOver = false;
        private IFileIO _file;
        private IRender _render;
        private IField _field;
        private ILibrary _library;
        private IRulesApplier _rulesApplier;
        private IEngine _engine;
        private IInputProcessor _inputProcessor;
        private Tuple<string[,], int, int, int> _returnValues;
        public int Length
        {
            get => _length;
            set => _length = value;
        }
        public int Width
        {
            get => _width;
            set => _width = value;
        }
        public bool CorrectKeyPressed
        {
            get => _correctKeyPressed;
            set => _correctKeyPressed = value;
        }
        public bool WrongInput
        {
            get => _wrongInput;
            set => _wrongInput = value;
        }
        public string[,] GameField
        {
            get => _gameField;
            set => _gameField = value;
        }
        public int Generation
        {
            get => _generation;
            set => _generation = value;
        }
        public bool Loaded
        {
            get => _loaded;
            set => _loaded = value;
        }
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
        public int GliderGunType
        {
            get => _gliderGunType;
            set => _gliderGunType = value;
        }

        /// <summary>
        /// Initiate field size choice.
        /// </summary>
        public void StartGame(IRender render, IFileIO file, IField field, ILibrary library, IRulesApplier rulesApplier,
            IEngine engine, IInputProcessor inputProcessor)
        {
            ConsoleKeyInfo fieldSizeChoice;
            _render = render;
            _file = file;
            _field = field;
            _library = library;
            _rulesApplier = rulesApplier;
            _engine = engine;
            _inputProcessor = inputProcessor;

            Console.CursorVisible = false;
            Console.SetWindowSize(170, 55);

            while (true)
            {
                if (!_gliderGunMode)
                {
                    _render.MainMenuRender(_wrongInput, _file.FileReadingError);
                    _wrongInput = false;
                    _file.FileReadingError = false;
                    fieldSizeChoice = Console.ReadKey(true);
                    _inputProcessor.CheckInputMainMenu(fieldSizeChoice);

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
                    _render.GliderGunModeRender();
                    fieldSizeChoice = Console.ReadKey(true);
                    _inputProcessor.CheckInputGliderGunMenu(fieldSizeChoice);
                    if (fieldSizeChoice.Key == ConsoleKey.D1 || fieldSizeChoice.Key == ConsoleKey.D2)
                    {
                        break;
                    }
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
                FirstRenderCalculations();
            }
            else
            {
                Console.Clear();
            }
            _loaded = false; // To reset the fact of previous loading to avoid disruption of the game after restart.

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
        /// Method for calling necessary methods for the first rendering.
        /// </summary>
        private void FirstRenderCalculations()
        {
            _gameField = _field.CreateField(_library, _engine, _rulesApplier, _render, _inputProcessor, _length, _width);
            Console.Clear();
            _render.RenderField(_gameField);
            _gameField = _field.PopulateField(_gliderGunMode, _gliderGunType);
            Console.Clear();
            _render.BlankUIRender();
            _render.RenderField(_gameField);
            Thread.Sleep(2000);
            Console.Clear();
        }

        /// <summary>
        /// Method for calling methods that take care of all the necessary calculations during the runtime.
        /// </summary>
        /// <param name="delay">Delay between generations in miliseconds</param>
        /// <param name="gliderGunMode">Parameter to enable the Glider Gun mode with dead borders rules.</param>
        /// <param name="resetGeneration">Parameter to reset the number of generation after restart.</param>
        /// <param name="readGeneration">Parameter that represents if the generation was read from the file.</param>
        /// <returns>Returns a tuple containing an array of the game field, number of alive and dead cells and the generation number.</returns>
        private Tuple<string[,], int, int, int> RuntimeCalculations(int delay, bool gliderGunMode, bool resetGeneration, bool readGeneration)
        {
            int generationsAfterLoading = 1; // Parameter for proper loading from file.
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
                aliveCells = CountAliveCells(_gameField);
                deadCells = _gameField.GetLength(0) * _gameField.GetLength(1) - aliveCells;
            }
            if (generationsAfterLoading == 0)
            {
                generationsAfterLoading++;
                aliveCells = CountAliveCells(_gameField);
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
            if (_gameOver)
            {
                _render.RenderField(_gameField, true);
            }
            else
            {
                _render.RenderField(_gameField);
            }
            return _returnValues;
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
        /// Method to restart the game without rerunning the application.
        /// </summary>
        private void RestartGame()
        {
            _gliderGunMode = false;
            _resetGeneration = true;
            _delay = 1000;
            Console.Clear();
            StartGame(_render, _file, _field, _library, _rulesApplier, _engine, _inputProcessor);
            RunGame();
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
    }
}
