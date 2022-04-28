using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IFileIO
    {
        bool FileReadingError { get; set; }

        bool FileLoaded { get; set; }

        string FilePath { get; }

        int NumberOfFiles { get; }

        void Injection(IRender render, IInputController inputController, IEngine engine);

        void SaveGameFieldToFile(GameFieldModel gameField);

        GameFieldModel LoadGameFieldFromFile(int fileNumber);

        void CountFiles();

        void InitiateLoadingFromFile();
    }
}
