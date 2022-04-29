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
    }
}
