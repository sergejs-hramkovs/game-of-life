using GameOfLife.Interfaces;
using static GameOfLife.StringConstants;

namespace GameOfLife
{
    public class Library : ILibrary
    {
        /// <summary>
        /// Method to spawn a glider pattern.
        /// </summary>
        /// <param name="locationX">Horizontal location of the upper left corner of a glider.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a glider.</param>
        /// <returns>Returns an array with gamefield elements with a glider seeded in it.</returns>
        public string[,] SpawnGlider(string[,] fieldArray, int locationX, int locationY)
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
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 1:
                            if (j - locationY == 0 || j - locationY == 2)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 2:
                            if (j - locationY == 1 || j - locationY == 2)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;
                    }
                }
            }
            return fieldArray;
        }

        /// <summary>
        /// Method to spawn a light-weight spaceship pattern.
        /// </summary>
        /// <param name="locationX">Horizontal location of the upper left corner of a light-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a light-weight spaceship.</param>
        /// <returns>Returns an array with gamefield elements with a light-weight spaceship seeded in it.</returns>
        public string[,] SpawnLightWeight(string[,] fieldArray, int locationX, int locationY)
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
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 1:
                            if (j - locationY == 3)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 2:
                            if (j - locationY == 3)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 3:
                            if (j - locationY == 0 || j - locationY == 3)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 4:
                            if (j - locationY == 1 || j - locationY == 2 || j - locationY == 3)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;
                    }
                }
            }
            return fieldArray;
        }

        /// <summary>
        /// Method to spawn a middle-weight spaceship pattern.
        /// </summary>
        /// <param name="locationX">Horizontal location of the upper left corner of a middle-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a middle-weight spaceship.</param>
        /// <returns>Returns an array with gamefield elements with a middle-weight spaceship seeded in it.</returns>
        public string[,] SpawnMiddleWeight(string[,] fieldArray, int locationX, int locationY)
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
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 1:
                            if (j - locationY == 0)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 2:
                            if (j - locationY == 0 || j - locationY == 4)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 3:
                            if (j - locationY == 0)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 4:
                            if (j - locationY == 0 || j - locationY == 3)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 5:
                            if (j - locationY == 0 || j - locationY == 1 || j - locationY == 2)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;
                    }
                }
            }
            return fieldArray;
        }

        /// <summary>
        /// Method to spawn a heavy-weight spaceship pattern.
        /// </summary>
        /// <param name="locationX">Horizontal location of the upper left corner of a heavy-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a heavy-weight spaceship.</param>
        /// <returns>Returns an array with gamefield elements with a heavy-weight spaceship seeded in it.</returns>
        public string[,] SpawnHeavyWeight(string[,] fieldArray, int locationX, int locationY)
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
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 1:
                            if (j - locationY == 0)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 2:
                            if (j - locationY == 0 || j - locationY == 4)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 3:
                            if (j - locationY == 0 || j - locationY == 4)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 4:
                            if (j - locationY == 0)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 5:
                            if (j - locationY == 0 || j - locationY == 3)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 6:
                            if (j - locationY == 0 || j - locationY == 1 || j - locationY == 2)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;
                    }
                }
            }
            return fieldArray;
        }

        /// <summary>
        /// Method to spawn a glider gun.
        /// </summary>
        /// <param name="locationX">Horizontal location of the upper left corner of a glider gun.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a glider gun.</param>
        /// <returns>Returns an array with gamefield elements with a glider gun seeded in it.</returns>
        public string[,] SpawnGliderGun(string[,] fieldArray, int locationX, int locationY)
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
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 2:
                            if (j - locationY == 5 || j - locationY == 6)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 11:
                            if (j - locationY == 5 || j - locationY == 6 || j - locationY == 7)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 12:
                            if (j - locationY == 4 || j - locationY == 8)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 13:
                            if (j - locationY == 3 || j - locationY == 9)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 14:
                            if (j - locationY == 3 || j - locationY == 9)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 15:
                            if (j - locationY == 6)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 16:
                            if (j - locationY == 4 || j - locationY == 8)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 17:
                            if (j - locationY == 5 || j - locationY == 6 || j - locationY == 7)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 18:
                            if (j - locationY == 6)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 21:
                            if (j - locationY == 3 || j - locationY == 4 || j - locationY == 5)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 22:
                            if (j - locationY == 3 || j - locationY == 4 || j - locationY == 5)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 23:
                            if (j - locationY == 2 || j - locationY == 6)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 25:
                            if (j - locationY == 1 || j - locationY == 2 || j - locationY == 6 || j - locationY == 7)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 35:
                            if (j - locationY == 3 || j - locationY == 4)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;

                        case 36:
                            if (j - locationY == 3 || j - locationY == 4)
                            {
                                fieldArray[i % fieldArray.GetLength(0), j % fieldArray.GetLength(1)] = AliveCellSymbol;
                            }
                            break;
                    }
                }
            }
            return fieldArray;
        }
    }
}
