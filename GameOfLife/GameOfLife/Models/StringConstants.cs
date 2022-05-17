namespace GameOfLife
{
    /// <summary>
    /// The StringConstantsModel class stores various string constants that are used across the application.
    /// </summary>
    [Serializable]
    public static class StringConstants
    {
        public const string WrongInputPhrase = " ### Wrong Input! ### ";

        public const string NoSavedGamesPhrase = " ### There are no saved games! ###";

        public const string EnterLengthPhrase = "\n # Enter the length of the field: ";

        public const string EnterWidthPhrase = "\n # Enter the width of the field: ";

        public const string SuccessfullySavedPhrase = "\n ### The current game state has been successfully saved! Press any key to continue ###";

        public const string AliveCellSymbol = "X";

        public const char AliveCellSymbolChar = 'X';

        public const string DeadCellSymbol = ".";

        public const char DeadCellSymbolChar = '.';

        public const string GameOverCellSymbol = "+";

        public const string StopSeedingPhrase = "\n # To stop seeding enter 'stop'";

        public const string EnterXPhrase = "\n # Enter X coordinate: ";

        public const string EnterYPhrase = "\n # Enter Y coordinate: ";

        public const string StopWord = "stop";

        public const string FileNotFoundPhrase = " ### The file is missing! ###";

        public const string EnterGameNumberPhrase = " # Enter the number of a game: ";

        public const string GameOverPhrase = "\n ### GAME OVER! THE WHOLE FIELD IS DEAD! ###";

        public const string FieldIsDeadPhrase = "  THE FIELD IS DEAD ";

        public const string DashesConstant = "---------------------------------";

        public const string GameAlreadyChosenPhrase = "\n ### This number has already been chosen! ###";

        public const string SavedGamesFolderName = "SavedGames\\";

        public const string MultipleGamesModeSavedGamesFolderName = "SavedGames\\MultipleGamesMode\\";

        public const string EnterNewGameNumbersPhrase = " # To change the displayed games press 'N'";
    }
}
