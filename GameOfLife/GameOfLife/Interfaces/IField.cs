using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IField
    {
        string[,] CreateField(int fieldLength, int fieldWidth);

        string[,] SeedField(bool gliderGunMode);
    }
}
