using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Views;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameOfLife
{
    /// <summary>
    /// The FileIO class deals with writing to the file and reading from it.
    /// It saves the state of the Game Field to the text file and then reads and restores data from it, if needed.
    /// It also saves\loads the states of all the Game Fields in the Mupltiple Games Mode.
    /// </summary>
    [Serializable]
    public class FileIO : IFileIO
    {
        private readonly IInputController _inputController;
        private readonly IMainEngine _mainEngine;
        private readonly IMenuNavigator _menuNavigator;

        private List<string> _stringList = new List<string>();
        private string[] _stringField;
        public string FilePath { get; set; }
        public string MultipleGamesModeFilePath { get; set; }
        public bool FileReadingError { get; set; }
        public bool FileLoaded { get; set; }
        public bool NoSavedGames { get; set; }
        public int NumberOfFiles { get; private set; }
        public int FileNumber { get; set; }

        /// <summary>
        /// Constructor that creates a path to the folder that stores the saved games files.
        /// </summary>
        public FileIO(IInputController inputController, IMainEngine mainEngine, IMenuNavigator menuNavigator)
        {
            _inputController = inputController;
            _mainEngine = mainEngine;
            _menuNavigator = menuNavigator;
            FilePath = AppDomain.CurrentDomain.BaseDirectory + StringConstants.SavedGamesFolderName;
            MultipleGamesModeFilePath = AppDomain.CurrentDomain.BaseDirectory + StringConstants.MultipleGamesModeSavedGamesFolderName;
        }

        /// <summary>
        /// Method to check if the directory exists. If it doesn't, the method creates one.
        /// </summary>
        /// <param name="filePath">The location of the folder.</param>
        private void EnsureDirectoryExists(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Directory.Exists)
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }
        }

        /// <summary>
        /// Method which converts 2-dimensional array to a 1-dimensional one in order to minimize the number of 'write' operations to a file.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <returns>Returns new 1-dimensional array of Game Field rows.</returns>
        private string[] ConvertGameFieldToArrayOfRows(GameFieldModel gameField)
        {
            _stringField = new string[gameField.Width];
            for (int xCoordinate = 0; xCoordinate < gameField.Length; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < gameField.Width; yCoordinate++)
                {
                    if (gameField.GameField[xCoordinate, yCoordinate] == StringConstants.AliveCellSymbol)
                    {
                        _stringField[yCoordinate] = _stringField[yCoordinate] + StringConstants.AliveCellSymbolChar.ToString() + " ";
                    }
                    else
                    {
                        _stringField[yCoordinate] = _stringField[yCoordinate] + StringConstants.DeadCellSymbolChar.ToString() + " ";
                    }
                }
            }

            return _stringField;
        }

        /// <summary>
        /// Method that takes the list of the Game Field cell rows and converts it into an array of the Game Field.
        /// </summary>
        /// <param name="inputList">List of the Game Field cell rows</param>
        private void ConvertListOfRowsToGameField(List<string> inputList)
        {
            int xCoordinate = 0;
            int yCoordinate = 0;
            _mainEngine.MultipleGames.ListOfGames.Add(new(inputList[4].Length / 2, inputList.Count - 4));
            _mainEngine.MultipleGames.Generation = int.Parse(TakeGenerationNumberFromFile(inputList));
            for (int listElementNumber = 4; listElementNumber < inputList.Count; listElementNumber++)
            {
                foreach (char character in inputList[listElementNumber])
                {
                    if ((xCoordinate < inputList[4].Length / 2) && (yCoordinate < inputList.Count - 4))
                    {
                        if (character == StringConstants.AliveCellSymbolChar || character == StringConstants.DeadCellSymbolChar)
                        {
                            if (character == StringConstants.AliveCellSymbolChar)
                            {
                                _mainEngine.MultipleGames.ListOfGames[0].GameField[xCoordinate, yCoordinate] = StringConstants.AliveCellSymbol;
                            }
                            else if (character == StringConstants.DeadCellSymbolChar)
                            {
                                _mainEngine.MultipleGames.ListOfGames[0].GameField[xCoordinate, yCoordinate] = StringConstants.DeadCellSymbol;
                            }

                            if (xCoordinate == (inputList[4].Length / 2 - 1))
                            {
                                xCoordinate = 0;
                                yCoordinate++;
                            }
                            else
                            {
                                xCoordinate++;
                            }
                        }
                    }
                }
            }

            _mainEngine.SavedGameLoaded = true;
            _stringList.Clear();
        }

        /// <summary>
        /// Method to read the generation number from the file.
        /// </summary>
        /// <param name="inputList">List of the Game Field cell rows.</param>
        /// <returns>Returns the generation number in the string form.</returns>
        private string TakeGenerationNumberFromFile(List<string> inputList)
        {
            string generationString = "";
            foreach (char character in inputList[0])
            {
                if (int.TryParse(character.ToString(), out int number))
                {
                    generationString += number.ToString();
                }
            }

            return generationString;
        }

        /// <summary>
        /// Method to save the Single Game Field state to a text file.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        public void SaveGameFieldToFile(GameFieldModel gameField)
        {
            EnsureDirectoryExists(FilePath);
            CountFiles(FilePath);
            StreamWriter writer = new(FilePath + $"game{NumberOfFiles + 1}.txt");
            ConvertGameFieldToArrayOfRows(gameField);
            writer.WriteLine($"Generation: {gameField.Generation}");
            writer.WriteLine($"Alive cells: {gameField.AliveCellsNumber}({(int)Math.Round(gameField.AliveCellsNumber / (double)gameField.Area * 100.0)}%)");
            writer.WriteLine($"Dead cells: {gameField.DeadCellsNumber}");
            writer.WriteLine();
            foreach (string line in _stringField)
            {
                writer.WriteLine(line);
            }

            writer.Close();
        }

        /// <summary>
        /// Method to load the saved field from the file.
        /// </summary>
        /// <param name="fileToLoad">The number of the saved game to be loaded.</param>
        public void LoadGameFieldFromFile(int fileToLoad)
        {
            string line;
            try
            {
                StreamReader reader = new StreamReader(FilePath + $"game{fileToLoad}.txt");
                while ((line = reader.ReadLine()) != null)
                {
                    _stringList.Add(line);
                }

                reader.Close();
            }
            catch
            {
                FileReadingError = true;
            }

            ConvertListOfRowsToGameField(_stringList);
        }

        /// <summary>
        /// Method to count the number of files in the folder.
        /// </summary>
        /// <param name="path">Parameter that stores the path to the folder.</param>
        private void CountFiles(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            NumberOfFiles = directoryInfo.GetFiles().Length;
            if (NumberOfFiles == 0)
            {
                NoSavedGames = true;
            }
        }

        /// <summary>
        /// Method to call file loading methods.
        /// </summary>
        /// <param name="loadMultipleGames">Parameter that represent whether Single Game or Multiple Games are loaded.</param>
        public void InitiateLoadingFromFile(bool loadMultipleGames = false)
        {
            string path;
            if (!loadMultipleGames)
            {
                path = FilePath;
            }
            else
            {
                path = MultipleGamesModeFilePath;
            }

            EnsureDirectoryExists(path);
            CountFiles(path);
            if (NoSavedGames)
            {
                _mainEngine.StartGame(false);
                _inputController.CorrectKeyPressed = true;
            }
            else
            {
                if (NumberOfFiles == 1)
                {
                    FileNumber = 1;
                }
                else
                {
                    _menuNavigator.NavigateSavedGameMenu(path);
                }

                if (!loadMultipleGames)
                {
                    LoadGameFieldFromFile(FileNumber);
                }
                else
                {
                    LoadMultipleGamesFromFile(FileNumber);
                }

                if (!FileReadingError)
                {
                    FileLoaded = true;
                    _mainEngine.ReadGeneration = true;
                    _inputController.CorrectKeyPressed = true;
                }
            }
        }

        /// <summary>
        /// Method to save all the games in the Multiple Games Mode to a file.
        /// </summary>
        /// <param name="multipleGames">An object that contains a list of Multiple Games.</param>
        public void SaveMultipleGamesToFile(MultipleGamesModel multipleGames)
        {
            EnsureDirectoryExists(MultipleGamesModeFilePath);
            CountFiles(MultipleGamesModeFilePath);
            using (Stream stream = File.Open(MultipleGamesModeFilePath + $"games{NumberOfFiles + 1}.bin", FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, multipleGames);
            }
        }

        /// <summary>
        /// Method to load Multiple Games from the file.
        /// </summary>
        /// <param name="fileToLoad">The number of file to be loaded.</param>
        public void LoadMultipleGamesFromFile(int fileToLoad)
        {
            using (Stream stream = File.Open(MultipleGamesModeFilePath + $"games{fileToLoad}.bin", FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                _mainEngine.MultipleGames = (MultipleGamesModel)binaryFormatter.Deserialize(stream);
            }

            _mainEngine.MultipleGamesMode = true;
            _mainEngine.SavedGameLoaded = true;
        }

        /// <summary>
        /// Method to create a list of names of the saved games files.
        /// </summary>
        /// <param name="filePath">The location of the folder.</param>
        public void CreateListOfFileNames(string filePath)
        {
            foreach (string file in Directory.GetFiles(filePath))
            {
                MenuViews.FileNames.Add(Path.GetFileName(file));
            }
        }
    }
}
