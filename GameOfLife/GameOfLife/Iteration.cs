namespace GameOfLife
{
    public class Iteration
    {
        public List<int> cellsToDieX = new List<int>();
        public List<int> cellsToDieY = new List<int>();
        public List<int> cellsToBeBornX = new List<int>();
        public List<int> cellsToBeBornY = new List<int>();

        /// <summary>
        /// Applies checks for alive and dead cells according to the rules.
        /// </summary>
        /// <param name="field"></param>
        public void CheckCells(string[,] field)
        {
            CheckAlive(field);
            CheckDead(field);
        }

        /// <summary>
        /// Determines if an alive cell dies before the next generation.
        /// </summary>
        /// <param name="field"></param>
        public void CheckAlive(string[,] field)
        {
            int neigboursCountOfAlive = 0;
            bool wrappedX = false;
            bool wrappedY = false;

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
                                if (neighbourX == -1)
                                {
                                    neighbourX = field.GetLength(0) - 1;
                                    wrappedX = true;
                                }
                                if (neighbourY == -1)
                                {
                                    neighbourY = field.GetLength(1) - 1;
                                    wrappedY = true;
                                }
                                if (field[neighbourX % field.GetLength(0), neighbourY % field.GetLength(1)] == "X")
                                {
                                    neigboursCountOfAlive++;
                                }
                                if (neighbourX == i && neighbourY == j && neigboursCountOfAlive > 0)
                                {
                                    neigboursCountOfAlive--;
                                }
                                if (wrappedY)
                                {
                                    neighbourY = j - 1;
                                    wrappedY = false;
                                }
                            }
                            if (wrappedX)
                            {
                                neighbourX = i - 1;
                                wrappedX = false;
                            }
                        }
                        if (neigboursCountOfAlive < 2 || neigboursCountOfAlive > 3)
                        {
                            cellsToDieX.Add(i);
                            cellsToDieY.Add(j);
                        }
                        neigboursCountOfAlive = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Determines if a dead cell is reborn before the next generation.
        /// </summary>
        /// <param name="field"></param>
        public void CheckDead(string[,] field)
        {
            int neigboursCountOfDead = 0;
            bool wrappedX = false;
            bool wrappedY = false;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == ".")
                    {
                        for (int neighbourX = i - 1; neighbourX <= i + 1; neighbourX++)
                        {
                            for (int neighbourY = j - 1; neighbourY <= j + 1; neighbourY++)
                            {
                                if (neighbourX == -1)
                                {
                                    neighbourX = field.GetLength(0) - 1;
                                    wrappedX = true;
                                }
                                if (neighbourY == -1)
                                {
                                    neighbourY = field.GetLength(1) - 1;
                                    wrappedY = true;
                                }
                                if (field[neighbourX % field.GetLength(0), neighbourY % field.GetLength(1)] == "X")
                                {
                                    neigboursCountOfDead++;
                                }
                                if (wrappedY)
                                {
                                    neighbourY = j - 1;
                                    wrappedY = false;
                                }
                            }

                            if (wrappedX)
                            {
                                neighbourX = i - 1;
                                wrappedX = false;
                            }
                        }

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

        /// <summary>
        /// Removes or creates new cells according to the rules.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string[,] FieldRefresh(string[,] field)
        {
            for (int i = 0; i < cellsToDieX.Count; i++)
            {
                field[cellsToDieX[i], cellsToDieY[i]] = ".";
            }
            for (int i = 0; i < cellsToBeBornX.Count; i++)
            {
                field[cellsToBeBornX[i], cellsToBeBornY[i]] = "X";
            }
            cellsToBeBornX.Clear();
            cellsToBeBornY.Clear();
            cellsToDieX.Clear();
            cellsToDieY.Clear();
            return field;
        }
    }
}
