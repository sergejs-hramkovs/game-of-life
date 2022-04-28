using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IFileIO
    {
        bool FileReadingError { get; set; }

        string FilePath { get; }

        void SaveGameFieldToFile(GameFieldModel gameField, int aliveCount, int deadCount, int generation);

        GameFieldModel LoadGameFieldFromFile(int fileNumber);

        int CountFiles();
    }
}
