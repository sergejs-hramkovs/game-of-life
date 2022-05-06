using GameOfLife.Interfaces;
using GameOfLife.Models;
using System.IO;
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
        public int RenderMultipleHorizontalFields(MultipleGamesModel multipleGames, int rowNumber)
        {
            int numberOfHorizontalFields = 2;
            int i;
            bool titleRendered = false;
            string indentation = "";
            string titleString;
            Console.WriteLine();
            switch (multipleGames.ListOfGames[0].Length)
            {
                case 25:
                    numberOfHorizontalFields = 3;
                    break;

                case 20:
                    numberOfHorizontalFields = 3;
                    break;

                case 15:
                    numberOfHorizontalFields = 4;
                    break;

                case 10:
                    numberOfHorizontalFields = 6;
                    break;
            }
            i = numberOfHorizontalFields * rowNumber;

            for (int yCoordinate = 0; yCoordinate < multipleGames.ListOfGames[0].Width; yCoordinate++)
            {
                if (!titleRendered)
                {
                    Console.WriteLine();
                    for (int fieldNumber = 0; fieldNumber < numberOfHorizontalFields; fieldNumber++)
                    {
                        if (multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[i]].AliveCellsNumber == 0)
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
                            titleString = $"  Game #{multipleGames.GamesToBeDisplayed[i]}. Alive: {multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[i]].AliveCellsNumber}";

                            for (int k = 0; k < multipleGames.ListOfGames[0].Length * 2 + 6 - titleString.Length; k++)
                            {
                                indentation += " ";
                            }

                            Console.Write(titleString + indentation);
                            indentation = "";
                        }

                        i++;
                    }

                    Console.WriteLine();
                    titleRendered = true;
                }

                for (int fieldNumber = 0; fieldNumber < numberOfHorizontalFields; fieldNumber++)
                {
                    Console.Write(" ");
                    for (int xCoordinate = 0; xCoordinate < multipleGames.ListOfGames[0].Length; xCoordinate++)
                    {
                        if (multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[fieldNumber + numberOfHorizontalFields * rowNumber]].AliveCellsNumber == 0)
                        {
                            Console.Write(" " + GameOverCellSymbol);
                        }
                        else
                        {
                            Console.Write(" " + multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[fieldNumber + numberOfHorizontalFields * rowNumber]].GameField[xCoordinate, yCoordinate]);
                        }
                    }

                    Console.Write("     ");
                }

                Console.WriteLine();
            }

            return numberOfHorizontalFields;
        }

        /// <summary>
        /// Method for rendering the UI when all the cells on the field are dead.
        /// </summary>
        /// <param name="generation">Parameter that represents the generation number.</param>
        public void GameOverRender(int generation)
        {
            Console.Clear();
            for (int dashNumber = 0; dashNumber < 5; dashNumber++)
            {
                Console.WriteLine(DashesConstant);
            }

            Console.WriteLine(WholeFieldDeadPhrase);
            Console.WriteLine($"\n Generations survived: {generation}");
        }

        /// <summary>
        /// Method for rendering the UI for choosing which saved game file to load.
        /// </summary>
        /// <param name="numberOfFiles">The number of saved game files currently present in the folder.</param>
        /// <param name="filePath">Parameter that stores the path to the folder with the saved games.</param>
        /// <param name="wrongInput">Parameter that represents if there was an attempt of wrong input.</param>
        public void ChooseFileToLoadMenuRender(int numberOfFiles, string filePath, bool wrongInput)
        {
            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine(" ### Choose which saved game to load ###");
            Console.WriteLine($"\n # There are currently {numberOfFiles} files");
            Console.WriteLine("\n--------------");
            RenderFileNames(filePath);
            Console.WriteLine("--------------");
            Console.WriteLine("\n # Choose the number of the file");
            if (wrongInput)
            {
                Console.WriteLine("\n--------------");
                Console.WriteLine(WrongInputPhrase);
                Console.WriteLine("--------------");
            }

            Console.Write("\n # Choice: ");
        }

        /// <summary>
        /// Method to display all the names of files curently present in the folder.
        /// </summary>
        /// <param name="filePath">The location of the folder.</param>
        private void RenderFileNames(string filePath)
        {
            foreach (string file in Directory.GetFiles(filePath))
            {
                Console.WriteLine(" - " + Path.GetFileName(file));
            }
        }
    }
}
