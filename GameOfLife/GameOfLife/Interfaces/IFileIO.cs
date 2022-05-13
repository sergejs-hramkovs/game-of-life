using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IFileIO
    {
        bool FileReadingError { get; set; }

        bool FileLoaded { get; set; }

        bool NoSavedGames { get; set; }

        string FilePath { get; }

        string MultipleGamesModeFilePath { get; }

        int NumberOfFiles { get; }

        void Injection(IRenderer render, IInputController inputController, IMainEngine engine, IUserInterfaceFiller userInterfaceFiller);

        void SaveGameFieldToFile(GameFieldModel gameField);

        void LoadGameFieldFromFile(int fileNumber);

        void InitiateLoadingFromFile(bool loadMultipleGames = false);

        void SaveMultipleGamesToFile(MultipleGamesModel multipleGames);

        void LoadMultipleGamesFromFile(int fileToLoad);
    }
}
