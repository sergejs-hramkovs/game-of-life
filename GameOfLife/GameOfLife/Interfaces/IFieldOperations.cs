using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IFieldOperations
    {
        int CoordinateX { get; set; }

        int CoordinateY { get; set; }

        bool StopDataInput { get; set; }

        void PopulateFieldManually(MultipleGamesModel multipleGames);

        void PopulateFieldRandomly(GameFieldModel gameField);

        void PopulateFieldFromLibrary(MultipleGamesModel multipleGames);

        void CallSpawningMethod(MultipleGamesModel multipleGames, Action<GameFieldModel, int, int> SpawnLibraryObject);
    }
}
