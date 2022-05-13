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
        /// Cell seeding coordinates are entered manually by the user.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <returns>Returns an instance of the GameFieldModel class with alive cells manually seeded in its field.</returns>
        public void ManualSeeding(MultipleGamesModel multipleGames)
        {
            while (true)
            {
                Console.Clear();
                if (!_inputController.WrongInput)
                {
                    _renderer.GridOfFieldsRenderer(multipleGames);
                }
                else if (_inputController.WrongInput)
                {
                    _renderer.GridOfFieldsRenderer(multipleGames);
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
                    if (multipleGames.ListOfGames[0].GameField[CoordinateX, CoordinateY] == StringConstants.DeadCellSymbol)
                    {
                        multipleGames.ListOfGames[0].GameField[CoordinateX, CoordinateY] = StringConstants.AliveCellSymbol;
                    }
                    else
                    {
                        multipleGames.ListOfGames[0].GameField[CoordinateX, CoordinateY] = StringConstants.DeadCellSymbol;
                    }
                }
                else
                {
                    StopDataInput = false;
                    break;
                }
            }
        }

        /// <summary>
        /// Cells amount and coordinates are generated automatically and randomly.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <returns>Returns an instance of the GameFieldModel class with alive cells randomly seeded in its field.</returns>
        public void RandomSeeding(GameFieldModel gameField)
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
        }

        /// <summary>
        /// Method to choose a cell pattern from the premade library.
        /// </summary
        public void LibrarySeeding(MultipleGamesModel multipleGames)
        {
            ConsoleKey libraryChoice;
            do
            {
                Console.Clear();
                _renderer.GridOfFieldsRenderer(multipleGames);
                _renderer.MenuRenderer(MenuViews.LibraryMenu, _inputController.WrongInput, clearScreen: false);
                _inputController.WrongInput = false;
            }
            while (!_inputController.LibraryMenuInputProcessor());
        }

        /// <summary>
        /// Method to call one of the methods for spawning an object from the library.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <param name="SpawnLibraryObject">Parameter that represents the method for spawning an object from the library that will be called.</param>
        public void CallSpawningMethod(MultipleGamesModel multipleGames, Action<GameFieldModel, int, int> SpawnLibraryObject)
        {
            Console.Clear();
            _renderer.GridOfFieldsRenderer(multipleGames);
            if (_inputController.EnterCoordinates() && !StopDataInput)
            {
                SpawnLibraryObject(multipleGames.ListOfGames[0], CoordinateX, CoordinateY);
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