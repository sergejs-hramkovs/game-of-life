﻿namespace GameOfLife
{
    public class Engine
    {
        int height;
        int width;
        int delay = 1000;
        string[,] gameField;
        ConsoleKeyInfo cki;

        /// <summary>
        /// Initiate field size choice.
        /// </summary>
        public void Start()
        {
            string fieldSizeChoice;
            Console.WriteLine("Welcome to the Game of Life!");

            while (true)
            {
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
                        height = 3;
                        width = 3;
                        break;

                    case "2":
                        height = 5;
                        width = 5;
                        break;

                    case "3":
                        height = 10;
                        width = 10;
                        break;

                    case "4":
                        height = 20;
                        width = 20;
                        break;

                    case "5":
                        while (true)
                        {
                            Console.Write("\nEnter the height of the field: ");
                            if (int.TryParse(Console.ReadLine(), out height) && height > 0)
                            {
                                Console.Write("\nEnter the width of the field: ");
                                if (int.TryParse(Console.ReadLine(), out width) && width > 0)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\nWrong Input!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nWrong Input!");
                            }
                        }
                        break;

                    default:
                        height = 10;
                        width = 10;
                        break;
                }
                if (fieldSizeChoice == "1" || fieldSizeChoice == "2" || fieldSizeChoice == "3" || fieldSizeChoice == "4" || fieldSizeChoice == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nWrong input!");
                }
            }
        }

        /// <summary>
        /// Main process of the game.
        /// </summary>
        public void Run()
        {
            Render.InitialRender(height, width, gameField);

            do
            {
                while (Console.KeyAvailable == false)
                {
                    Console.SetCursorPosition(0, 0);
                    Render.RuntimeRender(delay);
                    Thread.Sleep(delay);
                }
                cki = Console.ReadKey(true);

                switch (cki.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (delay > 100)
                        {
                            delay -= 100;
                        }     
                        break;

                    case ConsoleKey.RightArrow:
                        delay += 100;
                        break;
                }
            } while (cki.Key != ConsoleKey.Escape);
        }
    }
}
