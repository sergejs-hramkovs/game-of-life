using GameOfLife.Interfaces;
using GameOfLife.Models;
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
        public void DetermineCellsDestiny(GameFieldModel gameField, bool disableWrappingAroundField)
        {
            for (int i = 0; i < gameField.Length; i++)
            {
                for (int j = 0; j < gameField.Width; j++)
                {
                    if (gameField.GameField[i, j] == AliveCellSymbol)
                    {
                        neighboursCount = CountNeighbourCells(gameField, i, j, false);
                        switch (disableWrappingAroundField)
                        {
                            case false:
                                if (neighboursCount < 2 || neighboursCount > 3)
                                {
                                    _cellsToDie.Add((i, j));
                                }
                                break;

                            case true:
                                if (neighboursCount < 2 || neighboursCount > 3 || i == gameField.Length - 1 || j == gameField.Width - 1)
                                {
                                    _cellsToDie.Add((i, j));
                                }
                                break;
                        }
                    }
                    else
                    {
                        neighboursCount = CountNeighbourCells(gameField, i, j, true);
                        switch (disableWrappingAroundField)
                        {
                            case false:
                                if (neighboursCount == 3)
                                {
                                    _cellsToBeBorn.Add((i, j));
                                }
                                break;

                            case true:
                                if (neighboursCount == 3 && i != gameField.Length - 1 && j != gameField.Width - 1)
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
        private int CountNeighbourCells(GameFieldModel gameField, int x, int y, bool checkDeadCell)
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
                        neighbourX = gameField.Length - 1;
                        wrappedX = true;
                    }
                    if (neighbourY == -1)
                    {
                        neighbourY = gameField.Width - 1;
                        wrappedY = true;
                    }
                    if (gameField.GameField[neighbourX % gameField.Length, neighbourY % gameField.Width] == AliveCellSymbol)
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
        public void FieldRefresh(GameFieldModel gameField)
        {
            foreach ((int x, int y) cell in _cellsToDie)
            {
                gameField.GameField[cell.x, cell.y] = DeadCellSymbol;
            }
            foreach ((int x, int y) cell in _cellsToBeBorn)
            {
                gameField.GameField[cell.x, cell.y] = AliveCellSymbol;
            }
            _cellsToBeBorn.Clear();
            _cellsToDie.Clear();
        }
    }
}