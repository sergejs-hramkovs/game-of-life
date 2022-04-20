namespace GameOfLife
{
    public class Field
    {
        private int _fieldLength { get; set; }
        private int _fieldWidth { get; set; }
        private string[,] _fieldArray;
        private string _inputCoordinate;
        private int _coordinateX;
        private int _coordinateY;
        private bool _wrongInput = false;
        private bool _stop = false;

        public Field(int length, int width)
        {
            _fieldLength = length;
            _fieldWidth = width;
        }

        /// <summary>
        /// Initial creation of an empty gaming field.
        /// </summary>
        /// <returns>Returns an array of a gamefield seeded with dead cells(.) .</returns>
        public string[,] CreateField()
        {
            _fieldArray = new string[_fieldWidth, _fieldLength];

            for (int j = 0; j < _fieldLength; j++)
            {
                for (int i = 0; i < _fieldWidth; i++)
                {
                    _fieldArray[i, j] = ".";
                }
            }

            return _fieldArray;
        }

        /// <summary>
        /// Method that draws the field.
        /// </summary>
        /// <param name="field">An array of a gamefield.</param>
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
        /// Method to choose how to seed the field - manually or automatically.
        /// </summary>
        /// <returns>Returns an array of a seeded gamefield.</returns>
        public string[,] SeedField()
        {
            string seedingChoice;

            while (true)
            {
                if (_wrongInput)
                {
                    Console.Clear();
                    DrawField(_fieldArray);
                    Console.WriteLine("\nWrong Input!");
                    _wrongInput = false;
                }
                Console.WriteLine("\n1. Seed the field manually");
                Console.WriteLine("2. Seed the field automatically and randomly");
                Console.WriteLine("3. Choose cell patterns from the library");
                Console.Write("\nChoice: ");
                seedingChoice = Console.ReadLine();

                switch (seedingChoice)
                {
                    case "1":
                        ManualSeeding();
                        return _fieldArray;

                    case "2":
                        RandomSeeding();
                        return _fieldArray;

                    case "3":
                        Console.Clear();
                        LibrarySeeding();
                        return _fieldArray;

                    default:
                        _wrongInput = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Cell seeding coordinates are entered manually by the user.
        /// </summary>
        /// <returns>Returns an array of manually seeded gamefield.</returns>
        private string[,] ManualSeeding()
        {
            while (true)
            {
                Console.Clear();
                if (!_wrongInput)
                {
                    DrawField(_fieldArray);
                }
                else if (_wrongInput)
                {
                    DrawField(_fieldArray);
                    Console.WriteLine("\nWrong Input!");
                    _wrongInput = false;
                }

                if (!EnterCoordinates())
                {
                    _wrongInput = true;
                    continue;
                }
                else
                {
                    _wrongInput = false;
                }

                // Without this "if" there is a problem with the displaying of the last cell.
                if (!_stop)
                {
                    if (_fieldArray[_coordinateX, _coordinateY] == ".")
                    {
                        _fieldArray[_coordinateX, _coordinateY] = "X";
                    }
                    else
                    {
                        _fieldArray[_coordinateX, _coordinateY] = ".";
                    }
                }             
                else
                {
                    _stop = false;
                    break;
                }
            }
            return _fieldArray;
        }

        /// <summary>
        /// Cell amount and coordinates are generated automatically and randomly.
        /// </summary>
        /// <returns>Returns an array of randomly seeded gamefield.</returns>
        private string[,] RandomSeeding()
        {
            Random random = new Random();
            int aliveCellCount = random.Next(1, _fieldWidth * _fieldLength);
            int randomX, randomY;

            for (int i = 1; i <= aliveCellCount; i++)
            {
                randomX = random.Next(0, _fieldArray.GetLength(0) - 1);
                randomY = random.Next(0, _fieldArray.GetLength(1) - 1);

                if (_fieldArray[randomX, randomY] != "X")
                {
                    _fieldArray[randomX, randomY] = "X";
                }
            }
            return _fieldArray;
        }

        /// <summary>
        /// Method to choose a cell pattern from the premade library.
        /// </summary>
        /// <returns>Returns an array of a gamefield seeded with objects from the library.</returns>
        private string[,] LibrarySeeding()
        {
            string inputPattern;
            Library library = new Library(_fieldArray);

            while (true)
            {
                if (!_wrongInput)
                {
                    DrawField(_fieldArray);
                }
                if (_wrongInput)
                {
                    Console.Clear();
                    DrawField(_fieldArray);
                    Console.WriteLine("\nWrong Input!");
                    _wrongInput = false;
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
                        return _fieldArray;

                    case "1":
                        if (!EnterCoordinates())
                        {
                            _wrongInput = true;
                            continue;
                        }
                        library.SeedGlider(_coordinateX, _coordinateY);
                        Console.Clear();
                        break;

                    case "2":
                        if (!EnterCoordinates())
                        {
                            _wrongInput = true;
                            continue;
                        }
                        library.SeedLightWeight(_coordinateX, _coordinateY);
                        Console.Clear();
                        break;

                    case "3":
                        if (!EnterCoordinates())
                        {
                            _wrongInput = true;
                            continue;
                        }
                        library.SeedMiddleWeight(_coordinateX, _coordinateY);
                        Console.Clear();
                        break;

                    case "4":
                        if (!EnterCoordinates())
                        {
                            _wrongInput = true;
                            continue;
                        }
                        library.SeedHeavyWeight(_coordinateX, _coordinateY);
                        Console.Clear();
                        break;

                    default:
                        _wrongInput = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Method to process user input coordinates.
        /// </summary>
        /// <returns>Returns "stop = true" if the process of entering coordinates was stopped. Returns false if there is wrong input.</returns>
        private bool EnterCoordinates()
        {
            Console.WriteLine("\nTo stop seeding enter 'stop'");
            Console.Write("\nEnter X coordinate: ");
            _inputCoordinate = Console.ReadLine();
            if (_inputCoordinate == "stop")
            {
                return _stop = true;
            }
            else if (int.TryParse(_inputCoordinate, out var resultX) && resultX >= 0 && resultX < _fieldArray.GetLength(0))
            {
                _coordinateX = resultX;
                Console.Write("\nEnter Y coordinate: ");
                _inputCoordinate = Console.ReadLine();
                if (_inputCoordinate == "stop")
                {
                    return _stop = true;
                }
                else if (int.TryParse(_inputCoordinate, out var resultY) && resultY >= 0 && resultY < _fieldArray.GetLength(1))
                {
                    _coordinateY = resultY;
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