using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IEngine
    {
        void Start();

        void Run();

        int CountAlive(string[,] gameField);

        int CountDead(string[,] gameField);
    }
}
