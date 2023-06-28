using GameOfLife.Entities.Models;
using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IFieldSeedingService
    {

        /// <summary>
        /// Method to populate a field manually by entering cell cordinates manually.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        void PopulateFieldManually(GameModel game);

        /// <summary>
        /// Method to populate a field with randomly generated cell coordinates.
        /// </summary>
        /// <param name="gameField">A GameFieldModel object that contains the Game Field.</param>
        void PopulateFieldRandomly(SingleGameField gameField);

        /// <summary>
        /// Method to choose a cell pattern from the premade library.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that containts the list of Game Fields.</param>
        void PopulateFieldFromLibrary(GameModel game);

        /// <summary>
        /// Method to call one of the methods for spawning cell patterns from the library.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        /// <param name="SpawnLibraryObject">Parameter that represents the method for spawning a cell pattern from the library which is called.</param>
        void CallSpawningMethod(GameModel game, Action<SingleGameField, int, int> SpawnLibraryObject);
    }
}
