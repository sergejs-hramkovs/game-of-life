using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    /// <summary>
    /// The RulesApplier class deals with the apllication of the rules of the game to cells.
    /// </summary>
    [Serializable]
    public class RulesApplier : IRulesApplier
    {
        private List<(int xCoordinate, int yCoordinate)> _cellsToDie = new List<(int xCoordinate, int yCoordinate)>();
        private List<(int xCoordinate, int yCoordinate)> _cellsToBeBorn = new List<(int xCoordinate, int yCoordinate)>();
        private int neighboursCount;

        /// <summary>
        /// Method to go through all the field cells and call the corresponding method if the cell is alive or dead.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="disableWrappingAroundField">Parameter that shows if field's wrapping around is disabled.</param>
        public void IterateThroughGameFieldCells(GameFieldModel gameField, bool disableWrappingAroundField = false)
        {
            for (int xCoordinate = 0; xCoordinate < gameField.Length; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < gameField.Width; yCoordinate++)
                {
                    if (gameField.GameField[xCoordinate, yCoordinate] == AliveCellSymbol)
                    {
                        ActOnAliveCell(gameField, xCoordinate, yCoordinate, disableWrappingAroundField);
                    }
                    else
                    {
                        ActOnDeadCell(gameField, xCoordinate, yCoordinate, disableWrappingAroundField);
                    }
                }
            }
        }

        /// <summary>
        /// Method to count the neighbours of an alive cell and add it to the list of cells to be die in the next generation if it meets the rules.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="xCoordinate">Horizontal coordinate of a cell which neighbours are being counted.</param>
        /// <param name="yCoordinate">Vertical coordinate of a cell which neighbours are being counted</param>
        /// <param name="disableWrappingAroundField">Parameter that shows if field's wrapping around is disabled.</param>
        private void ActOnAliveCell(GameFieldModel gameField, int xCoordinate, int yCoordinate, bool disableWrappingAroundField)
        {
            neighboursCount = CountNeighbourCells(gameField, xCoordinate, yCoordinate, false);
            switch (disableWrappingAroundField)
            {
                case false:
                    if (neighboursCount < 2 || neighboursCount > 3)
                    {
                        _cellsToDie.Add((xCoordinate, yCoordinate));
                    }

                    break;

                case true:
                    if (neighboursCount < 2 || neighboursCount > 3 || xCoordinate == gameField.Length - 1 || yCoordinate == gameField.Width - 1)
                    {
                        _cellsToDie.Add((xCoordinate, yCoordinate));
                    }

                    break;
            }
        }

        /// <summary>
        /// Method to count the neighbours of a dead cell and add it to the list of cells to be reborn in the next generation if it meets the rules.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="xCoordinate">Horizontal coordinate of a cell which neighbours are being counted.</param>
        /// <param name="yCoordinate">Vertical coordinate of a cell which neighbours are being counted</param>
        /// <param name="disableWrappingAroundField">Parameter that shows if field's wrapping around is disabled.</param>
        private void ActOnDeadCell(GameFieldModel gameField, int xCoordinate, int yCoordinate, bool disableWrappingAroundField)
        {
            neighboursCount = CountNeighbourCells(gameField, xCoordinate, yCoordinate, true);
            switch (disableWrappingAroundField)
            {
                case false:
                    if (neighboursCount == 3)
                    {
                        _cellsToBeBorn.Add((xCoordinate, yCoordinate));
                    }

                    break;

                case true:
                    if (neighboursCount == 3 && xCoordinate != gameField.Length - 1 && yCoordinate != gameField.Width - 1)
                    {
                        _cellsToBeBorn.Add((xCoordinate, yCoordinate));
                    }

                    break;
            }
        }

        /// <summary>
        /// Method to count each cell's alive and dead neighbours to later determine which will die and which will be reborn.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="xCoordinate">Horizontal coordinate of a cell which neighbours are being counted.</param>
        /// <param name="yCoordinate">Vertical coordinate of a cell which neighbours are being counted</param>
        /// <param name="checkDeadCell">Parameter which shows whether the cell, which neighbours are being counted, is alive or dead.</param>
        /// <returns>Returns the number of cell's neighbours.</returns>
        private int CountNeighbourCells(GameFieldModel gameField, int xCoordinate, int yCoordinate, bool checkDeadCell)
        {
            bool wrappedX = false;
            bool wrappedY = false;
            neighboursCount = 0;
            for (int neighbourX = xCoordinate - 1; neighbourX <= xCoordinate + 1; neighbourX++)
            {
                for (int neighbourY = yCoordinate - 1; neighbourY <= yCoordinate + 1; neighbourY++)
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

                    if (!checkDeadCell && (neighbourX == xCoordinate && neighbourY == yCoordinate && neighboursCount > 0))
                    {
                        neighboursCount--;
                    }

                    if (wrappedY)
                    {
                        neighbourY = yCoordinate - 1;
                        wrappedY = false;
                    }
                }

                if (wrappedX)
                {
                    neighbourX = xCoordinate - 1;
                    wrappedX = false;
                }
            }

            return neighboursCount;
        }

        /// <summary>
        /// Removes or creates new cells according to the rules.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <returns>Returns an array of a gamefield after applying the rules of the game.</returns>
        public void FieldRefresh(GameFieldModel gameField)
        {
            foreach ((int xCoordinate, int yCoordinate) cell in _cellsToDie)
            {
                gameField.GameField[cell.xCoordinate, cell.yCoordinate] = DeadCellSymbol;
            }

            foreach ((int xCoordinate, int yCoordinate) cell in _cellsToBeBorn)
            {
                gameField.GameField[cell.xCoordinate, cell.yCoordinate] = AliveCellSymbol;
            }

            _cellsToBeBorn.Clear();
            _cellsToDie.Clear();
        }
    }
}