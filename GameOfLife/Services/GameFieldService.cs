using GameOfLife.Entities.Models;
using GameOfLife.Interfaces;
using GameOfLife.Models;

namespace GameOfLife
{
    /// <summary>
    /// The RulesApplier class deals with the apllication of the rules of the game to cells.
    /// </summary>
    [Serializable]
    public class GameFieldService : IGameFieldService
    {
        private List<(int xCoordinate, int yCoordinate)> _cellsToDie = new List<(int xCoordinate, int yCoordinate)>();
        private List<(int xCoordinate, int yCoordinate)> _cellsToBeBorn = new List<(int xCoordinate, int yCoordinate)>();
        private int neighboursCount;

        /// <summary>
        /// Method to go through all the field cells and call the corresponding method if the cell is alive or dead.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="disableWrappingAroundField">Parameter that shows if field's wrapping around is disabled.</param>
        public void IterateThroughGameFieldCells(SingleGameField gameField, bool disableWrappingAroundField = false)
        {
            for (int xCoordinate = 0; xCoordinate < gameField.Length; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < gameField.Width; yCoordinate++)
                {
                    if (gameField.GameField[xCoordinate, yCoordinate] == StringConstants.AliveCellSymbol)
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
        private void ActOnAliveCell(SingleGameField gameField, int xCoordinate, int yCoordinate, bool disableWrappingAroundField)
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
        private void ActOnDeadCell(SingleGameField gameField, int xCoordinate, int yCoordinate, bool disableWrappingAroundField)
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
        private int CountNeighbourCells(SingleGameField gameField, int xCoordinate, int yCoordinate, bool checkDeadCell)
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

                    if (gameField.GameField[neighbourX % gameField.Length, neighbourY % gameField.Width] == StringConstants.AliveCellSymbol)
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
        public void RefreshField(SingleGameField gameField)
        {
            foreach ((int xCoordinate, int yCoordinate) cell in _cellsToDie)
            {
                gameField.GameField[cell.xCoordinate, cell.yCoordinate] = StringConstants.DeadCellSymbol;
            }

            foreach ((int xCoordinate, int yCoordinate) cell in _cellsToBeBorn)
            {
                gameField.GameField[cell.xCoordinate, cell.yCoordinate] = StringConstants.AliveCellSymbol;
            }

            _cellsToBeBorn.Clear();
            _cellsToDie.Clear();
        }

        /// <summary>
        /// Method to count the number of alive cells on one field.
        /// </summary>
        /// <param name="gameField">A GameFieldModel object that contains the Game Field.</param>
        private static void CountAliveCells(SingleGameField gameField)
        {
            gameField.AliveCellsNumber = 0;
            for (int xCoordinate = 0; xCoordinate < gameField.Length; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < gameField.Width; yCoordinate++)
                {
                    if (gameField.GameField[xCoordinate, yCoordinate] == StringConstants.AliveCellSymbol)
                    {
                        gameField.AliveCellsNumber++;
                    }
                }
            }
        }

        /// <summary>
        /// Method to count total alive cells number on all the fields in the Multiple Games Mode.
        /// </summary>
        /// <param name="multipleGamesField">A MultipleGamesModel object that contains the list of Game Fields.</param>
        private static void CountTotalAliveCells(MultipleGamesField multipleGamesField)
        {
            multipleGamesField.TotalCellsAlive = 0;
            foreach (var field in multipleGamesField.ListOfGames)
            {
                CountAliveCells(field);
                multipleGamesField.TotalCellsAlive += field.AliveCellsNumber;
            }
        }

        /// <summary>
        /// Method to replace rendered dead fields with alive ones in the list of fields to be displayed.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        private static void RemoveDeadFieldsFromRendering(MultipleGamesField multipleGames, List<int> aliveFields)
        {
            for (int rowNumber = 0; rowNumber < multipleGames.NumberOfRows; rowNumber++)
            {
                for (int i = rowNumber * multipleGames.NumberOfHorizontalFields; i < multipleGames.NumberOfHorizontalFields + rowNumber * multipleGames.NumberOfHorizontalFields; i++)
                {
                    if ((multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[i]].AliveCellsNumber == 0) && (multipleGames.NumberOfFieldsAlive >= multipleGames.NumberOfGamesToBeDisplayed))
                    {
                        foreach (int aliveField in aliveFields)
                        {
                            if (!multipleGames.GamesToBeDisplayed.Contains(aliveField))
                            {
                                multipleGames.GamesToBeDisplayed[i] = aliveField;
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to perform the required runtime actions, like applying game rules and counting alive cells of each field.
        /// </summary>
        public void PerformRuntimeCalculations(GameModel game)
        {
            for (int gameNumber = 0; gameNumber < game.MultipleGamesField.TotalNumberOfGames; gameNumber++)
            {
                _gameFieldService.IterateThroughGameFieldCells(game.MultipleGamesField.ListOfGames[gameNumber], game.GameDetails.IsGliderGunMode);
                _gameFieldService.RefreshField(_engine.MultipleGames.ListOfGames[gameNumber]);
                CountAliveCells(_engine.MultipleGames.ListOfGames[gameNumber]);

                if (_engine.MultipleGames.ListOfGames[gameNumber].AliveCellsNumber == 0 && !_engine.MultipleGames.DeadFields.Contains(gameNumber))
                {
                    _engine.MultipleGames.NumberOfFieldsAlive--;
                    _engine.MultipleGames.DeadFields.Add(gameNumber);
                    if (_engine.MultipleGames.AliveFields.Contains(gameNumber))
                    {
                        _engine.MultipleGames.AliveFields.Remove(gameNumber);
                    }
                }
                else if (_engine.MultipleGames.ListOfGames[gameNumber].AliveCellsNumber > 0 && !_engine.MultipleGames.AliveFields.Contains(gameNumber))
                {
                    _engine.MultipleGames.AliveFields.Add(gameNumber);
                }
            }

            _engine.MultipleGames.Generation++;
        }
    }
}