using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    public class Library : ILibrary
    {
        /// <summary>
        /// Method to spawn a glider pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a glider.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a glider.</param>
        /// <returns>Returns an instance of the GameFieldModel class with a glider seeded in it.</returns>
        public GameFieldModel SpawnGlider(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int i = locationX; i < locationX + 3; i++)
            {
                for (int j = locationY; j < locationY + 3; j++)
                {
                    switch (i - locationX)
                    {
                        case 0:
                            if (j - locationY == 2)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 1:
                            if (j - locationY == 0 || j - locationY == 2)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 2:
                            if (j - locationY == 1 || j - locationY == 2)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;
                    }
                }
            }
            return gameField;
        }

        /// <summary>
        /// Method to spawn a light-weight spaceship pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a light-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a light-weight spaceship.</param>
        /// <returns>Returns an instance of the GameFieldModel class with a light-weight spaceship seeded in it.</returns>
        public GameFieldModel SpawnLightWeight(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int i = locationX; i < locationX + 5; i++)
            {
                for (int j = locationY; j < locationY + 4; j++)
                {
                    switch (i - locationX)
                    {
                        case 0:
                            if (j - locationY == 0 || j - locationY == 2)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 1:
                            if (j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 2:
                            if (j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 3:
                            if (j - locationY == 0 || j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 4:
                            if (j - locationY == 1 || j - locationY == 2 || j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;
                    }
                }
            }
            return gameField;
        }

        /// <summary>
        /// Method to spawn a middle-weight spaceship pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a middle-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a middle-weight spaceship.</param>
        /// <returns>Returns an instance of the GameFieldModel class with a middle-weight spaceship seeded in it.</returns>
        public GameFieldModel SpawnMiddleWeight(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int i = locationX; i < locationX + 6; i++)
            {
                for (int j = locationY; j < locationY + 5; j++)
                {
                    switch (i - locationX)
                    {
                        case 0:
                            if (j - locationY == 1 || j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 1:
                            if (j - locationY == 0)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 2:
                            if (j - locationY == 0 || j - locationY == 4)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 3:
                            if (j - locationY == 0)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 4:
                            if (j - locationY == 0 || j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 5:
                            if (j - locationY == 0 || j - locationY == 1 || j - locationY == 2)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;
                    }
                }
            }
            return gameField;
        }

        /// <summary>
        /// Method to spawn a heavy-weight spaceship pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a heavy-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a heavy-weight spaceship.</param>
        /// <returns>Returns an instance of the GameFieldModel class with a heavy-weight spaceship seeded in it.</returns>
        public GameFieldModel SpawnHeavyWeight(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int i = locationX; i < locationX + 7; i++)
            {
                for (int j = locationY; j < locationY + 5; j++)
                {
                    switch (i - locationX)
                    {
                        case 0:
                            if (j - locationY == 1 || j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 1:
                            if (j - locationY == 0)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 2:
                            if (j - locationY == 0 || j - locationY == 4)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 3:
                            if (j - locationY == 0 || j - locationY == 4)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 4:
                            if (j - locationY == 0)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 5:
                            if (j - locationY == 0 || j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 6:
                            if (j - locationY == 0 || j - locationY == 1 || j - locationY == 2)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;
                    }
                }
            }
            return gameField;
        }

        /// <summary>
        /// Method to spawn Gosper's glider gun.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of the Gosper's glider gun.</param>
        /// <param name="locationY">Vertical location of the upper left corner of Gosper's glider gun.</param>
        /// <returns>Returns an instance of the GameFieldModel class with the Gosper's glider gun seeded in it.</returns>
        public GameFieldModel SpawnGosperGliderGun(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int i = locationX; i < locationX + 37; i++)
            {
                for (int j = locationY; j < locationY + 10; j++)
                {
                    switch (i - locationX)
                    {
                        case 1:
                            if (j - locationY == 5 || j - locationY == 6)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 2:
                            if (j - locationY == 5 || j - locationY == 6)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 11:
                            if (j - locationY == 5 || j - locationY == 6 || j - locationY == 7)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 12:
                            if (j - locationY == 4 || j - locationY == 8)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 13:
                            if (j - locationY == 3 || j - locationY == 9)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 14:
                            if (j - locationY == 3 || j - locationY == 9)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 15:
                            if (j - locationY == 6)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 16:
                            if (j - locationY == 4 || j - locationY == 8)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 17:
                            if (j - locationY == 5 || j - locationY == 6 || j - locationY == 7)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 18:
                            if (j - locationY == 6)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 21:
                            if (j - locationY == 3 || j - locationY == 4 || j - locationY == 5)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 22:
                            if (j - locationY == 3 || j - locationY == 4 || j - locationY == 5)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 23:
                            if (j - locationY == 2 || j - locationY == 6)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 25:
                            if (j - locationY == 1 || j - locationY == 2 || j - locationY == 6 || j - locationY == 7)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 35:
                            if (j - locationY == 3 || j - locationY == 4)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 36:
                            if (j - locationY == 3 || j - locationY == 4)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;
                    }
                }
            }
            return gameField;
        }

        /// <summary>
        /// Method to spawn Simkin's glider gun.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of the Simkin's glider gun.</param>
        /// <param name="locationY">Vertical location of the upper left corner of the Simkin's glider gun.</param>
        /// <returns>Returns an instance of the GameFieldModel class with the Simkin's glider gun seeded in it.</returns>
        public GameFieldModel SpawnSimkinGliderGun(GameFieldModel gameField, int locationX, int locationY)
        {
            for (int i = locationX; i < locationX + 36; i++)
            {
                for (int j = locationY; j < locationY + 24; j++)
                {
                    switch (i - locationX)
                    {
                        case 2:
                            if (j - locationY == 2 || j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 3:
                            if (j - locationY == 2 || j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 6:
                            if (j - locationY == 5 || j - locationY == 6)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 7:
                            if (j - locationY == 5 || j - locationY == 6)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 9:
                            if (j - locationY == 2 || j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 10:
                            if (j - locationY == 2 || j - locationY == 3)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 22:
                            if (j - locationY == 19 || j - locationY == 20)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 23:
                            if (j - locationY == 12 || j - locationY == 13 || j - locationY == 14 || j - locationY == 19 || j - locationY == 21)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 24:
                            if (j - locationY == 11 || j - locationY == 14 || j - locationY == 21)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 25:
                            if (j - locationY == 11 || j - locationY == 14 || j - locationY == 21 || j - locationY == 22)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 27:
                            if (j - locationY == 11)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 28:
                            if (j - locationY == 11 || j - locationY == 15)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 29:
                            if (j - locationY == 12 || j - locationY == 14)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 30:
                            if (j - locationY == 13)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 33:
                            if (j - locationY == 13 || j - locationY == 14)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;

                        case 34:
                            if (j - locationY == 13 || j - locationY == 14)
                            {
                                gameField.GameField[i % gameField.Length, j % gameField.Width] = AliveCellSymbol;
                            }
                            break;
                    }
                }
            }
            return gameField;
        }
    }
}
