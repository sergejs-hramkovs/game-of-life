using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Iteration
    {
        public List<int> cellsToDieX = new List<int>();
        public List<int> cellsToDieY = new List<int>();

        public List<int> cellsToBeBornX = new List<int>();
        public List<int> cellsToBeBornY = new List<int>();

        public void CheckCells(string[,] field)
        {
            int neigboursCountOfAlive = 0;
            int neigboursCountOfDead = 0;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == "X")
                    {
                        for (int neighbourX = i - 1; neighbourX <= i + 1; neighbourX++)
                        {
                            for (int neighbourY = j - 1; neighbourY <= j + 1; neighbourY++)
                            {
                                if (field[neighbourX, neighbourY] == "X")
                                {
                                    neigboursCountOfAlive++;
                                }

                                if (neighbourX == i && neighbourY == j && neigboursCountOfAlive > 0)
                                {
                                    neigboursCountOfAlive--;
                                }                         
                            }                  
                        }

                        //Console.WriteLine($"Alive_cell({i}, {j}); Count = {neigboursCountOfAlive}");

                        if (neigboursCountOfAlive < 2 || neigboursCountOfAlive > 3)
                        {
                            cellsToDieX.Add(i);
                            cellsToDieY.Add(j);
                        }

                        neigboursCountOfAlive = 0;
                    }

                    if (field[i, j] == "-")
                    {
                        for (int neighbourX = i - 1; neighbourX <= i + 1; neighbourX++)
                        {
                            for (int neighbourY = j - 1; neighbourY <= j + 1; neighbourY++)
                            {
                                if (field[neighbourX, neighbourY] == "X")
                                {
                                    neigboursCountOfDead++;
                                }
                            }
                        }

                        //Console.WriteLine($"Dead_cell({i}, {j}); Count = {neigboursCountOfDead}");

                        if (neigboursCountOfDead == 3)
                        {
                            cellsToBeBornX.Add(i);
                            cellsToBeBornY.Add(j);
                        }

                        neigboursCountOfDead = 0;
                    }
                }
            }
        }

        public string[,] FieldRefresh(string[,] field)
        {
            for (int i = 0; i < cellsToDieX.Count; i++)
            {
                field[cellsToDieX[i], cellsToDieY[i]] = "-";
            }

            for (int i = 0; i < cellsToBeBornX.Count; i++)
            {
                field[cellsToBeBornX[i], cellsToBeBornY[i]] = "X";
            }

            return field;
        }

        public void DrawFieldNew(string[,] field)
        {
            Console.WriteLine("Generation 1: ");

            for (int j = 0; j < field.GetLength(0); j++)
            {
                for (int i = 0; i < field.GetLength(1); i++)
                {
                    Console.Write(" " + field[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}
