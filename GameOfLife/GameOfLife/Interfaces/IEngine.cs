using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IEngine
    {
        void Start(IRender render, IFileIO file);

        void Run(IField field);

        int CountAlive(string[,] gameField);

        int CountDead(string[,] gameField);
    }
}
