using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface ILibrary
    {
        string[,] SeedGlider(int locationX, int locationY);

        string[,] SeedLightWeight(int locationX, int locationY);

        string[,] SeedMiddleWeight(int locationX, int locationY);

        string[,] SeedHeavyWeight(int locationX, int locationY);

        string[,] SeedGliderGun(int locationX, int locationY);
    }
}
