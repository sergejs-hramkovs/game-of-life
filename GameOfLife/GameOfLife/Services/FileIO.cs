using GameOfLife.Interfaces;
using GameOfLife.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static GameOfLife.StringConstantsModel;
using static GameOfLife.Views.MenuViews;

namespace GameOfLife
{
    /// <summary>
    /// The FileIO class deals with writing to the file and reading from it.
    /// It saves the state of the Game Field to the text file and then reads and restores data from it, if needed.
    /// </summary>
    [Serializable]
    public class FileIO : IFileIO
    {
        private IRenderer _render;
        private IInputController _inputController;
        private IEngine _engine;
        private IUserInterfaceFiller _userInterfaceFiller;
        private string[] _stringField;
        private List<string> _stringList = new List<string>();
        public string FilePath { get; set; }
        public string MultipleGamesModeFilePath { get; set; }
        public bool FileReadingError { get; set; }
        public bool FileLoaded { get; set; }
        public bool NoSavedGames { get; set; }
        public int NumberOfFiles { get; private set; }

        /// <summary>
        /// Constructor that creates a path to the folder that stores the saved games files.
        /// </summary>
        public FileIO()
        {
            FilePath = AppDomain.CurrentDomain.BaseDirectory + SavedGamesFolderName;
            MultipleGamesModeFilePath = AppDomain.CurrentDomain.BaseDirectory + MultipleGamesModeSavedGamesFolderName;
        }

        /// <summary>
        /// Method to inject required objects into the class.
        /// </summary>
        /// <param name="render">An instance of the Render class.</param>
        /// <param name="inputController">An instance of the InputController class.</param>
        /// <param name="engine">An instance of the Engine class.</param>
        public void Injection(IRenderer render, IInputController inputController, IEngine engine, IUserInterfaceFiller userInterfaceFiller)
        {
            _render = render;
            _inputController = inputController;
            _engine = engine;
            _userInterfaceFiller = userInterfaceFiller;
        }

        /// <summary>
        /// Method to check if the directory exists. If it doesn't, the method creates one.
        /// </summary>
        /// <param name="filePath">The location of the folder.</param>
        private void EnsureDirectoryExists(string filePath)
        {
            FileInfo fileInfo = new(filePath);
            if (!fileInfo.Directory.Exists)
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }
        }

        /// <summary>
        /// Method which converts 2-dimensional array to a 1-dimensional one in order to minimize the number of 'write' operations to a file.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <returns>Returns new 1-dimensional array of game field rows.</returns>
        private string[] ConvertGameFieldToArrayOfRows(GameFieldModel gameField)
        {
            _stringField = new string[gameField.Width];
            for (int xCoordinate = 0; xCoordinate < gameField.Length; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < gameField.Width; yCoordinate++)
                {
                    _stringField[yCoordinate] = _stringField[yCoordinate] + gameField.GameField[xCoordinate, yCoordinate] + " ";
                }
            }

            return _stringField;
        }

        /// <summary>
        /// Method that takes the list of the Game Field cells and converts it into an array of the Game Field.
        /// </summary>
        /// <param name="inputList">List of the game field cells.</param>
        /// <returns>Returns an instance of the GameFieldModel class.</returns>
        private GameFieldModel ConvertListOfRowsToGameField(List<string> inputList)
        {
            int xCoordinate = 0;
            int yCoordinate = 0;
            string generationString = "";
            GameFieldModel gameField = new(inputList[4].Length / 2, inputList.Count - 4);
            foreach (char character in inputList[0])
            {
                if (int.TryParse(character.ToString(), out int number))
                {
                    generationString += number.ToString();
                }
            }

            gameField.Generation = int.Parse(generationString);
            for (int listElementNumber = 4; listElementNumber < inputList.Count; listElementNumber++)
            {
                foreach (char character in inputList[listElementNumber])
                {
                    if ((xCoordinate < inputList[4].Length / 2) && (yCoordinate < inputList.Count - 4))
                    {
                        if (character == AliveCellSymbolChar || character == DeadCellSymbolChar)
                        {
                            gameField.GameField[xCoordinate, yCoordinate] = character.ToString();
                            if (xCoordinate == inputList[4].Length / 2 - 1)
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

            _stringList.Clear();
            return gameField;
        }

        /// <summary>
        /// Method to save the current games state to a text file.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        public void SaveGameFieldToFile(GameFieldModel gameField)
        {
            EnsureDirectoryExists(FilePath);
            CountFiles(FilePath);
            StreamWriter writer = new StreamWriter(FilePath + $"game{NumberOfFiles + 1}.txt");
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
        /// <returns>Returns call to ListToField method, which returns an instance of the GameField class.</returns>
        public GameFieldModel LoadGameFieldFromFile(int fileToLoad)
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
                return null;
            }

            return ConvertListOfRowsToGameField(_stringList);
        }

        /// <summary>
        /// Method to count the number of files in the folder.
        /// </summary>
        private void CountFiles(string path)
        {
            DirectoryInfo directoryInfo = new(path);
            NumberOfFiles = directoryInfo.GetFiles().Length;
            if (NumberOfFiles == 0)
            {
                NoSavedGames = true;
            }
        }

        /// <summary>
        /// Method to call file loading methods.
        /// </summary>
        public void InitiateLoadingFromFile()
        {
            int fileNumber;
            EnsureDirectoryExists(FilePath);
            CountFiles(FilePath);
            if (NoSavedGames)
            {
                _engine.StartGame(false);
                _inputController.CorrectKeyPressed = true;
            }
            else
            {
                if (NumberOfFiles == 1)
                {
                    fileNumber = 1;
                }
                else
                {
                    do
                    {
                        Console.CursorVisible = true;
                        CreateListOfFileNames(FilePath);
                        _userInterfaceFiller.ChooseFileMenuCreation(NumberOfFiles, FileNames);
                        if (_inputController.WrongInput)
                        {
                            _render.MenuRenderer(WrongInputFileMenu, wrongInput:true);
                        }
                        _render.MenuRenderer(ChooseFileMenu, newLine:false, clearScreen:!_inputController.WrongInput);
                        FileNames.Clear();
                        fileNumber = _inputController.CheckInputSavedGameMenu(NumberOfFiles);
                        Console.CursorVisible = false;
                    } while (_inputController.WrongInput);
                }

                _inputController.GameField = LoadGameFieldFromFile(fileNumber);
                if (!FileReadingError)
                {
                    FileLoaded = true;
                    _engine.ReadGeneration = true;
                    _inputController.CorrectKeyPressed = true;
                }
            }
        }

        /// <summary>
        /// Method to call file loading methods in the Multiple Games Mode.
        /// </summary>
        public void InitiateLoadingMultipleGamesFromFile()
        {
            int fileNumber;
            EnsureDirectoryExists(MultipleGamesModeFilePath);
            CountFiles(MultipleGamesModeFilePath);
            if (NoSavedGames)
            {
                _engine.StartGame(false);
                _inputController.CorrectKeyPressed = true;
            }
            else
            {
                if (NumberOfFiles == 1)
                {
                    fileNumber = 1;
                }
                else
                {
                    do
                    {
                        Console.CursorVisible = true;
                        CreateListOfFileNames(MultipleGamesModeFilePath);
                        _userInterfaceFiller.ChooseFileMenuCreation(NumberOfFiles, FileNames);
                        if (_inputController.WrongInput)
                        {
                            _render.MenuRenderer(WrongInputFileMenu, wrongInput: true);
                        }
                        _render.MenuRenderer(ChooseFileMenu, newLine: false, clearScreen: !_inputController.WrongInput);
                        FileNames.Clear();
                        fileNumber = _inputController.CheckInputSavedGameMenu(NumberOfFiles);
                        Console.CursorVisible = false;
                    } while (_inputController.WrongInput);
                }

                _inputController.MultipleGames = LoadMultipleGamesFromFile(fileNumber);
                if (!FileReadingError)
                {
                    FileLoaded = true;
                    _engine.ReadGeneration = true;
                    _inputController.CorrectKeyPressed = true;
                }
            }
        }

        /// <summary>
        /// Method to save all the game in the Multiple Games Mode to file.
        /// </summary>
        /// <param name="multipleGames">An object that contains a list of Multiple Games.</param>
        public void SaveMultipleGamesToFile(MultipleGamesModel multipleGames)
        {
            EnsureDirectoryExists(MultipleGamesModeFilePath);
            CountFiles(MultipleGamesModeFilePath);
            using (Stream stream = File.Open(MultipleGamesModeFilePath + $"games{NumberOfFiles + 1}.bin", FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new();
                binaryFormatter.Serialize(stream, multipleGames);
            }
        }

        /// <summary>
        /// Method to load Multiple Games from the file.
        /// </summary>
        /// <returns>Returns an object containing a lsit of Multiple Games.</returns>
        public MultipleGamesModel LoadMultipleGamesFromFile(int fileToLoad)
        {
            MultipleGamesModel multipleGames;
            using (Stream stream = File.Open(MultipleGamesModeFilePath + $"games{fileToLoad}.bin", FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new();
                multipleGames = (MultipleGamesModel)binaryFormatter.Deserialize(stream);
            }

            return multipleGames;
        }

        /// <summary>
        /// Method to create a list of names of the saved games files.
        /// </summary>
        /// <param name="filePath">The location of the folder.</param>
        private void CreateListOfFileNames(string filePath)
        {
            foreach (string file in Directory.GetFiles(filePath))
            {
                FileNames.Add(Path.GetFileName(file));
            }
        }
    }
}
