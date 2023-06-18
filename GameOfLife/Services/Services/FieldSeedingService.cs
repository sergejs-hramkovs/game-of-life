using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Views;

namespace GameOfLife
{
    /// <summary>
    /// The FieldOperations class deals with populating game fields with alive cells or cell patterns from the library.
    /// </summary>
    [Serializable]
    public class FieldSeedingService : IFieldSeedingService
    {
        private readonly IRenderingService _renderingService;
        private readonly IInputProcessorService _inputProcessorService;

        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public bool StopDataInput { get; set; }

        public FieldSeedingService(IRenderingService renderingService, IInputProcessorService inputProcessorService)
        {
            _renderingService = renderingService;
            _inputProcessorService = inputProcessorService;
        }

        /// <summary>
        /// Method to populate a field manually by entering cell cordinates manually.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        public void PopulateFieldManually(MultipleGamesField multipleGames)
        {
            while (true)
            {
                Console.Clear();
                if (!_inputProcessorService.WrongInput)
                {
                    _renderingService.RenderGridOfFields(multipleGames);
                }
                else if (_inputProcessorService.WrongInput)
                {
                    _renderingService.RenderGridOfFields(multipleGames);
                    Console.WriteLine("\n" + StringConstants.WrongInputPhrase);
                    _inputProcessorService.WrongInput = false;
                }

                if (!_inputProcessorService.EnterCoordinates())
                {
                    _inputProcessorService.WrongInput = true;
                    continue;
                }
                else
                {
                    _inputProcessorService.WrongInput = false;
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
        public void PopulateFieldRandomly(SingleGameField gameField)
        {
            var random = new Random();
            var aliveCellCount = random.Next(1, gameField.Length * gameField.Width);
            int randomX;
            int randomY;

            for (var cellNumber = 1; cellNumber <= aliveCellCount; cellNumber++)
            {
                randomX = random.Next(0, gameField.Length);
                randomY = random.Next(0, gameField.Width);

                if (gameField.GameField[randomX, randomY] != StringConstants.AliveCellSymbol)
                {
                    gameField.GameField[randomX, randomY] = StringConstants.AliveCellSymbol;
                }
                else
                {
                    random = new Random();
                }
            }
        }

        /// <summary>
        /// Method to choose a cell pattern from the premade library.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that containts the list of Game Fields.</param>
        public void PopulateFieldFromLibrary(MultipleGamesField multipleGames)
        {
            do
            {
                Console.Clear();
                _renderingService.RenderGridOfFields(multipleGames);
                _renderingService.RenderMenu(MenuViews.LibraryMenu, _inputProcessorService.WrongInput, clearScreen: false);
                _inputProcessorService.WrongInput = false;
            }
            while (!_inputProcessorService.HandleInputLibraryMenu());
        }

        /// <summary>
        /// Method to call one of the methods for spawning cell patterns from the library.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        /// <param name="SpawnLibraryObject">Parameter that represents the method for spawning a cell pattern from the library which is called.</param>
        public void CallSpawningMethod(MultipleGamesField multipleGames, Action<SingleGameField, int, int> SpawnLibraryObject)
        {
            Console.Clear();
            _renderingService.RenderGridOfFields(multipleGames);
            if (_inputProcessorService.EnterCoordinates() && !StopDataInput)
            {
                SpawnLibraryObject(multipleGames.ListOfGames[0], CoordinateX, CoordinateY);
            }
            else if (StopDataInput)
            {
                StopDataInput = false;
            }
            else
            {
                _inputProcessorService.WrongInput = true;
            }

            Console.Clear();
        }
    }
}