using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    public class FieldOperations : IFieldOperations
    {
        private int _coordinateX;
        private int _coordinateY;
        private bool _stop = false;
        private IRender _render;
        private ILibrary _library;
        private IInputController _inputProcessor;
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

        public FieldOperations(ILibrary library, IRender render, IInputController processor)
        {
            _library = library;
            _render = render;
            _inputProcessor = processor;
        }

        /// <summary>
        /// Method to choose how to seed the field - manually or automatically.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <param name="gliderGunMode">Parameter to show whether the glider gun mode is on.</param>
        /// <param name="gliderGunType">Parameter that represents the chosen type of the glider gun.</param>
        /// <returns>Returns an instance of the GameFieldModel class.</returns>
        public GameFieldModel PopulateField(GameFieldModel gameField, bool gliderGunMode, int gliderGunType)
        {
            ConsoleKeyInfo seedingChoice;

            while (true)
            {
                if (_inputProcessor.WrongInput)
                {
                    Console.Clear();
                    _render.RenderField(gameField);
                    Console.WriteLine("\n" + WrongInputPhrase);
                    _inputProcessor.WrongInput = false;
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
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <returns>Returns an instance of the GameFieldModel class with alive cells manually seeded in its field.</returns>
        public GameFieldModel ManualSeeding(GameFieldModel gameField)
        {
            while (true)
            {
                Console.Clear();
                if (!_inputProcessor.WrongInput)
                {
                    _render.RenderField(gameField);
                }
                else if (_inputProcessor.WrongInput)
                {
                    _render.RenderField(gameField);
                    Console.WriteLine("\n" + WrongInputPhrase);
                    _inputProcessor.WrongInput = false;
                }
                if (!_inputProcessor.EnterCoordinates())
                {
                    _inputProcessor.WrongInput = true;
                    continue;
                }
                else
                {
                    _inputProcessor.WrongInput = false;
                }
                if (!Stop)
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
                    Stop = false;
                    break;
                }
            }
            return gameField;
        }

        /// <summary>
        /// Cell amount and coordinates are generated automatically and randomly.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <returns>Returns an instance of the GameFieldModel class with alive cells randomly seeded in its field.</returns>
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
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <param name="gliderGunMode">Parameter to show whether the glider gun mode is on.</param>
        /// <param name="gliderGunType">Parameter that represents the chosen type of the glider gun.</param>
        /// <returns>Returns an instance of the GameFieldModel class with a library object seeded in its field.</returns>
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
                if (_inputProcessor.WrongInput)
                {
                    Console.Clear();
                }
                _render.RenderField(gameField);
                _render.LibraryMenuRender(_inputProcessor.WrongInput);
                _inputProcessor.WrongInput = false;
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
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <param name="SpawnLibraryObject">Parameter that represents the method for spawning an object from the library that will be called.</param>
        public void CallSpawningMethod(GameFieldModel gameField, Func<GameFieldModel, int, int, GameFieldModel> SpawnLibraryObject)
        {
            Console.Clear();
            _render.RenderField(gameField);
            if (_inputProcessor.EnterCoordinates() && !Stop)
            {
                SpawnLibraryObject(gameField, CoordinateX, CoordinateY);
            }
            else if (Stop)
            {
                Stop = false;
            }
            else
            {
                _inputProcessor.WrongInput = true;
            }
            Console.Clear();
        }
    }
}