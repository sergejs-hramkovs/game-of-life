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
                    fieldArray[i, j] = ".";
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
        /// Function to choose how to seed the field - manually or automatically.
        /// </summary>
        /// <returns></returns>
        public string[,] SeedField()
        {
            string seedingChoice;

            while (true)
            {
                Console.WriteLine("\n1. To seed the field manually enter 'M'");
                Console.WriteLine("2. To seed the field automatically and randomly enter 'R'");
                Console.WriteLine("3. To choose objects from the library enter 'L'");
                Console.Write("\nChoice: ");
                seedingChoice = Console.ReadLine();

                if (seedingChoice == "M")
                {
                    ManualSeeding();
                    return fieldArray;
                }
                else if (seedingChoice == "R")
                {
                    RandomSeeding();
                    return fieldArray;
                }
                else if (seedingChoice == "L")
                {
                    ChooseFromLibrary();
                    return fieldArray;
                }
                else
                {
                    Console.WriteLine("Wrong Input!");
                }
            }
        }

        /// <summary>
        /// Cell seeding coordinates are entered manually by the user.
        /// </summary>
        /// <returns></returns>
        public string[,] ManualSeeding()
        {
            string input;
            int cellX;
            int cellY;

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

                if (fieldArray[cellX, cellY] == ".")
                {
                    fieldArray[cellX, cellY] = "X";
                }
                else
                {
                    fieldArray[cellX, cellY] = ".";
                }
                DrawField(fieldArray);
            }
        }

        /// <summary>
        /// Method to choose a cell pattern from the premade library.
        /// </summary>
        /// <returns></returns>
        public string[,] ChooseFromLibrary()
        {
            string input;
            int coordinateX;
            int coordinateY;

            while (true)
            {
                Console.WriteLine("\nTo stop seeding enter 'stop'");
                Console.WriteLine("To spawn a glider enter 'G'");
                Console.WriteLine("To spawn a light-weight spaceship enter 'LW'");
                Console.WriteLine("To go back to manual seeding enter 'M'");
                input = Console.ReadLine();
                if (input == "stop")
                {
                    Console.WriteLine("\nThe seeding has been stopped!");
                    return fieldArray;
                }
                if (input == "M")
                {
                    ManualSeeding();
                }
                if (input == "G")
                {
                    Console.Write("\nEnter X coordinate of the glider: ");
                    input = Console.ReadLine();

                    if (int.TryParse(input, out var resultX) && resultX >= 0 && resultX < fieldArray.GetLength(0))
                    {
                        coordinateX = resultX;
                        Console.Write("\nEnter Y coordinate of the glider: ");
                        input = Console.ReadLine();

                        if (int.TryParse(input, out var resultY) && resultY >= 0 && resultY < fieldArray.GetLength(0))
                        {
                            coordinateY = resultY;
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
                    SeedGlider(coordinateX, coordinateY);
                    DrawField(fieldArray);
                }
                if (input == "LW")
                {
                    Console.Write("\nEnter X coordinate of the LWSS: ");
                    input = Console.ReadLine();

                    if (int.TryParse(input, out var resultX) && resultX >= 0 && resultX < fieldArray.GetLength(0))
                    {
                        coordinateX = resultX;
                        Console.Write("\nEnter Y coordinate of the LWSS: ");
                        input = Console.ReadLine();

                        if (int.TryParse(input, out var resultY) && resultY >= 0 && resultY < fieldArray.GetLength(0))
                        {
                            coordinateY = resultY;
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
                    SeedLightweight(coordinateX, coordinateY);
                    DrawField(fieldArray);
                }
            }
        }

        /// <summary>
        /// Method to spawn a glider pattern.
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <returns></returns>
        public string[,] SeedGlider(int locationX, int locationY)
        {
            for (int i = locationX; i < locationX + 3; i++)
            {
                for (int j = locationY; j < locationY + 3; j++)
                {
                    switch (i - locationX)
                    {
                        case 0:
                            if (j - locationY == 2)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 1:
                            if (j - locationY == 0 || j - locationY == 2)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 2:
                            if (j - locationY == 1 || j - locationY == 2)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = "X";
                            }
                            break;
                    }
                }
            }
            return fieldArray;
        }

        /// <summary>
        /// Method to spawn a light-weight spaceship pattern.
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <returns></returns>
        public string[,] SeedLightweight(int locationX, int locationY)
        {
            for (int i = locationX; i < locationX + 5; i++)
            {
                for (int j = locationY; j < locationY + 4; j++)
                {
                    switch (i - locationX)
                    {
                        case 0:
                            if (j - locationY == 0 || j - locationY == 2)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 1:
                            if (j - locationY == 3)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 2:
                            if (j - locationY == 3)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 3:
                            if (j - locationY == 0 || j - locationY == 3)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 4:
                            if (j - locationY == 1 || j - locationY == 2 || j - locationY == 3)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = "X";
                            }
                            break;
                    }
                }
            }
            return fieldArray;
        }

        /// <summary>
        /// Cell amount and coordinates are generated automatically and randomly.
        /// </summary>
        /// <returns></returns>
        public string[,] RandomSeeding()
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
    }
}