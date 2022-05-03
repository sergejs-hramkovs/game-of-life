using GameOfLife.Interfaces;
using GameOfLife.Models;
using System.IO;

namespace GameOfLife
{
    /// <summary>
    /// The FileIO class deals with writing to the file and reading from it.
    /// It saves the state of the Game Field to the text file and then reads and restores data from it, if needed.
    /// </summary>
    public class FileIO : IFileIO
    {
        private IRender _render;
        private IInputController _inputController;
        private IEngine _engine;
        private string[] _stringField;
        private List<string> _stringList = new List<string>();
        public string FilePath { get; } = @"C:\GameOfLife_SavedGames\";
        public bool FileReadingError { get; set; } = false;
        public bool FileLoaded { get; set; } = false;
        public bool NoSavedGames { get; set; } = false;
        public int NumberOfFiles { get; private set; }

        /// <summary>
        /// Method to inject required objects into the class.
        /// </summary>
        /// <param name="render">An instance of the Render class.</param>
        /// <param name="inputController">An instance of the InputController class.</param>
        /// <param name="engine">An instance of the Engine class.</param>
        public void Injection(IRender render, IInputController inputController, IEngine engine)
        {
            _render = render;
            _inputController = inputController;
            _engine = engine;
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

            for (int horizontalX = 0; horizontalX < gameField.Length; horizontalX++)
            {
                for (int verticalY = 0; verticalY < gameField.Width; verticalY++)
                {
                    _stringField[verticalY] = _stringField[verticalY] + gameField.GameField[horizontalX, verticalY] + " ";
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
            int horizontalX = 0;
            int verticalY = 0;
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

            for (int i = 4; i < inputList.Count; i++)
            {
                foreach (char character in inputList[i])
                {
                    if ((horizontalX < inputList[4].Length / 2) && (verticalY < inputList.Count - 4))
                    {
                        if (character == 'X' || character == '.')
                        {
                            gameField.GameField[horizontalX, verticalY] = character.ToString();
                            if (horizontalX == inputList[4].Length / 2 - 1)
                            {
                                horizontalX = 0;
                                verticalY++;
                            }
                            else
                            {
                                horizontalX++;
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
            CountFiles();
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
        public void CountFiles()
        {
            DirectoryInfo directoryInfo = new(FilePath);
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
            CountFiles();
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
                        _render.ChooseFileToLoadMenuRender(NumberOfFiles, FilePath, _inputController.WrongInput);
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
    }
}
