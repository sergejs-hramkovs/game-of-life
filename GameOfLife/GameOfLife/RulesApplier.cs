using GameOfLife.Interfaces;
using static GameOfLife.Phrases;

namespace GameOfLife
{
    public class RulesApplier : IRulesApplier
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
        /// Applies checks for alive and dead cells according to the rules, with dead borders.
        /// </summary>
        /// <param name="field">An array of a gamefield.</param>
        public void CheckCellsDeadBorder(string[,] field)
        {
            CheckAliveDeadBorder(field);
            CheckDeadDeadBorder(field);
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
                    if (field[i, j] == AliveCellSymbol)
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
                                if (field[neighbourX % field.GetLength(0), neighbourY % field.GetLength(1)] == AliveCellSymbol)
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
                    if (field[i, j] == DeadCellSymbol)
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
                                if (field[neighbourX % field.GetLength(0), neighbourY % field.GetLength(1)] == AliveCellSymbol)
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
                field[cell.x, cell.y] = DeadCellSymbol;
            }
            foreach ((int x, int y) cell in _cellsToBeBorn)
            {
                field[cell.x, cell.y] = AliveCellSymbol;
            }
            _cellsToBeBorn.Clear();
            _cellsToDie.Clear();
            return field;
        }

        /// <summary>
        /// Modification of CheckAlive method with the addition of dead borders.
        /// </summary>
        /// <param name="field">An array of a gamefield.</param>
        private void CheckAliveDeadBorder(string[,] field)
        {
            {
                int neigboursCountOfAlive = 0;
                bool wrappedX = false;
                bool wrappedY = false;

                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        if (field[i, j] == AliveCellSymbol)
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
                                    if (field[neighbourX % field.GetLength(0), neighbourY % field.GetLength(1)] == AliveCellSymbol)
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
                            if (neigboursCountOfAlive < 2 || neigboursCountOfAlive > 3 || i == field.GetLength(0) - 1 || j == field.GetLength(1) - 1)
                            {
                                _cellsToDie.Add((i, j));
                            }
                            neigboursCountOfAlive = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Modification of CheckDead method with the addition of dead borders.
        /// </summary>
        /// <param name="field">An array of a gamefield.</param>
        private void CheckDeadDeadBorder(string[,] field)
        {
            int neigboursCountOfDead = 0;
            bool wrappedX = false;
            bool wrappedY = false;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == DeadCellSymbol)
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
                                if (field[neighbourX % field.GetLength(0), neighbourY % field.GetLength(1)] == AliveCellSymbol)
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
                        if (neigboursCountOfDead == 3 && i != field.GetLength(0) - 1 && j != field.GetLength(1) - 1)
                        {
                            _cellsToBeBorn.Add((i, j));
                        }
                        neigboursCountOfDead = 0;
                    }
                }
            }
        }
    }
}
