namespace GameOfLife
{
    public class Iteration
    {
        private List<(int x, int y)> _cellsToDie = new List<(int x, int y)>();
        private List<(int x, int y)> _cellsToBeBorn = new List<(int x, int y)>();

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
        private void CheckAlive(string[,] field)
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
                            _cellsToDie.Add((i, j));
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
        private void CheckDead(string[,] field)
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
                            _cellsToBeBorn.Add((i, j));
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
            foreach ((int x, int y) cell in _cellsToDie)
            {
                field[cell.x, cell.y] = ".";
            }
            foreach ((int x, int y) cell in _cellsToBeBorn)
            {
                field[cell.x, cell.y] = "X";
            }
            _cellsToBeBorn.Clear();
            _cellsToDie.Clear();
            return field;
        }
    }
}
