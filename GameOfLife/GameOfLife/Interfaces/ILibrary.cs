using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface ILibrary
    {
        string[,] SpawnGlider(int locationX, int locationY);

        string[,] SpawnLightWeight(int locationX, int locationY);

        string[,] SpawnMiddleWeight(int locationX, int locationY);

        string[,] SpawnHeavyWeight(int locationX, int locationY);

        string[,] SpawnGliderGun(int locationX, int locationY);
    }
}
