using GameOfLife.Interfaces;
using GameOfLife.Models;

namespace GameOfLife
{
    /// <summary>
    /// The Library class deals with populating the Game Field with premade cell patterns.
    /// </summary>
    [Serializable]
    public class Library : ILibrary
    {
        /// <summary>
        /// Method to spawn a Glider pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a glider.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a glider.</param>
        public void SpawnGlider(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int xCoordinate = locationX; xCoordinate < locationX + 3; xCoordinate++)
            {
                for (int yCoordinate = locationY; yCoordinate < locationY + 3; yCoordinate++)
                {
                    switch (xCoordinate - locationX)
                    {
                        case 0:
                            if (yCoordinate - locationY == 2)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 1:
                            if (yCoordinate - locationY == 0 || yCoordinate - locationY == 2)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 2:
                            if (yCoordinate - locationY == 1 || yCoordinate - locationY == 2)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Method to spawn a Light-Weight Spaceship pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a light-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a light-weight spaceship.</param>
        public void SpawnLightWeight(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int xCoordinate = locationX; xCoordinate < locationX + 5; xCoordinate++)
            {
                for (int yCoordinate = locationY; yCoordinate < locationY + 4; yCoordinate++)
                {
                    switch (xCoordinate - locationX)
                    {
                        case 0:
                            if (yCoordinate - locationY == 0 || yCoordinate - locationY == 2)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 1:

                        case 2:
                            if (yCoordinate - locationY == 3)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 3:
                            if (yCoordinate - locationY == 0 || yCoordinate - locationY == 3)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 4:
                            if (yCoordinate - locationY == 1 || yCoordinate - locationY == 2 || yCoordinate - locationY == 3)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Method to spawn a Middle-Weight Spaceship pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a middle-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a middle-weight spaceship.</param>
        public void SpawnMiddleWeight(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int xCoordinate = locationX; xCoordinate < locationX + 6; xCoordinate++)
            {
                for (int yCoordinate = locationY; yCoordinate < locationY + 5; yCoordinate++)
                {
                    switch (xCoordinate - locationX)
                    {
                        case 0:
                            if (yCoordinate - locationY == 1 || yCoordinate - locationY == 3)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 1:
                            if (yCoordinate - locationY == 0)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 2:
                            if (yCoordinate - locationY == 0 || yCoordinate - locationY == 4)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 3:
                            if (yCoordinate - locationY == 0)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 4:
                            if (yCoordinate - locationY == 0 || yCoordinate - locationY == 3)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 5:
                            if (yCoordinate - locationY == 0 || yCoordinate - locationY == 1 || yCoordinate - locationY == 2)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Method to spawn a Heavy-Weight Spaceship pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a heavy-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a heavy-weight spaceship.</param>
        public void SpawnHeavyWeight(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int xCoordinate = locationX; xCoordinate < locationX + 7; xCoordinate++)
            {
                for (int yCoordinate = locationY; yCoordinate < locationY + 5; yCoordinate++)
                {
                    switch (xCoordinate - locationX)
                    {
                        case 0:
                            if (yCoordinate - locationY == 1 || yCoordinate - locationY == 3)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 1:
                            if (yCoordinate - locationY == 0)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 2:

                        case 3:
                            if (yCoordinate - locationY == 0 || yCoordinate - locationY == 4)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 4:
                            if (yCoordinate - locationY == 0)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 5:
                            if (yCoordinate - locationY == 0 || yCoordinate - locationY == 3)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 6:
                            if (yCoordinate - locationY == 0 || yCoordinate - locationY == 1 || yCoordinate - locationY == 2)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Method to spawn Gosper's Glider Gun.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of the Gosper's glider gun.</param>
        /// <param name="locationY">Vertical location of the upper left corner of Gosper's glider gun.</param>
        public void SpawnGosperGliderGun(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int xCoordinate = locationX; xCoordinate < locationX + 37; xCoordinate++)
            {
                for (int yCoordinate = locationY; yCoordinate < locationY + 10; yCoordinate++)
                {
                    switch (xCoordinate - locationX)
                    {
                        case 1:

                        case 2:
                            if (yCoordinate - locationY == 5 || yCoordinate - locationY == 6)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 11:
                            if (yCoordinate - locationY == 5 || yCoordinate - locationY == 6 || yCoordinate - locationY == 7)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 12:
                            if (yCoordinate - locationY == 4 || yCoordinate - locationY == 8)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 13:

                        case 14:
                            if (yCoordinate - locationY == 3 || yCoordinate - locationY == 9)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 15:
                            if (yCoordinate - locationY == 6)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 16:
                            if (yCoordinate - locationY == 4 || yCoordinate - locationY == 8)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 17:
                            if (yCoordinate - locationY == 5 || yCoordinate - locationY == 6 || yCoordinate - locationY == 7)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 18:
                            if (yCoordinate - locationY == 6)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 21:

                        case 22:
                            if (yCoordinate - locationY == 3 || yCoordinate - locationY == 4 || yCoordinate - locationY == 5)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 23:
                            if (yCoordinate - locationY == 2 || yCoordinate - locationY == 6)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 25:
                            if (yCoordinate - locationY == 1 || yCoordinate - locationY == 2 || yCoordinate - locationY == 6 || yCoordinate - locationY == 7)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 35:

                        case 36:
                            if (yCoordinate - locationY == 3 || yCoordinate - locationY == 4)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Method to spawn Simkin's Glider Gun.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of the Simkin's glider gun.</param>
        /// <param name="locationY">Vertical location of the upper left corner of the Simkin's glider gun.</param>
        public void SpawnSimkinGliderGun(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int xCoordinate = locationX; xCoordinate < locationX + 36; xCoordinate++)
            {
                for (int yCoordinate = locationY; yCoordinate < locationY + 24; yCoordinate++)
                {
                    switch (xCoordinate - locationX)
                    {
                        case 2:

                        case 3:
                            if (yCoordinate - locationY == 2 || yCoordinate - locationY == 3)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 6:

                        case 7:
                            if (yCoordinate - locationY == 5 || yCoordinate - locationY == 6)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 9:

                        case 10:
                            if (yCoordinate - locationY == 2 || yCoordinate - locationY == 3)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 22:
                            if (yCoordinate - locationY == 19 || yCoordinate - locationY == 20)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 23:
                            if (yCoordinate - locationY == 12 || yCoordinate - locationY == 13 || yCoordinate - locationY == 14 || yCoordinate - locationY == 19 || yCoordinate - locationY == 21)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 24:
                            if (yCoordinate - locationY == 11 || yCoordinate - locationY == 14 || yCoordinate - locationY == 21)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 25:
                            if (yCoordinate - locationY == 11 || yCoordinate - locationY == 14 || yCoordinate - locationY == 21 || yCoordinate - locationY == 22)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 27:
                            if (yCoordinate - locationY == 11)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 28:
                            if (yCoordinate - locationY == 11 || yCoordinate - locationY == 15)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 29:
                            if (yCoordinate - locationY == 12 || yCoordinate - locationY == 14)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 30:
                            if (yCoordinate - locationY == 13)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;

                        case 33:

                        case 34:
                            if (yCoordinate - locationY == 13 || yCoordinate - locationY == 14)
                            {
                                gameField.GameField[xCoordinate % gameField.Length, yCoordinate % gameField.Width] = StringConstants.AliveCellSymbol;
                            }

                            break;
                    }
                }
            }
        }
    }
}
