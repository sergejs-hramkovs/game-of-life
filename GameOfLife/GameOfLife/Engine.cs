using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife;

namespace GameOfLife
{
    public class Engine
    {
        int height;
        int width;
        int generation = 1;
        string[,] gameField;

        /// <summary>
        /// Initiate field size choice
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
            Field field = new Field(height, width);
            Iteration iteration = new Iteration();

            gameField = field.CreateField();
            field.DrawField(gameField);

            gameField = field.SeedField();

            Console.Clear();
            Console.CursorVisible = false;


            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Press ESC to stop");
                Console.WriteLine($"\nGeneration: {generation}");
                field.DrawField(gameField);
                iteration.CheckCells(gameField);
                gameField = iteration.FieldRefresh(gameField);
                Thread.Sleep(1000);
                generation++;
            }
        }
    }
}
