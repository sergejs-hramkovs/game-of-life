using GameOfLife.Interfaces;
using static GameOfLife.StringConstants;

namespace GameOfLife
{
    public class Field : IField
    {
        private string[,] _fieldArray;
        private int _coordinateX;
        private int _coordinateY;
        private bool _wrongInput = false;
        private bool _stop = false;
        private IRender _render;
        private ILibrary _library;
        private IEngine _engine;
        private IRulesApplier _rulesApplier;

        /// <summary>
        /// Initial creation of an empty gaming field.
        /// </summary>
        /// <returns>Returns an array of a gamefield seeded with dead cells(.) .</returns>
        public string[,] CreateField(ILibrary library, IEngine engine, IRulesApplier rulesApplier, IRender render, int fieldLength, int fieldWidth)
        {
            _library = library;
            _fieldArray = new string[fieldLength, fieldWidth];
            _engine = engine;
            _rulesApplier = rulesApplier;
            _render = render;

            for (int i = 0; i < fieldLength; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    _fieldArray[i, j] = DeadCellSymbol;
                }
            }
            return _fieldArray;
        }

        /// <summary>
        /// Method to choose how to seed the field - manually or automatically.
        /// </summary>
        /// <param name="gliderGunMode">Parameter to show whether the glider gun mode is on.</param>
        /// <returns>Returns an array of a seeded gamefield.</returns>
        public string[,] PopulateField(bool gliderGunMode)
        {
            ConsoleKeyInfo seedingChoice;

            while (true)
            {
                if (_wrongInput)
                {
                    Console.Clear();
                    _render.RenderField(_fieldArray);
                    Console.WriteLine("\n" + WrongInputPhrase);
                    _wrongInput = false;
                }
                if (gliderGunMode)
                {
                    Console.Clear();
                    LibrarySeeding(gliderGunMode);
                    return _fieldArray;
                }
                else
                {
                    _render.SeedFieldMenuRender();
                    seedingChoice = Console.ReadKey(true);
                }

                switch (seedingChoice.Key)
                {
                    case ConsoleKey.D1:
                        ManualSeeding();
                        return _fieldArray;

                    case ConsoleKey.D2:
                        RandomSeeding(_fieldArray.GetLength(0), _fieldArray.GetLength(1));
                        return _fieldArray;

                    case ConsoleKey.D3:
                        Console.Clear();
                        LibrarySeeding(gliderGunMode);
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
                    _render.RenderField(_fieldArray);
                }
                else if (_wrongInput)
                {
                    _render.RenderField(_fieldArray);
                    Console.WriteLine("\n" + WrongInputPhrase);
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
                if (!_stop)
                {
                    if (_fieldArray[_coordinateX, _coordinateY] == DeadCellSymbol)
                    {
                        _fieldArray[_coordinateX, _coordinateY] = AliveCellSymbol;
                    }
                    else
                    {
                        _fieldArray[_coordinateX, _coordinateY] = DeadCellSymbol;
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
        private string[,] RandomSeeding(int fieldLength, int fieldWidth)
        {
            Random random = new();
            int aliveCellCount = random.Next(1, fieldWidth * fieldLength);
            int randomX, randomY;

            for (int i = 1; i <= aliveCellCount; i++)
            {
                randomX = random.Next(0, _fieldArray.GetLength(0));
                randomY = random.Next(0, _fieldArray.GetLength(1));

                if (_fieldArray[randomX, randomY] != AliveCellSymbol)
                {
                    _fieldArray[randomX, randomY] = AliveCellSymbol;
                }
                else
                {
                    random = new();
                }
            }
            return _fieldArray;
        }

        /// <summary>
        /// Method to choose a cell pattern from the premade library.
        /// </summary
        /// <param name="gliderGunMode">Parameter to show whether the glider gun mode is on.</param>
        /// <returns>Returns an array of a gamefield seeded with objects from the library.</returns>
        private string[,] LibrarySeeding(bool gliderGunMode)
        {
            ConsoleKeyInfo libraryChoice;

            while (true)
            {
                if (gliderGunMode)
                {
                    _library.SpawnGliderGun(_fieldArray, 1, 1);
                    Console.Clear();
                    return _fieldArray;
                }

                if (!_wrongInput)
                {
                    _render.RenderField(_fieldArray);
                }
                if (_wrongInput)
                {
                    Console.Clear();
                    _render.RenderField(_fieldArray);
                    Console.WriteLine("\n" + WrongInputPhrase);
                    _wrongInput = false;
                }
                _render.LibraryMenuRender();
                libraryChoice = Console.ReadKey(true);

                switch (libraryChoice.Key)
                {
                    case ConsoleKey.Escape:
                        return _fieldArray;

                    case ConsoleKey.D1:
                        CallSpawningMethod(_library.SpawnGlider);
                        break;

                    case ConsoleKey.D2:
                        CallSpawningMethod(_library.SpawnLightWeight);
                        break;

                    case ConsoleKey.D3:
                        CallSpawningMethod(_library.SpawnMiddleWeight);
                        break;

                    case ConsoleKey.D4:
                        CallSpawningMethod(_library.SpawnHeavyWeight);
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
            string inputCoordinate;

            Console.WriteLine(StopSeedingPhrase);
            Console.Write(EnterXPhrase);
            inputCoordinate = Console.ReadLine();

            if (inputCoordinate == StopWord)
            {
                _stop = true;
            }
            else if (int.TryParse(inputCoordinate, out var resultX) && resultX >= 0 && resultX < _fieldArray.GetLength(0))
            {
                _coordinateX = resultX;
                Console.Write(EnterYPhrase);
                inputCoordinate = Console.ReadLine();

                if (inputCoordinate == StopWord)
                {
                    _stop = true;
                }
                else if (int.TryParse(inputCoordinate, out var resultY) && resultY >= 0 && resultY < _fieldArray.GetLength(1))
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

        /// <summary>
        /// Method to call one of the methods for spawning an object from the library, depending on the pressed key.
        /// </summary>
        /// <param name="SpawnLibraryObject">Parameter that represents the method for spawning an object from the library that will be called.</param>
        private void CallSpawningMethod(Func<string[,], int, int, string[,]> SpawnLibraryObject)
        {
            if (EnterCoordinates() && !_stop)
            {
                SpawnLibraryObject(_fieldArray, _coordinateX, _coordinateY);
            }
            else if (_stop)
            {
                _stop = false;
            }
            else
            {
                _wrongInput = true;
            }
            Console.Clear();
        }
    }
}