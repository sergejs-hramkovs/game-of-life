namespace GameOfLife
{
    public class Engine
    {
        private int _length;
        private int _width;
        private int _delay = 1000;
        private string[,] _gameField;
        private ConsoleKeyInfo _cki;
        private ConsoleKeyInfo _saveKey;
        private ConsoleKeyInfo _fieldSizeChoice;
        private bool _wrongInput = false;
        private bool _loaded = false;
        private bool _gliderGunMode = false;
        private File _file;
        private Tuple<string[,], int, int, int> _renderReturnValues;

        /// <summary>
        /// Initiate field size choice.
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Welcome to the Game of Life!");

            while (true)
            {
                if (_wrongInput)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong Input!");
                    _wrongInput = false;
                }
                if (!_gliderGunMode)
                {
                    Console.Clear();
                    Console.WriteLine("\nChoose the field size:");
                    Console.WriteLine("1. 3x3");
                    Console.WriteLine("2. 5x5");
                    Console.WriteLine("3. 10x10");
                    Console.WriteLine("4. 20x20");
                    Console.WriteLine("5. Custom");
                    Console.WriteLine("\n# To load the field from the file press 'L'");
                    Console.WriteLine("# To load Glider Gun Mode press 'G'");
                    Console.WriteLine("# Press 'F1' to read the rules and the description of the game");
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
                            while (true)
                            {
                                if (_wrongInput)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Wrong Input!");
                                    _wrongInput = false;
                                }
                                Console.Write("\nEnter the length of the field: ");
                                if (int.TryParse(Console.ReadLine(), out _length) && _length > 0)
                                {
                                    Console.Write("\nEnter the width of the field: ");
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
                            _file = new File();
                            _gameField = _file.LoadFromFile();
                            _loaded = true;
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
                        _fieldSizeChoice.Key == ConsoleKey.D5 || _fieldSizeChoice.Key == ConsoleKey.L)
                    {
                        break;
                    }
                    else
                    {
                        if (_fieldSizeChoice.Key != ConsoleKey.G)
                        {
                            _wrongInput = true;
                        }          
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("The Glider Gun Mode");
                    Console.WriteLine("\n1. 40x30 (The best size for a glider gun)");
                    Console.WriteLine("Press 'G' to turn off the Glider Gun Mode");
                    _fieldSizeChoice = Console.ReadKey(true);
                    if (_fieldSizeChoice.Key == ConsoleKey.D1)
                    {
                        _length = 40;
                        _width = 30;
                        break;
                    }
                    else if (_fieldSizeChoice.Key == ConsoleKey.G)
                    {
                        _gliderGunMode = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Input!");
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
                Console.WriteLine("\n# To save the current game state to a file press 'S'");
                Console.WriteLine("# To restart the game press 'R'");
                Console.WriteLine("# Press any other key to cancel saving and continue with the game");
                Console.WriteLine("# Press 'Esc' to exit");
                _saveKey = Console.ReadKey(true);

                if (_saveKey.Key == ConsoleKey.S)
                {
                    _file.SaveToFile(_renderReturnValues.Item1, _renderReturnValues.Item2, _renderReturnValues.Item3, _renderReturnValues.Item4);
                    Console.WriteLine("\n### The current game state has been successfully saved! Press any key to continue ###");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (_saveKey.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else if (_saveKey.Key == ConsoleKey.R)
                {
                    _gliderGunMode = false;
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
        public void Run()
        {
            Render.InitialRender(_length, _width, _gameField, _loaded);
            _loaded = false;
            _file = new File();

            do
            {
                while (Console.KeyAvailable == false)
                {
                    Console.SetCursorPosition(0, 0);
                    _renderReturnValues = Render.RuntimeRender(_delay, _gliderGunMode);
                    Thread.Sleep(_delay);
                }
                _cki = Console.ReadKey(true);
                Pause(_cki);
                _delay = ChangeDelay(_delay, _cki);
            } while (_cki.Key != ConsoleKey.Escape);

            Console.WriteLine("\n# Press 'R' to restart");
            Console.WriteLine("# Press 'Esc' to exit");
            _cki = Console.ReadKey(true);
            if (_cki.Key == ConsoleKey.R)
            {
                _gliderGunMode = false;
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
                    if (gameField[i, j] == "X")
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
                    if (gameField[i, j] == ".")
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
            _delay = 1000;
            Console.Clear();
            Start();
            Run();
        }
    }
}
