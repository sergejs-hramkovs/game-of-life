using GameOfLife.Entities.Enums;
using GameOfLife.Entities.Models;
using GameOfLife.Interfaces;
using GameOfLife.Models;

namespace GameOfLife
{
    /// <summary>
    /// The Render class deals with the rendering of the User Interface.
    /// </summary>
    [Serializable]
    public class RenderingService : IRenderingService
    {
        public RenderingService()
        {

        }

        public void RenderMenu(MenuType menuType)
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
                        ChangeColorWrite(line, newLine: false);
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
                        ChangeColorWrite(line, newLine: false);
                        gameOver = false;
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
                else if (line == StringConstants.NoSavedGamesPhrase)
                {
                    if (noSavedGames)
                    {
                        ChangeColorWrite(line, newLine: false);
                        noSavedGames = false;
                    }
                }
                else if (line == StringConstants.EnterNewGameNumbersPhrase)
                {
                    if (isMultipleGamesMode)
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
        /// Method to change text color, write text and change the color back.
        /// </summary>
        /// <param name="textToWrite">Parameter that represents a string to be written.</param>
        public void ChangeColorWrite(string textToWrite, bool newLine)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (newLine)
            {
                Console.WriteLine(textToWrite);
            }
            else
            {
                Console.Write(textToWrite);
            }

            Console.ForegroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Method to render several rows of Game Fields.
        /// </summary>
        /// <param name="multipleGames">An object that contains a list of Game Fields.</param>
        /// <param name="clearScreen">Parameter that states if the screen is to be cleared, 'false' by default.</param>
        public void RenderGridOfFields(GameModel game, bool clearScreen = false)
        {
            if (clearScreen)
            {
                Console.Clear();
            }

            for (int rowNumber = 0; rowNumber < game.MultipleGamesField.NumberOfRows; rowNumber++)
            {
                RenderRowOfFields(game, rowNumber);
            }
        }

        /// <summary>
        /// Method to create and render the titles of the Game Fields in the Multiple Games Mode.
        /// </summary>
        /// <param name="multipleGames">An object of the MultiplGamesModel class that contains the list of all Game Fields.</param>
        /// <param name="rowNumber">The number of the Game Field row currently displayed.</param>
        /// <returns>Returns true - the fact that the whole row of titles was rendered.</returns>
        private bool RenderTitle(MultipleGamesField multipleGames, int rowNumber)
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

                    ChangeColorWrite(StringConstants.FieldIsDeadPhrase, false);
                    Console.Write(indentation);
                    indentation = "";
                }
                else
                {
                    titleString = $" Game #{multipleGames.GamesToBeDisplayed[gameNumber]}. " +
                        $"Alive: {multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[gameNumber]].AliveCellsNumber}";
                    for (int k = 0; k < multipleGames.ListOfGames[0].Length * 2 + 6 - titleString.Length; k++)
                    {
                        indentation += " ";
                    }

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(titleString);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(indentation);
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
        private void RenderRowOfFields(GameModel game, int rowNumber)
        {
            bool titleRendered = false;
            Console.WriteLine();
            for (int yCoordinate = 0; yCoordinate < game.MultipleGamesField.ListOfGames[0].Width; yCoordinate++)
            {
                if (!titleRendered && game.GameDetails.IsMultipleGamesMode)
                {
                    titleRendered = RenderTitle(game.MultipleGamesField, rowNumber);
                }

                for (int fieldNumber = 0; fieldNumber < game.MultipleGamesField.NumberOfHorizontalFields; fieldNumber++)
                {
                    Console.Write(" ");
                    for (int xCoordinate = 0; xCoordinate < game.MultipleGamesField.ListOfGames[0].Length; xCoordinate++)
                    {
                        if (game.MultipleGamesField.ListOfGames[game.MultipleGamesField.GamesToBeDisplayed[fieldNumber + game.MultipleGamesField.NumberOfHorizontalFields * rowNumber]].AliveCellsNumber == 0 && game.GameDetails.InitializationFinished)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write(" " + StringConstants.GameOverCellSymbol);
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                        else
                        {
                            Console.Write(game.MultipleGamesField.ListOfGames[game.MultipleGamesField.GamesToBeDisplayed[fieldNumber + game.MultipleGamesField.NumberOfHorizontalFields * rowNumber]].GameField[xCoordinate, yCoordinate]);
                        }
                    }

                    Console.Write("     ");
                }

                Console.WriteLine();
            }
        }
    }
}
