using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// The FileIO class deals with writing to the file and reading from it.
    /// It saves the state of the Game Field to the text file and then reads and restores data from it, if needed.
    /// It also saves\loads the states of all the Game Fields in the Mupltiple Games Mode.
    /// </summary>
    public interface IFileIO
    {
        bool FileReadingError { get; set; }

        bool FileLoaded { get; set; }

        bool NoSavedGames { get; set; }

        string FilePath { get; }

        string MultipleGamesModeFilePath { get; }

        int NumberOfFiles { get; }

        int FileNumber { get; set; }

        /// <summary>
        /// Method to inject required objects into the class.
        /// </summary>
        /// <param name="inputController">An instance of the InputController class.</param>
        /// <param name="engine">An instance of the Engine class.</param>
        void Inject(IInputController inputController, IMainEngine engine, IMenuNavigator menuNavigator);

        /// <summary>
        /// Method to save the Single Game Field state to a text file.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        void SaveGameFieldToFile(GameFieldModel gameField);

        /// <summary>
        /// Method to load the saved field from the file.
        /// </summary>
        /// <param name="fileToLoad">The number of the saved game to be loaded.</param>
        void LoadGameFieldFromFile(int fileToLoad);

        /// <summary>
        /// Method to call file loading methods.
        /// </summary>
        /// <param name="loadMultipleGames">Parameter that represent whether Single Game or Multiple Games are loaded.</param>
        void InitiateLoadingFromFile(bool loadMultipleGames = false);

        /// <summary>
        /// Method to save all the games in the Multiple Games Mode to a file.
        /// </summary>
        /// <param name="multipleGames">An object that contains a list of Multiple Games.</param>
        void SaveMultipleGamesToFile(MultipleGamesModel multipleGames);

        /// <summary>
        /// Method to load Multiple Games from the file.
        /// </summary>
        /// <param name="fileToLoad">The number of file to be loaded.</param>
        void LoadMultipleGamesFromFile(int fileToLoad);

        /// <summary>
        /// Method to create a list of names of the saved games files.
        /// </summary>
        /// <param name="filePath">The location of the folder.</param>
        void CreateListOfFileNames(string filePath);
    }
}
