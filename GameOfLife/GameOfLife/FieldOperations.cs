using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    public class FieldOperations : IFieldOperations
    {
        private int _coordinateX;
        private int _coordinateY;
        private bool _wrongInput = false;
        private bool _stop = false;
        private IRender _render;
        private ILibrary _library;
        private IEngine _engine;
        private IRulesApplier _rulesApplier;
        private IInputProcessor _inputProcessor;
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

        public FieldOperations(ILibrary library, IEngine engine, IRulesApplier rulesApplier, IRender render, IInputProcessor processor)
        {
            _library = library;
            _engine = engine;
            _rulesApplier = rulesApplier;
            _render = render;
            _inputProcessor = processor;
        }

        /// <summary>
        /// Method to choose how to seed the field - manually or automatically.
        /// </summary>
        /// <param name="gliderGunMode">Parameter to show whether the glider gun mode is on.</param>
        /// <param name="gliderGunType">Parameter that represents the chosen type of the glider gun.</param>
        /// <returns>Returns an array of a seeded gamefield.</returns>
        public GameFieldModel PopulateField(GameFieldModel gameField, bool gliderGunMode, int gliderGunType)
        {
            ConsoleKeyInfo seedingChoice;

            while (true)
            {
                if (_wrongInput)
                {
                    Console.Clear();
                    _render.RenderField(gameField);
                    Console.WriteLine("\n" + WrongInputPhrase);
                    _wrongInput = false;
                }
                if (gliderGunMode)
                {
                    Console.Clear();
                    LibrarySeeding(gameField, gliderGunMode, gliderGunType);
                    return gameField;
                }
                else
                {
                    _render.SeedFieldMenuRender();
                    seedingChoice = Console.ReadKey(true);
                }
                if (_inputProcessor.CheckInputPopulateFieldMenu(seedingChoice))
                {
                    return gameField;
                }
            }
        }

        /// <summary>
        /// Cell seeding coordinates are entered manually by the user.
        /// </summary>
        /// <returns>Returns an array of manually seeded gamefield.</returns>
        public GameFieldModel ManualSeeding(GameFieldModel gameField)
        {
            while (true)
            {
                Console.Clear();
                if (!_wrongInput)
                {
                    _render.RenderField(gameField);
                }
                else if (_wrongInput)
                {
                    _render.RenderField(gameField);
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
                    if (gameField.GameField[_coordinateX, _coordinateY] == DeadCellSymbol)
                    {
                        gameField.GameField[_coordinateX, _coordinateY] = AliveCellSymbol;
                    }
                    else
                    {
                        gameField.GameField[_coordinateX, _coordinateY] = DeadCellSymbol;
                    }
                }
                else
                {
                    _stop = false;
                    break;
                }
            }
            return gameField;
        }

        /// <summary>
        /// Cell amount and coordinates are generated automatically and randomly.
        /// </summary>
        /// <param name="fieldLength">The horizontal dimension of the field.</param>
        /// <param name="fieldWidth">The vertical dimension of the field.</param>
        /// <returns>Returns an array of randomly seeded gamefield.</returns>
        public GameFieldModel RandomSeeding(GameFieldModel gameField)
        {
            Random random = new();
            int aliveCellCount = random.Next(1, gameField.Length * gameField.Width);
            int randomX, randomY;

            for (int i = 1; i <= aliveCellCount; i++)
            {
                randomX = random.Next(0, gameField.Length);
                randomY = random.Next(0, gameField.Width);

                if (gameField.GameField[randomX, randomY] != AliveCellSymbol)
                {
                    gameField.GameField[randomX, randomY] = AliveCellSymbol;
                }
                else
                {
                    random = new();
                }
            }
            return gameField;
        }

        /// <summary>
        /// Method to choose a cell pattern from the premade library.
        /// </summary
        /// <param name="gliderGunMode">Parameter to show whether the glider gun mode is on.</param>
        /// <param name="gliderGunType">Parameter that represents the chosen type of the glider gun.</param>
        /// <returns>Returns an array of a gamefield seeded with objects from the library.</returns>
        public GameFieldModel LibrarySeeding(GameFieldModel gameField, bool gliderGunMode, int gliderGunType)
        {
            ConsoleKeyInfo libraryChoice;

            while (true)
            {
                if (gliderGunMode)
                {
                    switch (gliderGunType)
                    {
                        case 1:
                            gameField = _library.SpawnGosperGliderGun(gameField, 1, 1);
                            break;

                        case 2:
                            _library.SpawnSimkinGliderGun(gameField, 0, 16);
                            break;
                    }
                    Console.Clear();
                    return gameField;
                }

                if (!_wrongInput)
                {
                    _render.RenderField(gameField);
                }
                if (_wrongInput)
                {
                    Console.Clear();
                    _render.RenderField(gameField);
                    Console.WriteLine("\n" + WrongInputPhrase);
                    _wrongInput = false;
                }
                _render.LibraryMenuRender();
                libraryChoice = Console.ReadKey(true);

                if (_inputProcessor.CheckInputLibraryMenu(libraryChoice))
                {
                    return gameField;
                }
            }
        }

        /// <summary>
        /// Method to call one of the methods for spawning an object from the library, depending on the pressed key.
        /// </summary>
        /// <param name="SpawnLibraryObject">Parameter that represents the method for spawning an object from the library that will be called.</param>
        public void CallSpawningMethod(GameFieldModel gameField, Func<GameFieldModel, int, int, GameFieldModel> SpawnLibraryObject)
        {
            if (_inputProcessor.EnterCoordinates() && !_stop)
            {
                SpawnLibraryObject(gameField, _coordinateX, _coordinateY);
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