﻿namespace GameOfLife
{
    public class Engine
    {
        private int _length;
        private int _width;
        private int _delay = 1000;
        private string[,] _gameField;
        private ConsoleKeyInfo _cki;
        private bool _wrongInput = false;

        /// <summary>
        /// Initiate field size choice.
        /// </summary>
        public void Start()
        {
            string fieldSizeChoice;
            Console.WriteLine("Welcome to the Game of Life!");

            while (true)
            {
                if (_wrongInput)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong Input!");
                    _wrongInput = false;
                }
                Console.WriteLine("\nChoose the field size:");
                Console.WriteLine("1. 3x3");
                Console.WriteLine("2. 5x5");
                Console.WriteLine("3. 10x10");
                Console.WriteLine("4. 20x20");
                Console.WriteLine("5. Custom");
                Console.Write("\nChoice: ");
                fieldSizeChoice = Console.ReadLine();

                switch (fieldSizeChoice)
                {
                    case "1":
                        _length = 3;
                        _width = 3;
                        break;

                    case "2":
                        _length = 5;
                        _width = 5;
                        break;

                    case "3":
                        _length = 10;
                        _width = 10;
                        break;

                    case "4":
                        _length = 20;
                        _width = 20;
                        break;

                    case "5":
                        while (true)
                        {
                            if (_wrongInput)
                            {
                                Console.Clear();
                                Console.WriteLine("Wrong Input!");
                                _wrongInput = false;
                            }
                            Console.Write("\nEnter the height of the field: ");
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

                    default:
                        _length = 10;
                        _width = 10;
                        break;
                }
                if (fieldSizeChoice == "1" || fieldSizeChoice == "2" || fieldSizeChoice == "3" || fieldSizeChoice == "4" || fieldSizeChoice == "5")
                {
                    break;
                }
                else
                {
                    _wrongInput = true;
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
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Main process of the game.
        /// </summary>
        public void Run()
        {
            Render.InitialRender(_length, _width, _gameField);

            do
            {
                while (Console.KeyAvailable == false)
                {
                    Console.SetCursorPosition(0, 0);
                    Render.RuntimeRender(_delay);
                    Thread.Sleep(_delay);
                }
                _cki = Console.ReadKey(true);
                _delay = ChangeDelay(_delay, _cki);
                Pause(_cki);
            } while (_cki.Key != ConsoleKey.Escape);

            Console.WriteLine("\nPress 'R' to restart");
            _cki = Console.ReadKey(true);
            if (_cki.Key == ConsoleKey.R)
            {
                Restart();
            }
        }

        /// <summary>
        /// Method to count the current number of alive cells on the field.
        /// </summary>
        /// <returns>Returns the number of alive cells currently in the gamefield array.</returns>
        public int CountAlive(string[,] gameField)
        {
            int aliveCellCount = 0;

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(0); j++)
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
        /// <returns>Returns the number of dead cells currently in the gamefield array.</returns>
        public int CountDead(string[,] gameField)
        {
            int deadCellCount = 0;

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(0); j++)
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
