using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Views;

namespace GameOfLife
{
    /// <summary>
    /// The FieldOperations class deals with populating game fields with alive cells or cell patterns from the library.
    /// </summary>
    [Serializable]
    public class FieldOperations : IFieldOperations
    {
        private IRenderer _renderer;
        private ILibrary _library;
        private IInputController _inputController;
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public bool StopDataInput { get; set; }

        /// <summary>
        /// Constructor to inject required onjects in the class.
        /// </summary>
        /// <param name="library">An instance of the Library class.</param>
        /// <param name="render">An instance of the Render class.</param>
        /// <param name="controller">An instance of the InputController class.</param>
        public FieldOperations(ILibrary library, IRenderer render, IInputController controller)
        {
            _library = library;
            _renderer = render;
            _inputController = controller;
        }

        /// <summary>
        /// Method to initiate the field seeding.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <param name="gliderGunMode">Parameter to show whether the glider gun mode is on.</param>
        /// <param name="gliderGunType">Parameter that represents the chosen type of the glider gun.</param>
        /// <returns>Returns an instance of the GameFieldModel class.</returns>
        public GameFieldModel PopulateField(GameFieldModel gameField, bool gliderGunMode, int gliderGunType)
        {
            ConsoleKey seedingChoice;
            while (true)
            {
                if (_inputController.WrongInput)
                {
                    Console.Clear();
                    _renderer.RenderField(gameField);
                    Console.WriteLine("\n" + StringConstants.WrongInputPhrase);
                    _inputController.WrongInput = false;
                }

                if (gliderGunMode)
                {
                    Console.Clear();
                    LibrarySeeding(gameField, gliderGunMode, gliderGunType);
                    return gameField;
                }
                else
                {
                    _renderer.MenuRenderer(MenuViews.FieldSeedingChoiceChoiceMenu, clearScreen:false);
                    seedingChoice = Console.ReadKey(true).Key;
                }

                if (_inputController.CheckInputPopulateFieldMenu(seedingChoice))
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
                if (!_inputController.WrongInput)
                {
                    _renderer.RenderField(gameField);
                }
                else if (_inputController.WrongInput)
                {
                    _renderer.RenderField(gameField);
                    Console.WriteLine("\n" + StringConstants.WrongInputPhrase);
                    _inputController.WrongInput = false;
                }

                if (!_inputController.EnterCoordinates())
                {
                    _inputController.WrongInput = true;
                    continue;
                }
                else
                {
                    _inputController.WrongInput = false;
                }

                if (!StopDataInput)
                {
                    if (gameField.GameField[CoordinateX, CoordinateY] == StringConstants.DeadCellSymbol)
                    {
                        gameField.GameField[CoordinateX, CoordinateY] = StringConstants.AliveCellSymbol;
                    }
                    else
                    {
                        gameField.GameField[CoordinateX, CoordinateY] = StringConstants.DeadCellSymbol;
                    }
                }
                else
                {
                    StopDataInput = false;
                    break;
                }
            }

            return gameField;
        }

        /// <summary>
        /// Cells amount and coordinates are generated automatically and randomly.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <returns>Returns an instance of the GameFieldModel class with alive cells randomly seeded in its field.</returns>
        public GameFieldModel RandomSeeding(GameFieldModel gameField)
        {
            Random random = new();
            int aliveCellCount = random.Next(1, gameField.Length * gameField.Width);
            int randomX, randomY;
            for (int cellNumber = 1; cellNumber <= aliveCellCount; cellNumber++)
            {
                randomX = random.Next(0, gameField.Length);
                randomY = random.Next(0, gameField.Width);
                if (gameField.GameField[randomX, randomY] != StringConstants.AliveCellSymbol)
                {
                    gameField.GameField[randomX, randomY] = StringConstants.AliveCellSymbol;
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
            ConsoleKey libraryChoice;
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

                if (_inputController.WrongInput)
                {
                    Console.Clear();
                }

                _renderer.RenderField(gameField);
                _renderer.MenuRenderer(MenuViews.LibraryMenu, _inputController.WrongInput, clearScreen: false);
                _inputController.WrongInput = false;
                libraryChoice = Console.ReadKey(true).Key;
                if (_inputController.CheckInputLibraryMenu(libraryChoice))
                {
                    return gameField;
                }
            }
        }

        /// <summary>
        /// Method to call one of the methods for spawning an object from the library.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <param name="SpawnLibraryObject">Parameter that represents the method for spawning an object from the library that will be called.</param>
        public void CallSpawningMethod(GameFieldModel gameField, Func<GameFieldModel, int, int, GameFieldModel> SpawnLibraryObject)
        {
            Console.Clear();
            _renderer.RenderField(gameField);
            if (_inputController.EnterCoordinates() && !StopDataInput)
            {
                SpawnLibraryObject(gameField, CoordinateX, CoordinateY);
            }
            else if (StopDataInput)
            {
                StopDataInput = false;
            }
            else
            {
                _inputController.WrongInput = true;
            }

            Console.Clear();
        }
    }
}