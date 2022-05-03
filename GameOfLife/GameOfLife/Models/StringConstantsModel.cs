using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public static class StringConstantsModel
    {
        public const string WrongInputPhrase = " ### Wrong Input! ### ";

        public const string NoSavedGamesPhrase = " ### There are no saved games! ###";

        public const string EnterLengthPhrase = "\n # Enter the length of the field: ";

        public const string EnterWidthPhrase = "\n # Enter the width of the field: ";

        public const string SuccessfullySavedPhrase = "\n ### The current game state has been successfully saved! Press any key to continue ###";

        public const string AliveCellSymbol = "X";

        public const string DeadCellSymbol = ".";

        public const string GameOverCellSymbol = "+";

        public const string StopSeedingPhrase = "\n # To stop seeding enter 'stop'";

        public const string EnterXPhrase = "\n # Enter X coordinate: ";

        public const string EnterYPhrase = "\n # Enter Y coordinate: ";

        public const string StopWord = "stop";

        public const string FileNotFoundPhrase = " ### The file is missing! ###";

        public const string EnterGameNumberPhrase = " # Enter the number of a game: ";

        public const string FieldDeadPhrase = "\n ### THE WHOLE FIELD IS DEAD! ###";

        public const string DashesConstant = "---------------------------------";

        public const string EnterTotalGamesNumberPhrase = "\n # Enter the number of games to be created (2-2000): ";

        public const string EnterLengthMultipleGamesPhrase = "\n # Enter the horizontal dimension of the field (3-30): ";

        public const string EnterWidthMultipleGamesPhrase = "\n # Enter the vertical dimension of the field (3-10): ";

        public const string EnterNumberOfGamesDisplayedPhrase = "\n # Enter the number of how many game will be displayed (2-4): ";

        public const string GameAlreadyChosenPhrase = "\n ### This number has already been chosen! ###";
    }
}
