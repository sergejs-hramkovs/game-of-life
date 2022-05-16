using GameOfLife.Interfaces;
using GameOfLife.Models;

namespace GameOfLife
{
    /// <summary>
    /// The Render class deals with the rendering of the User Interface.
    /// </summary>
    [Serializable]
    public class Renderer : IRenderer
    {
        private IMainEngine _engine;

        /// <summary>
        /// Method to inject the Engine class into the Renderer class.
        /// </summary>
        /// <param name="engine"></param>
        public void Injection(IMainEngine engine)
        {
            _engine = engine;
        }

        /// <summary>
        /// Method to iterate through an array of UI menu lines and to dispaly them.
        /// </summary>
        /// <param name="menuLines">An array of UI menu lines.</param>
        public void MenuRenderer(string[] menuLines, bool wrongInput = false, bool fileNotFound = false,
            bool noSavedGames = false, bool clearScreen = false, bool multipleGames = false, bool newLine = true, bool gameOver = false)
        {
            if (clearScreen)
            {
                Console.Clear();
            }

            foreach (string line in menuLines)
            {
                if (line == StringConstants.WrongInputPhrase)
                {
                    if (wrongInput)
                    {
                        Console.WriteLine(line);
                        wrongInput = false;
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
                else if (line == StringConstants.GameOverPhrase)
                {
                    if (gameOver)
                    {
                        Console.WriteLine(line);
                        gameOver = false;
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
                else if (line == StringConstants.FileNotFoundPhrase)
                {
                    if (fileNotFound)
                    {
                        Console.WriteLine(line);
                        fileNotFound = false;
                    }
                }
                else if (line == StringConstants.NoSavedGamesPhrase)
                {
                    if (noSavedGames)
                    {
                        Console.WriteLine(line);
                        noSavedGames = false;
                    }
                }
                else if (line == StringConstants.EnterNewGameNumbersPhrase)
                {
                    if (multipleGames)
                    {
                        Console.WriteLine(line);
                    }
                }
                else if (line == menuLines[menuLines.Length - 1])
                {
                    if (!newLine)
                    {
                        Console.Write(line);
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                }
                else
                {
                    Console.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// Method to render several rows of Game Fields.
        /// </summary>
        /// <param name="multipleGames">An object that contains a list of Game Fields.</param>
        public void GridOfFieldsRenderer(MultipleGamesModel multipleGames, bool clearScreen = false)
        {
            if (clearScreen)
            {
                Console.Clear();
            }

            for (int rowNumber = 0; rowNumber < multipleGames.NumberOfRows; rowNumber++)
            {
                RowOfFieldsRenderer(multipleGames, rowNumber);
            }
        }

        /// <summary>
        /// Method to create and render the titles of the Game Fields in the Multiple Games Mode.
        /// </summary>
        /// <param name="multipleGames">An object of the MultiplGamesModel class that contains the list of all Game Fields.</param>
        /// <param name="rowNumber">The number of the Game Field row currently displayed.</param>
        /// <returns>Returns true - the fact that the whole row of titles was rendered.</returns>
        private static bool TitleRenderer(MultipleGamesModel multipleGames, int rowNumber)
        {
            int gameNumber;
            string indentation = "";
            string titleString;
            gameNumber = multipleGames.NumberOfHorizontalFields * rowNumber;
            Console.WriteLine();
            for (int fieldNumber = 0; fieldNumber < multipleGames.NumberOfHorizontalFields; fieldNumber++)
            {
                if (multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[gameNumber]].AliveCellsNumber == 0)
                {
                    for (int k = 0; k < multipleGames.ListOfGames[0].Length * 2 + 6 - StringConstants.FieldIsDeadPhrase.Length; k++)
                    {
                        indentation += " ";
                    }

                    Console.Write(StringConstants.FieldIsDeadPhrase + indentation);
                    indentation = "";
                }
                else
                {
                    titleString = $"  Game #{multipleGames.GamesToBeDisplayed[gameNumber]}. " +
                        $"Alive: {multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[gameNumber]].AliveCellsNumber}";
                    for (int k = 0; k < multipleGames.ListOfGames[0].Length * 2 + 6 - titleString.Length; k++)
                    {
                        indentation += " ";
                    }

                    Console.Write(titleString + indentation);
                    indentation = "";
                }

                gameNumber++;
            }

            Console.WriteLine();
            return true;
        }

        /// <summary>
        /// Method to display several Game Fields horizontally.
        /// </summary>
        /// <param name="multipleGames">An instance of the MultipleGamesModel class.</param>
        /// <param name="rowNumber">The number of the Game Field row currently displayed.</param>
        /// <returns>Returns the number of how many fields are displayed horizontally.</returns>
        private void RowOfFieldsRenderer(MultipleGamesModel multipleGames, int rowNumber)
        {
            bool titleRendered = false;
            Console.WriteLine();
            for (int yCoordinate = 0; yCoordinate < multipleGames.ListOfGames[0].Width; yCoordinate++)
            {
                if (!titleRendered && _engine.MultipleGamesMode)
                {
                    titleRendered = TitleRenderer(multipleGames, rowNumber);
                }

                for (int fieldNumber = 0; fieldNumber < multipleGames.NumberOfHorizontalFields; fieldNumber++)
                {
                    Console.Write(" ");
                    for (int xCoordinate = 0; xCoordinate < multipleGames.ListOfGames[0].Length; xCoordinate++)
                    {
                        if (multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[fieldNumber + multipleGames.NumberOfHorizontalFields * rowNumber]].AliveCellsNumber == 0 && _engine.InitializationFinished)
                        {
                            Console.Write(" " + StringConstants.GameOverCellSymbol);
                        }
                        else
                        {
                            Console.Write(" " + multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[fieldNumber + multipleGames.NumberOfHorizontalFields * rowNumber]].GameField[xCoordinate, yCoordinate]);
                        }
                    }

                    Console.Write("     ");
                }

                Console.WriteLine();
            }
        }
    }
}
