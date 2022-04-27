using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface ILibrary
    {
        string[,] SpawnGlider(string[,] fieldArray, int locationX, int locationY);

        string[,] SpawnLightWeight(string[,] fieldArray, int locationX, int locationY);

        string[,] SpawnMiddleWeight(string[,] fieldArray, int locationX, int locationY);

        string[,] SpawnHeavyWeight(string[,] fieldArray, int locationX, int locationY);

        string[,] SpawnGosperGliderGun(string[,] fieldArray, int locationX, int locationY);

        string[,] SpawnSimkinGliderGun(string[,] fieldArray, int locationX, int locationY);
    }
}
