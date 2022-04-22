using GameOfLife.Interfaces;
using static GameOfLife.Phrases;

namespace GameOfLife
{
    public class Engine : IEngine
    {
        private int _length;
        private int _width;
        public int Length { get => _length; }

        public int Width { get => _width; }
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
        private IFileIO _file;
        private IRender _render;
        private IField _field;
        private Tuple<string[,], int, int, int> _renderReturnValues;

        /// <summary>
        /// Initiate field size choice.
        /// </summary>
        public void Start(IRender render, IFileIO file)
        {
            Console.SetWindowSize(170, 55);
            _render = render;
            _file = file;

            while (true)
            {
                if (!_gliderGunMode)
                {
                    _render.FieldSizeMenuRender(_wrongInput);
                    _wrongInput = false;
                    _fieldSizeChoice = Console.ReadKey(true);

                    switch (_fieldSizeChoice.Key)
                    {
                        case ConsoleKey.D1:
                            _length = 3;
                            _width = 3;
                            break;

                        case ConsoleKey.D2:
                            _length = 5;
                            _width = 5;
                            break;

                        case ConsoleKey.D3:
                            _length = 10;
                            _width = 10;
                            break;

                        case ConsoleKey.D4:
                            _length = 20;
                            _width = 20;
                            break;

                        case ConsoleKey.D5:
                            _length = 75;
                            _width = 40;
                            break;

                        case ConsoleKey.D6:
                            while (true)
                            {
                                if (_wrongInput)
                                {
                                    Console.Clear();
                                    Console.WriteLine(WrongInputPhrase);
                                    _wrongInput = false;
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
                                        _wrongInput = true;
                                    }
                                }
                                else
                                {
                                    _wrongInput = true;
                                }
                            }
                            break;

                        case ConsoleKey.L:                 
                            _gameField = _file.LoadFromFile();
                            _generation = _file.Generation;
                            _loaded = true;
                            _readGeneration = true;
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
                    if (_fieldSizeChoice.Key == ConsoleKey.D1 || _fieldSizeChoice.Key == ConsoleKey.D2 ||
                        _fieldSizeChoice.Key == ConsoleKey.D3 || _fieldSizeChoice.Key == ConsoleKey.D4 ||
                        _fieldSizeChoice.Key == ConsoleKey.D5 || _fieldSizeChoice.Key == ConsoleKey.D6 ||
                        _fieldSizeChoice.Key == ConsoleKey.L)
                    {
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
                    if (_fieldSizeChoice.Key == ConsoleKey.D1)
                    {
                        _length = 40;
                        _width = 30;
                        break;
                    }
                    else if (_fieldSizeChoice.Key == ConsoleKey.G)
                    {
                        Console.Clear();
                        _gliderGunMode = false;
                    }
                    else
                    {
                        Console.WriteLine(WrongInputPhrase);
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
        private void Pause(ConsoleKeyInfo keyPressed)
        {
            if (keyPressed.Key == ConsoleKey.Spacebar)
            {
                _render.PauseRender();
                _saveKey = Console.ReadKey(true);

                if (_saveKey.Key == ConsoleKey.S)
                {
                    _file.SaveToFile(_renderReturnValues.Item1, _renderReturnValues.Item2, _renderReturnValues.Item3, _renderReturnValues.Item4);
                    Console.WriteLine(SuccessfullySavedPhrase);
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (_saveKey.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else if (_saveKey.Key == ConsoleKey.R)
                {
                    Restart();
                }
                else
                {
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Main process of the game.
        /// </summary>
        public void Run(IField field)
        {
            _field = field;
            _render.InitialRender(_field, _gameField, _loaded, _gliderGunMode);
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
                Pause(_cki);
                _delay = ChangeDelay(_delay, _cki);
            } while (_cki.Key != ConsoleKey.Escape);

            _render.ExitRender();
            _cki = Console.ReadKey(true);
            if (_cki.Key == ConsoleKey.R)
            {
                Restart();
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
        public int CountAlive(string[,] gameField)
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
        /// Method to count the current number of dead cells on the field.
        /// </summary>
        /// <param name="gameField">An array of the game field cells.</param>
        /// <returns>Returns the number of dead cells currently in the gamefield array.</returns>
        public int CountDead(string[,] gameField)
        {
            int deadCellCount = 0;

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    if (gameField[i, j] == DeadCellSymbol)
                    {
                        deadCellCount++;
                    }
                }
            }
            return deadCellCount;
        }

        /// <summary>
        /// Method to restart the game without rerunning the application.
        /// </summary>
        private void Restart()
        {
            _gliderGunMode = false;
            _resetGeneration = true;
            _delay = 1000;
            Console.Clear();
            Start(_render, _file);
            _field = new Field(_length, _width);
            Run(_field);
        }
    }
}
