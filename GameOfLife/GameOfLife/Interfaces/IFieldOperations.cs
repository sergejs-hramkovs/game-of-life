using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IFieldOperations
    {
        int CoordinateX { get; set; }

        int CoordinateY { get; set; }

        bool Stop { get; set; }

        GameFieldModel PopulateField(GameFieldModel gameField, bool gliderGunMode, int gliderGunType);

        GameFieldModel ManualSeeding(GameFieldModel gameField);

        GameFieldModel RandomSeeding(GameFieldModel gameField);

        GameFieldModel LibrarySeeding(GameFieldModel gameField, bool gliderGunMode, int gliderGunType);

        void CallSpawningMethod(GameFieldModel gameField, Func<GameFieldModel, int, int, GameFieldModel> SpawnLibraryObject);
    }
}
