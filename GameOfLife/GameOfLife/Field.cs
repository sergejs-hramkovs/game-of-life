using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Field
    {
        private int _fieldHeight { get; set; }
        private int _fieldWidth { get; set; }

        private string[,] fieldArray;

        public Field(int height, int width)
        {
            _fieldHeight = height;
            _fieldWidth = width;
        }

        /// <summary>
        /// Initial creation of an empty gaming field.
        /// </summary>
        /// <returns></returns>
        public string[,] CreateField()
        {
            fieldArray = new string[_fieldWidth, _fieldHeight];

            for (int j = 0; j < _fieldHeight; j++)
            {
                for (int i = 0; i < _fieldWidth; i++)
                {
                    fieldArray[i, j] = "-";
                }
            }

            return fieldArray;
        }

        /// <summary>
        /// Function that draws the field.
        /// </summary>
        /// <param name="field"></param>
        public void DrawField(string [,] field)
        {
            Console.WriteLine();

            for (int j = 0; j < field.GetLength(0); j++)
            {
                for (int i = 0; i < field.GetLength(1); i++)
                {
                    Console.Write(" " + field[i, j]);
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Seeding the field with user input cells.
        /// </summary>
        /// <returns></returns>
        public string[,] SeedField()
        {
            int cellX;
            int cellY;
            string input;
            string seedingChoice;

            while (true)
            {
                Console.WriteLine("\n1. To seed the field manually enter 'M'");
                Console.WriteLine("2. To seed the field automatically and randomly enter 'R'");
                Console.Write("\nChoice: ");
                seedingChoice = Console.ReadLine();

                if (seedingChoice == "M")
                {
                    while (true)
                    {
                        Console.WriteLine("\nTo stop seeding enter 'stop'");
                        Console.Write("\nEnter X coordinate of the cell: ");
                        input = Console.ReadLine();

                        if (input == "stop")
                        {
                            Console.WriteLine("\nThe seeding has been stopped!");
                            return fieldArray;
                        }

                        if (int.TryParse(input, out var resultX) && resultX >= 0 && resultX < fieldArray.GetLength(0))
                        {
                            cellX = resultX;

                            Console.Write("\nEnter Y coordinate of the cell: ");
                            input = Console.ReadLine();

                            if (input == "stop")
                            {
                                Console.WriteLine("\nThe seeding has been stopped!");
                                return fieldArray;
                            }

                            if (int.TryParse(input, out var resultY) && resultY >= 0 && resultY < fieldArray.GetLength(0))
                            {
                                cellY = resultY;
                            }

                            else
                            {
                                Console.WriteLine("\nWrong Input!");
                                continue;
                            }
                        }

                        else
                        {
                            Console.WriteLine("\nWrong Input!");
                            continue;
                        }

                        if (fieldArray[cellX, cellY] == "-")
                        {
                            fieldArray[cellX, cellY] = "X";
                        }
                        else
                        {
                            fieldArray[cellX, cellY] = "-";
                        }
                        
                        DrawField(fieldArray);
                    }
                }

                else if (seedingChoice == "R")
                {
                    Random random = new Random();
                    int aliveCellCount = random.Next(1, _fieldWidth * _fieldHeight);
                    int randomX, randomY;

                    for (int i = 1; i <= aliveCellCount; i++)
                    {

                        randomX = random.Next(0, fieldArray.GetLength(0) - 1);
                        randomY = random.Next(0, fieldArray.GetLength(1) - 1);

                        if (fieldArray[randomX, randomY] != "X")
                        {
                            fieldArray[randomX, randomY] = "X";
                        }
                      
                    }

                    return fieldArray;
                }

                else             
                {
                    Console.WriteLine("Wrong Input!");
                }
            }         
            
            return fieldArray;
        }
    }
}