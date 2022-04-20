﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class File
    {
        private string _filePath = @"C:\Users\sergejs.hramkovs\OneDrive - Accenture\Documents\field.txt";
        private string _line;
        private StreamWriter _writer;
        private StreamReader _reader;
        private string[,] _gameField;
        private string[] _stringField;
        private List<string> _stringList = new List<string>();


        /// <summary>
        /// Method which converts 2-dimensional array to a 1-dimensional in order to minimize the number of 'write' operations to a file.
        /// </summary>
        /// <param name="gameField">An array containing game field cells.</param>
        /// <returns>Returns new 1-dimensional array.</returns>
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

        private string[,] ListToField(List<string> inputList)
        {
            int x = 0;
            int y = 0;
            _gameField = new string[inputList[4].Length / 2, inputList.Count - 4];
            for (int i = 4; i < inputList.Count; i++)
            {
                foreach (char character in inputList[i])
                {
                    if ((y < inputList.Count - 4) && (x < inputList[4].Length / 2))
                    {
                        if (character == 'X' || character == '.')
                        {
                            _gameField[x, y] = character.ToString();
                        }
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
            return _gameField;
        }

        /// <summary>
        /// Method to save the current games state to a text file.
        /// </summary>
        /// <param name="currentGameState">An array with field cells representing the current game state.</param>
        /// <param name="aliveCount">Number of alive cells on the field.</param>
        /// <param name="deadCount">Number of dead cells on the field.</param>
        /// <param name="generation">Current generation number.</param>
        public void SaveToFile(string[,] currentGameState, int aliveCount, int deadCount, int generation)
        {
            _writer = new StreamWriter(_filePath);
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

        public string[,] LoadFromFile()
        {
            _reader = new StreamReader(_filePath);
            while ((_line = _reader.ReadLine()) != null)
            {
                _stringList.Add(_line);
            }
            return ListToField(_stringList);
        }
    }
}
