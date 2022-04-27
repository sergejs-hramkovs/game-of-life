using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IFileIO
    {
        int Generation { get; set; }

        bool FileReadingError { get; set; }

        void SaveGameFieldToFile(GameFieldModel gameField, int aliveCount, int deadCount, int generation);

        GameFieldModel LoadGameFieldFromFile();
    }
}
