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
        private IInputProcessor _inputProcessor;
        public string[,] FieldArray
        {
            get => _fieldArray;
            set => _fieldArray = value;
        }
        public int CoordinateX
        {
            get => _coordinateX;
            set => _coordinateX = value;
        }
        public int CoordinateY
        {
            get => _coordinateY;
            set => _coordinateY = value;
        }
        public bool Stop
        {
            get => _stop;
            set => _stop = value;
        }

        /// <summary>
        /// Initial creation of an empty gaming field.
        /// </summary
        /// <param name="fieldLength">The horizontal dimension of the field.</param>
        /// <param name="fieldWidth">The vertical dimenstion of the field.</param>
        /// <returns>Returns an array of a gamefield seeded with dead cells(.) .</returns>
        public string[,] CreateField(ILibrary library, IEngine engine, IRulesApplier rulesApplier, IRender render, IInputProcessor processor,
            int fieldLength, int fieldWidth)
        {
            _library = library;
            _fieldArray = new string[fieldLength, fieldWidth];
            _engine = engine;
            _rulesApplier = rulesApplier;
            _render = render;
            _inputProcessor = processor;

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
        /// <param name="gliderGunType">Parameter that represents the chosen type of the glider gun.</param>
        /// <returns>Returns an array of a seeded gamefield.</returns>
        public string[,] PopulateField(bool gliderGunMode, int gliderGunType)
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
                    LibrarySeeding(gliderGunMode, gliderGunType);
                    return _fieldArray;
                }
                else
                {
                    _render.SeedFieldMenuRender();
                    seedingChoice = Console.ReadKey(true);
                }
                if (_inputProcessor.CheckInputPopulateFieldMenu(seedingChoice))
                {
                    return _fieldArray;
                }
            }
        }

        /// <summary>
        /// Cell seeding coordinates are entered manually by the user.
        /// </summary>
        /// <returns>Returns an array of manually seeded gamefield.</returns>
        public string[,] ManualSeeding()
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
                if (!_inputProcessor.EnterCoordinates())
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
        /// <param name="fieldLength">The horizontal dimension of the field.</param>
        /// <param name="fieldWidth">The vertical dimension of the field.</param>
        /// <returns>Returns an array of randomly seeded gamefield.</returns>
        public string[,] RandomSeeding(int fieldLength, int fieldWidth)
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
        /// <param name="gliderGunType">Parameter that represents the chosen type of the glider gun.</param>
        /// <returns>Returns an array of a gamefield seeded with objects from the library.</returns>
        public string[,] LibrarySeeding(bool gliderGunMode, int gliderGunType)
        {
            ConsoleKeyInfo libraryChoice;

            while (true)
            {
                if (gliderGunMode)
                {
                    switch (gliderGunType)
                    {
                        case 1:
                            _library.SpawnGosperGliderGun(_fieldArray, 1, 1);
                            break;

                        case 2:
                            _library.SpawnSimkinGliderGun(_fieldArray, 0, 16);
                            break;

                        default:
                            break;
                    }
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

                if (_inputProcessor.CheckInputLibraryMenu(libraryChoice))
                {
                    return _fieldArray;
                }
            }
        }

        /// <summary>
        /// Method to call one of the methods for spawning an object from the library, depending on the pressed key.
        /// </summary>
        /// <param name="SpawnLibraryObject">Parameter that represents the method for spawning an object from the library that will be called.</param>
        public void CallSpawningMethod(Func<string[,], int, int, string[,]> SpawnLibraryObject)
        {
            if (_inputProcessor.EnterCoordinates() && !_stop)
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