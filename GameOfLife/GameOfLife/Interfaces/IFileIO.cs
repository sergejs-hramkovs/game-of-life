using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IFileIO
    {
        int Generation { get; }

        void SaveGameFieldToFile(string[,] currentGameState, int aliveCount, int deadCount, int generation);

        string[,] LoadGameFieldFromFile();
    }
}
