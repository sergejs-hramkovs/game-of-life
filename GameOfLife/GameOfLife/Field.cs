namespace GameOfLife
{
    public class Field
    {
        private int _fieldHeight { get; set; }
        private int _fieldWidth { get; set; }
        private string[,] fieldArray;
        string inputCoordinates;
        int coordinateX;
        int coordinateY;
        bool wrongInput = false;

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
        public void DrawField(string[,] field)
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
                if (wrongInput)
                {
                    Console.Clear();
                    DrawField(fieldArray);
                    Console.WriteLine("\nWrong Input!");
                    wrongInput = false;
                }
                Console.WriteLine("\n1. Seed the field manually");
                Console.WriteLine("2. Seed the field automatically and randomly");
                Console.WriteLine("3. Choose cell patterns from the library");
                Console.Write("\nChoice: ");
                seedingChoice = Console.ReadLine();

                if (seedingChoice == "1")
                {
                    ManualSeeding();
                    return fieldArray;
                }
                else if (seedingChoice == "2")
                {
                    RandomSeeding();
                    return fieldArray;
                }
                else if (seedingChoice == "3")
                {
                    Console.Clear();
                    LibrarySeeding();
                    return fieldArray;
                }
                else
                {
                    wrongInput = true;
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

        /// <summary>
        /// Method to choose a cell pattern from the premade library.
        /// </summary>
        /// <returns></returns>
        public string[,] LibrarySeeding()
        {
            string inputPattern;
            Library library = new Library(fieldArray);

            while (true)
            {   
                if (!wrongInput)
                {
                    DrawField(fieldArray);
                }
                if (wrongInput)
                {
                    Console.Clear();
                    DrawField(fieldArray);
                    Console.WriteLine("\nWrong Input!");
                    wrongInput = false;
                }
                Console.WriteLine("\n# To stop seeding enter 'stop'");
                Console.WriteLine("\n1. Spawn a glider");
                Console.WriteLine("2. Spawn a light-weight spaceship");
                Console.WriteLine("3. Spawn a middle-weight spaceship");
                Console.WriteLine("4. Spawn a heavy-weight spaceship");
                Console.Write("\nChoice: ");
                inputPattern = Console.ReadLine();

                switch (inputPattern)
                {
                    case "stop":
                        Console.WriteLine("\nThe seeding has been stopped!");
                        return fieldArray;

                    case "1":
                        if (!EnterCoordinates())
                        {
                            wrongInput = true;
                            continue;
                        }
                        library.SeedGlider(coordinateX, coordinateY);
                        Console.Clear();
                        break;

                    case "2":
                        if (!EnterCoordinates())
                        {
                            wrongInput = true;
                            continue;
                        }
                        library.SeedLightweight(coordinateX, coordinateY);
                        Console.Clear();
                        break;

                    case "3":
                        if (!EnterCoordinates())
                        {
                            wrongInput = true;
                            continue;
                        }
                        library.SeedMiddleweight(coordinateX, coordinateY);
                        Console.Clear();
                        break;

                    case "4":
                        if (!EnterCoordinates())
                        {
                            wrongInput = true;
                            continue;
                        }
                        library.SeedHeavyweight(coordinateX, coordinateY);
                        Console.Clear();
                        break;

                    default:
                        wrongInput = true;
                        break;
                }
            }
        }

        public bool EnterCoordinates()
        {
            Console.Write("\nEnter X coordinate: ");
            inputCoordinates = Console.ReadLine();

            if (int.TryParse(inputCoordinates, out var resultX) && resultX >= 0 && resultX < fieldArray.GetLength(0))
            {
                coordinateX = resultX;
                Console.Write("\nEnter Y coordinate: ");
                inputCoordinates = Console.ReadLine();

                if (int.TryParse(inputCoordinates, out var resultY) && resultY >= 0 && resultY < fieldArray.GetLength(1))
                {
                    coordinateY = resultY;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}