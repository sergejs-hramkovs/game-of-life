using GameOfLife.Interfaces;

namespace GameOfLife
{
    public class FileIO : IFileIO
    {
        private string _filePath = @"C:\Users\sergejs.hramkovs\OneDrive - Accenture\Documents\field.txt";
        private string[,] _gameField;
        private string[] _stringField;
        private List<string> _stringList = new List<string>();
        private bool _fileReadingError = false;
        private int _generation;
        public bool FileReadingError
        {
            get => _fileReadingError;
            set => _fileReadingError = value;
        }
        public int Generation
        {
            get => _generation;
            set => _generation = value;
        }

        /// <summary>
        /// Method which converts 2-dimensional array to a 1-dimensional in order to minimize the number of 'write' operations to a file.
        /// </summary>
        /// <param name="gameField">An array containing game field cells.</param>
        /// <returns>Returns new 1-dimensional array.</returns>
        private string[] ConvertGameFieldToArrayOfRows(string[,] gameField)
        {
            _gameField = gameField;
            _stringField = new string[_gameField.GetLength(1)];

            for (int x = 0; x < _gameField.GetLength(0); x++)
            {
                for (int y = 0; y < _gameField.GetLength(1); y++)
                {
                    _stringField[y] = _stringField[y] + _gameField[x, y] + " ";
                }
            }
            return _stringField;
        }

        /// <summary>
        /// Method that takes the list of the game field cells and converts it to an array of the game field.
        /// </summary>
        /// <param name="inputList">List of the game field cells.</param>
        /// <returns>Returns an array of the game field.</returns>
        private string[,] ConvertListOfRowsToGameField(List<string> inputList)
        {
            int x = 0;
            int y = 0;
            string generationString = "";
            _gameField = new string[inputList[4].Length / 2, inputList.Count - 4];

            foreach (char character in inputList[0])
            {
                if (int.TryParse(character.ToString(), out int number))
                {
                    generationString += number.ToString();
                }
            }
            Generation = int.Parse(generationString);

            for (int i = 4; i < inputList.Count; i++)
            {
                foreach (char character in inputList[i])
                {
                    if ((x < inputList[4].Length / 2) && (y < inputList.Count - 4))
                    {
                        if (character == 'X' || character == '.')
                        {
                            _gameField[x, y] = character.ToString();
                            if (x == inputList[4].Length / 2 - 1)
                            {
                                x = 0;
                                y++;
                            }
                            else
                            {
                                x++;
                            }
                        }
                    }
                }
            }
            _stringList.Clear();
            return _gameField;
        }

        /// <summary>
        /// Method to save the current games state to a text file.
        /// </summary>
        /// <param name="currentGameState">An array with field cells representing the current game state.</param>
        /// <param name="aliveCount">Number of alive cells on the field.</param>
        /// <param name="deadCount">Number of dead cells on the field.</param>
        /// <param name="generation">Current generation number.</param>
        public void SaveGameFieldToFile(string[,] currentGameState, int aliveCount, int deadCount, int generation)
        {
            StreamWriter writer = new StreamWriter(_filePath);
            ConvertGameFieldToArrayOfRows(currentGameState);
            writer.WriteLine($"Generation: {generation}");
            writer.WriteLine($"Alive cells: {aliveCount}({(int)Math.Round(aliveCount / (double)(deadCount + aliveCount) * 100.0)}%)");
            writer.WriteLine($"Dead cells: {deadCount}");
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
        /// <returns>Returns call to ListToField method, which returns an array of the gamefield.</returns>
        public string[,] LoadGameFieldFromFile()
        {
            string line;

            try
            {
                StreamReader reader = new StreamReader(_filePath);
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
    }
}
