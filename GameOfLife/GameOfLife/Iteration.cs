namespace GameOfLife
{
    public class Iteration
    {
        private List<Tuple<int, int>> cellsToDie = new List<Tuple<int, int>>();
        private List<Tuple<int, int>> cellsToBeBorn = new List<Tuple<int, int>>();

        /// <summary>
        /// Applies checks for alive and dead cells according to the rules.
        /// </summary>
        /// <param name="field">An array of a gamefield.</param>
        public void CheckCells(string[,] field)
        {
            CheckAlive(field);
            CheckDead(field);
        }

        /// <summary>
        /// Determines if an alive cell dies before the next generation.
        /// </summary>
        /// <param name="field">An array of a gamefield.</param>
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
                            cellsToDie.Add(new Tuple<int, int>(i, j));
                        }
                        neigboursCountOfAlive = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Determines if a dead cell is reborn before the next generation.
        /// </summary>
        /// <param name="field">An array of a gamefield.</param>
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
                            cellsToBeBorn.Add(new Tuple<int, int>(i, j));
                        }
                        neigboursCountOfDead = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Removes or creates new cells according to the rules.
        /// </summary>
        /// <param name="field">An array of a gamefield.</param>
        /// <returns>Returns an array of a gamefield after applying the rules of the game.</returns>
        public string[,] FieldRefresh(string[,] field)
        {
            foreach (Tuple<int, int> cell in cellsToDie)
            {
                field[cell.Item1, cell.Item2] = ".";
            }
            foreach (Tuple<int, int> cell in cellsToBeBorn)
            {
                field[cell.Item1, cell.Item2] = "X";
            }
            cellsToBeBorn.Clear();
            cellsToDie.Clear();
            return field;
        }
    }
}
