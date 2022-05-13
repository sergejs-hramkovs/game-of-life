using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IFieldOperations
    {
        int CoordinateX { get; set; }

        int CoordinateY { get; set; }

        bool StopDataInput { get; set; }

        void ManualSeeding(MultipleGamesModel multipleGames);

        void RandomSeeding(GameFieldModel gameField);

        void LibrarySeeding(MultipleGamesModel multipleGames);

        void CallSpawningMethod(MultipleGamesModel multipleGames, Action<GameFieldModel, int, int> SpawnLibraryObject);
    }
}
