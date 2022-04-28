using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IFileIO
    {
        bool FileReadingError { get; set; }

        bool FileLoaded { get; set; }

        string FilePath { get; }

        int NumberOfFiles { get; }

        void SaveGameFieldToFile(GameFieldModel gameField);

        GameFieldModel LoadGameFieldFromFile(int fileNumber);

        void CountFiles();
    }
}
