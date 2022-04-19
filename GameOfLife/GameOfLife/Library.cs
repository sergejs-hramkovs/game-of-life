using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Library
    {
        private string[,] _fieldArray;

        public Library(string[,] fieldArray)
        {
            _fieldArray = fieldArray;
        }

        /// <summary>
        /// Method to spawn a glider pattern.
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <returns></returns>
        public string[,] SeedGlider(int locationX, int locationY)
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
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 1:
                            if (j - locationY == 0 || j - locationY == 2)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 2:
                            if (j - locationY == 1 || j - locationY == 2)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;
                    }
                }
            }
            return _fieldArray;
        }

        /// <summary>
        /// Method to spawn a light-weight spaceship pattern.
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <returns></returns>
        public string[,] SeedLightweight(int locationX, int locationY)
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
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 1:
                            if (j - locationY == 3)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 2:
                            if (j - locationY == 3)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 3:
                            if (j - locationY == 0 || j - locationY == 3)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 4:
                            if (j - locationY == 1 || j - locationY == 2 || j - locationY == 3)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;
                    }
                }
            }
            return _fieldArray;
        }

        /// <summary>
        /// Method to spawn a middle-weight spaceship pattern.
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <returns></returns>
        public string[,] SeedMiddleweight(int locationX, int locationY)
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
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 1:
                            if (j - locationY == 0)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 2:
                            if (j - locationY == 0 || j - locationY == 4)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 3:
                            if (j - locationY == 0)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 4:
                            if (j - locationY == 0 || j - locationY == 3)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 5:
                            if (j - locationY == 0 || j - locationY == 1 || j - locationY == 2)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;
                    }
                }
            }
            return _fieldArray;
        }

        /// <summary>
        /// Method to spawn a heavy-weight spaceship pattern.
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <returns></returns>
        public string[,] SeedHeavyweight(int locationX, int locationY)
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
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 1:
                            if (j - locationY == 0)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 2:
                            if (j - locationY == 0 || j - locationY == 4)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 3:
                            if (j - locationY == 0 || j - locationY == 4)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 4:
                            if (j - locationY == 0)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 5:
                            if (j - locationY == 0 || j - locationY == 3)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;

                        case 6:
                            if (j - locationY == 0 || j - locationY == 1 || j - locationY == 2)
                            {
                                _fieldArray[i % _fieldArray.GetLength(0), j % _fieldArray.GetLength(1)] = "X";
                            }
                            break;
                    }
                }
            }
            return _fieldArray;
        }
    }
}
