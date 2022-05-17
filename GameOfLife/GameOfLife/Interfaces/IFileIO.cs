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

        int FileNumber { get; set; }

        void Inject(IInputController inputController, IMainEngine engine, IMenuNavigator menuNavigator);

        void SaveGameFieldToFile(GameFieldModel gameField);

        void LoadGameFieldFromFile(int fileNumber);

        void InitiateLoadingFromFile(bool loadMultipleGames = false);

        void SaveMultipleGamesToFile(MultipleGamesModel multipleGames);

        void LoadMultipleGamesFromFile(int fileToLoad);

        void CreateListOfFileNames(string filePath);
    }
}
