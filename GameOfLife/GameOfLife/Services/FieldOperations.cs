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
        private IInputController _inputController;
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public bool StopDataInput { get; set; }

        /// <summary>
        /// Constructor to inject required onjects in the class.
        /// </summary>
        /// <param name="renderer">An instance of the Render class.</param>
        /// <param name="controller">An instance of the InputController class.</param>
        public FieldOperations(IRenderer renderer, IInputController controller)
        {
            _renderer = renderer;
            _inputController = controller;
        }

        /// <summary>
        /// Method to populate a field manually by entering cell cordinates manually.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        public void PopulateFieldManually(MultipleGamesModel multipleGames)
        {
            while (true)
            {
                Console.Clear();
                if (!_inputController.WrongInput)
                {
                    _renderer.RenderGridOfFields(multipleGames);
                }
                else if (_inputController.WrongInput)
                {
                    _renderer.RenderGridOfFields(multipleGames);
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
        /// Method to populate a field with randomly generated cell coordinates.
        /// </summary>
        /// <param name="gameField">A GameFieldModel object that contains the Game Field.</param>
        public void PopulateFieldRandomly(GameFieldModel gameField)
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
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that containts the list of Game Fields.</param>
        public void PopulateFieldFromLibrary(MultipleGamesModel multipleGames)
        {
            do
            {
                Console.Clear();
                _renderer.RenderGridOfFields(multipleGames);
                _renderer.RenderMenu(MenuViews.LibraryMenu, _inputController.WrongInput, clearScreen: false);
                _inputController.WrongInput = false;
            }
            while (!_inputController.HandleInputLibraryMenu());
        }

        /// <summary>
        /// Method to call one of the methods for spawning cell patterns from the library.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        /// <param name="SpawnLibraryObject">Parameter that represents the method for spawning a cell pattern from the library which is called.</param>
        public void CallSpawningMethod(MultipleGamesModel multipleGames, Action<GameFieldModel, int, int> SpawnLibraryObject)
        {
            Console.Clear();
            _renderer.RenderGridOfFields(multipleGames);
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