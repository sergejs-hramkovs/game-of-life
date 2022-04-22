using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IEngine
    {
        void StartGame(IRender render, IFileIO file, IField field);

        void RunGame();

        int CountAliveCells(string[,] gameField);
    }
}
