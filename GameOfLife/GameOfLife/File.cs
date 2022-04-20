using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class File
    {
        private string _filePath;
        private StreamWriter _writer;
        private string[,] _gameField;
        private string[] _stringField;

        private string[] FieldToString(string[,] gameField)
        {
            _gameField = gameField;
            _stringField = new string[_gameField.GetLength(1)];
            for (int y = 0; y < _gameField.GetLength(1); y++)
            {
                for (int x = 0; x < _gameField.GetLength(0); x++)
                {
                    _stringField[x] = _stringField[x] + _gameField[x, y] + " ";
                }
            }
            return _stringField;
        }

        public void SaveToFile(string[,] currentGameState, int aliveCount, int deadCount, int generation)
        {
            _writer = new StreamWriter(@"C:\Users\sergejs.hramkovs\OneDrive - Accenture\Documents\field.txt");
            FieldToString(currentGameState);
            _writer.WriteLine($"Generation: {generation}");
            _writer.WriteLine($"Alive cells: {aliveCount}({(int)Math.Round(aliveCount / (double)(deadCount + aliveCount) * 100.0)}%)");
            _writer.WriteLine($"Dead cells: {deadCount}");
            _writer.WriteLine();
            foreach (string line in _stringField)
            {
                _writer.WriteLine(line);
            }
            _writer.Close();
        }
    }
}
