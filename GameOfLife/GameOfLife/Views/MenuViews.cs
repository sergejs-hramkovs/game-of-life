namespace GameOfLife.Views
{
    /// <summary>
    /// The MenuViews class store string arrays of menu lines for each menu.
    /// </summary>
    public static class MenuViews
    {
        public static string[] MainMenu { get; } = {
            StringConstants.FileNotFoundPhrase,
            StringConstants.WrongInputPhrase,
            StringConstants.NoSavedGamesPhrase,
            " ### Welcome to the Game of Life! ###",
            "\n # Choose the field size:",
            " 1. 3x3",
            " 2. 5x5",
            " 3. 10x10",
            " 4. 20x20",
            " 5. 75x40",
            " 6. Custom",
            "\n # To load the field from the file press 'L'",
            " # To load Glider Gun Mode press 'G'",
            " # To load Multiple Games Mode press 'M'",
            "\n # Press 'F1' to read the rules and the description of the game"
        };

        public static string[] FieldSeedingChoiceChoiceMenu { get; } = {
            "\n ### Choose the field seeding type ###",
            "\n 1. Seed the field manually",
            " 2. Seed the field automatically and randomly",
            " 3. Choose cell patterns from the library"
        };

        public static string[] LibraryMenu { get; } = {
            StringConstants.WrongInputPhrase,
            "\n ### Choose an object from the library ###",
            "\n 1. Spawn a glider",
            " 2. Spawn a light-weight spaceship",
            " 3. Spawn a middle-weight spaceship",
            " 4. Spawn a heavy-weight spaceship",
            "\n # To stop seeding press 'Esc'"
        };

        public static string[] GliderGunModeMenu { get; } = {
            StringConstants.WrongInputPhrase,
            " ### The Glider Gun Mode ###",
            "\n Choose the type of the glider gun:",
            "\n 1. Gosper's glider gun",
            " 2. Simkin's glider gun.",
            "\n # Press 'G' to turn off the Glider Gun Mode"
        };

        public static string[] PauseMenu { get; } = {
            "\n # To save the current game state to a file press 'S'",
            " # To restart the game press 'R'",
            StringConstants.EnterNewGameNumbersPhrase,
            "\n # Press any other key to continue",
            "\n # Press 'Esc' to exit"
        };

        public static string[] ExitMenu { get; } = {
            "\n # Press 'R' to restart",
            " # Press 'Esc' to exit"
        };

        public static string[] BlankUI { get; } = {
            " Loading...",
            "\n\n\n Generation: 0",
            "\n\n\n"
        };

        public static string[] RulesPage { get; } = {
            "\n ### Game of Life ###",
            "\n The Game of Life, also known simply as Life, is a cellular automaton devised by the British mathematician John Horton Conway in 1970.",
            " It is a zero - player game, meaning that its evolution is determined by its initial state, requiring no further input. ",
            " One interacts with the Game of Life by creating an initial configuration and observing how it evolves.",
            " It is Turing complete and can simulate a universal constructor or any other Turing machine.",
            "\n ## Rules ##",
            "\n The universe of the Game of Life is an infinite, two - dimensional orthogonal grid of square cells, ",
            " each of which is in one of two possible states, live or dead(or populated and unpopulated, respectively).",
            " Every cell interacts with its eight neighbours, which are the cells that are horizontally, vertically, or diagonally adjacent. ",
            " At each step in time, the following transitions occur:",
            "\n # Any live cell with fewer than two live neighbours dies, as if by underpopulation.",
            " # Any live cell with two or three live neighbours lives on to the next generation.",
            " # Any live cell with more than three live neighbours dies, as if by overpopulation.",
            " # Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.",
            "\n These rules, which compare the behavior of the automaton to real life, can be condensed into the following:",
            "\n Any live cell with two or three live neighbours survives.",
            " # Any dead cell with three live neighbours becomes a live cell.",
            " # All other live cells die in the next generation.Similarly, all other dead cells stay dead.",
            "\n The initial pattern constitutes the seed of the system.",
            " The first generation is created by applying the above rules simultaneously to every cell in the seed, live or dead; ",
            " births and deaths occur simultaneously, and the discrete moment at which this happens is sometimes called a tick. ",
            " Each generation is a pure function of the preceding one.The rules continue to be applied repeatedly to create further generations.",
            "\n # Press any key to go back"
        };

        public static string[] MultipleGamesModeMenu { get; } = {
            "### Multiple Games Mode ###",
            "\n # 1. Enter numbers manually",
            " # 2. Random numbers"
        };

        public static string[] MultipleGamesModeFieldSizeChoiceMenu { get; } = {
            " ### Multiple Games Mode ###",
            "\n # Choose the field size:",
            "\n 1. 10x10 - 24 fields on the screen",
            " 2. 15x15 - 12 fields on the screen",
            " 3. 20x20 - 6 fields on the screen",
            " 4. 25x25 - 6 fields on the screen"
        };

        public static string[] MultipleGamesModeGamesQuantityMenu { get; } = {
            " ### Multiple Games Mode ###",
            "\n# Enter how many games to run (100-10000): "
        };

        public static string[] LoadGameMenu { get; } = {
            " ### Game loading menu ###",
            "\n # Choose what kind of games to load",
            "\n 1. Single game",
            " 2. Multiple games"
        };

        public static string[] GameOverUI { get; set; } = {
            StringConstants.DashesConstant,
            StringConstants.DashesConstant,
            StringConstants.DashesConstant,
            StringConstants.DashesConstant,
            StringConstants.DashesConstant,
            StringConstants.WholeFieldDeadPhrase,
            ""
        };

        public static string[] WrongInputFileMenu { get; } = {
            StringConstants.DashesConstant,
            StringConstants.WrongInputPhrase,
            StringConstants.DashesConstant
        };

        public static string[] SingleGameUI { get; set; }

        public static string[] MultiGameUI { get; set; }

        public static string[] ChooseFileMenu { get; set; }

        public static List<string> FileNames { get; set; } = new();
    }
}
