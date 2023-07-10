using GameOfLife.Entities.Models;
using GameOfLife.Interfaces;
using GameOfLife.Models;

namespace GameOfLife
{
    /// <summary>
    /// The FieldOperations class deals with populating game fields with alive cells or cell patterns from the library.
    /// </summary>
    [Serializable]
    public class FieldSeedingService : IFieldSeedingService
    {
        private readonly IRenderingService _renderingService;

        public FieldSeedingService(IRenderingService renderingService)
        {
            _renderingService = renderingService;
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
        /// Method to call one of the methods for spawning cell patterns from the library.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        /// <param name="SpawnLibraryObject">Parameter that represents the method for spawning a cell pattern from the library which is called.</param>
        public void CallSpawningMethod(GameModel game, Action<SingleGameField, int, int> SpawnLibraryObject)
        {
            //Console.Clear();
            //_renderingService.RenderGridOfFields(game);
            //if (_inputProcessorService.EnterCoordinates(game) && !StopDataInput)
            //{
            //    SpawnLibraryObject(game.MultipleGamesField.ListOfGames[0], CoordinateX, CoordinateY);
            //}
            //else if (StopDataInput)
            //{
            //    StopDataInput = false;
            //}
            //else
            //{
            //    _inputProcessorService.WrongInput = true;
            //}

            //Console.Clear();
        }
    }
}