using GameOfLife.Interfaces;
using static GameOfLife.StringConstants;

namespace GameOfLife
{
    public class RulesApplier : IRulesApplier
    {
        private List<(int x, int y)> _cellsToDie = new List<(int x, int y)>();
        private List<(int x, int y)> _cellsToBeBorn = new List<(int x, int y)>();
        private int neighboursCount;

        /// <summary>
        /// Method to determine which cells die and which are reborn, judging by the number of alive neighbours.
        /// </summary>
        /// <param name="field">An array of the gamefield cells.</param>
        /// <param name="disableWrappingAroundField">Parameter if field's wrapping around is enabled.</param>
        public void DetermineCellsDestiny(string[,] field, bool disableWrappingAroundField)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == AliveCellSymbol)
                    {
                        neighboursCount = CountNeighbourCells(field, i, j, false);
                        switch (disableWrappingAroundField)
                        {
                            case false:
                                if (neighboursCount < 2 || neighboursCount > 3)
                                {
                                    _cellsToDie.Add((i, j));
                                }
                                break;

                            case true:
                                if (neighboursCount < 2 || neighboursCount > 3 || i == field.GetLength(0) - 1 || j == field.GetLength(1) - 1)
                                {
                                    _cellsToDie.Add((i, j));
                                }
                                break;
                        }
                    }
                    else
                    {
                        neighboursCount = CountNeighbourCells(field, i, j, true);
                        switch (disableWrappingAroundField)
                        {
                            case false:
                                if (neighboursCount == 3)
                                {
                                    _cellsToBeBorn.Add((i, j));
                                }
                                break;

                            case true:
                                if (neighboursCount == 3 && i != field.GetLength(0) - 1 && j != field.GetLength(1) - 1)
                                {
                                    _cellsToBeBorn.Add((i, j));
                                }
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to count each cell's alive and dead neighbours to later determine which will die and which will be reborn.
        /// </summary>
        /// <param name="field">An array of the gamefield cells.</param>
        /// <param name="x">Horizontal coordinate of a cell which neighbours are being counted.</param>
        /// <param name="y">Vertical coordinate of a cell which neighbours are being counted</param>
        /// <param name="checkDeadCell">Parameter which shows whether the cell, which neighbours are being counted, is alive or dead.</param>
        /// <returns>Returns the number of cell's neighbours.</returns>
        private int CountNeighbourCells(string[,] field, int x, int y, bool checkDeadCell)
        {
            bool wrappedX = false;
            bool wrappedY = false;
            neighboursCount = 0;

            for (int neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
            {
                for (int neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
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
                        neighboursCount++;
                    }
                    if (!checkDeadCell && (neighbourX == x && neighbourY == y && neighboursCount > 0))
                    {
                        neighboursCount--;
                    }
                    if (wrappedY)
                    {
                        neighbourY = y - 1;
                        wrappedY = false;
                    }
                }
                if (wrappedX)
                {
                    neighbourX = x - 1;
                    wrappedX = false;
                }
            }
            return neighboursCount;
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
    }
}