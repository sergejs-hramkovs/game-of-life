using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    /// <summary>
    /// The Render class deals with the rendering of the User Interface.
    /// </summary>
    [Serializable]
    public class Renderer : IRenderer
    {
        /// <summary>
        /// Method to iterate through an array of UI menu lines and to dispaly them.
        /// </summary>
        /// <param name="menuLines">An array of UI menu lines.</param>
        public void MenuRenderer(string[] menuLines, bool wrongInput = false, bool fileNotFound = false,
            bool noSavedGames = false, bool clearScreen = false, bool multipleGames = false, bool newLine = true)
        {
            if (clearScreen)
            {
                Console.Clear();
            }

            foreach (string line in menuLines)
            {
                if (line == WrongInputPhrase)
                {
                    if (wrongInput)
                    {
                        Console.WriteLine(line);
                        wrongInput = false;
                    }
                }
                else if (line == FileNotFoundPhrase)
                {
                    if (fileNotFound)
                    {
                        Console.WriteLine(line);
                        fileNotFound = false;
                    }
                }
                else if (line == NoSavedGamesPhrase)
                {
                    if (noSavedGames)
                    {
                        Console.WriteLine(line);
                        noSavedGames = false;
                    }
                }
                else if (line == EnterNewGameNumbersPhrase)
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
        /// Method that draws the field.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="dead">Parameter to render the field with '+' when the whoel field is dead.</param>
        public void RenderField(GameFieldModel gameField, bool dead = false)
        {
            Console.WriteLine();
            if (!dead)
            {
                for (int yCoordinate = 0; yCoordinate < gameField.Width; yCoordinate++)
                {
                    Console.Write(" ");
                    for (int xCoordinate = 0; xCoordinate < gameField.Length; xCoordinate++)
                    {
                        Console.Write(" " + gameField.GameField[xCoordinate, yCoordinate]);
                    }

                    Console.WriteLine();
                }
            }
            else
            {
                for (int yCoordinate = 0; yCoordinate < gameField.Width; yCoordinate++)
                {
                    for (int xCoordinate = 0; xCoordinate < gameField.Length; xCoordinate++)
                    {
                        Console.Write(" " + GameOverCellSymbol);
                    }

                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Method to display several Game Fields horizontally.
        /// </summary>
        /// <param name="multipleGames">An instance of the MultipleGamesModel class.</param>
        /// <param name="rowNumber">The number of the Game Field row currently displayed.</param>
        /// <returns>Returns the number of how many fields are displayed horizontally.</returns>
        public void RenderMultipleHorizontalFields(MultipleGamesModel multipleGames, int rowNumber)
        {
            int gameNumber;
            bool titleRendered = false;
            string indentation = "";
            string titleString;
            Console.WriteLine();
            gameNumber = multipleGames.NumberOfHorizontalFields * rowNumber;
            for (int yCoordinate = 0; yCoordinate < multipleGames.ListOfGames[0].Width; yCoordinate++)
            {
                if (!titleRendered)
                {
                    Console.WriteLine();
                    for (int fieldNumber = 0; fieldNumber < multipleGames.NumberOfHorizontalFields; fieldNumber++)
                    {
                        if (multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[gameNumber]].AliveCellsNumber == 0)
                        {
                            for (int k = 0; k < multipleGames.ListOfGames[0].Length * 2 + 6 - FieldIsDeadPhrase.Length; k++)
                            {
                                indentation += " ";
                            }

                            Console.Write(FieldIsDeadPhrase + indentation);
                            indentation = "";
                        }
                        else
                        {
                            titleString = $"  Game #{multipleGames.GamesToBeDisplayed[gameNumber]}. Alive: {multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[gameNumber]].AliveCellsNumber}";

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
                    titleRendered = true;
                }

                for (int fieldNumber = 0; fieldNumber < multipleGames.NumberOfHorizontalFields; fieldNumber++)
                {
                    Console.Write(" ");
                    for (int xCoordinate = 0; xCoordinate < multipleGames.ListOfGames[0].Length; xCoordinate++)
                    {
                        if (multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[fieldNumber + multipleGames.NumberOfHorizontalFields * rowNumber]].AliveCellsNumber == 0)
                        {
                            Console.Write(" " + GameOverCellSymbol);
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

        /// <summary>
        /// Method to render several rows of Game Fields.
        /// </summary>
        /// <param name="multipleGames">An object that contains a list of Game Fields.</param>
        public void RenderGridOfFields(MultipleGamesModel multipleGames)
        {
            for (int rowNumber = 0; rowNumber < multipleGames.NumberOfRows; rowNumber++)
            {
                RenderMultipleHorizontalFields(multipleGames, rowNumber);
            }
        }
    }
}
