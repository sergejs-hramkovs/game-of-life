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
        private StreamWriter _writer = new StreamWriter("field.txt");
        private string[,] _gameField;
        private string[] _stringField;

        public File(string[,] gameField)
        {
            _gameField = gameField;
            _stringField = new string[_gameField.GetLength(0)];
        }

        private string[] FieldToString()
        {
            for (int y = 0; y < _gameField.GetLength(1); y++)
            {
                for (int x = 0; x < _gameField.GetLength(0); x++)
                {
                    _stringField[y] += _gameField[x, y];
                }
            }
            return _stringField;
        }

        public void SaveToFile()
        {
            foreach (string line in _stringField)
            {
                _writer.WriteLine(line);
            }
        }
    }
}
