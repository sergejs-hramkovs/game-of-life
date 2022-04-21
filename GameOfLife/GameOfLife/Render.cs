namespace GameOfLife
{
    public static class Render
    {
        static string[,] gameField;
        static int generation = 1;
        static int aliveCells;
        static int deadCells;
        static Field field;
        static Iteration iteration;
        static Engine engine;
        static Tuple<string[,], int, int, int> returnValues;

        /// <summary>
        /// Method that draws the field.
        /// </summary>
        /// <param name="field">An array of a gamefield.</param>
        public static void RenderField(string[,] field)
        {
            Console.WriteLine();
            for (int i = 0; i < field.GetLength(1); i++)
            {
                for (int j = 0; j < field.GetLength(0); j++)
                {
                    Console.Write(" " + field[j, i]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Method for initial rendering of the game field.
        /// </summary>
        /// <param name="length">Horizontal dimension of a gamefield.</param>
        /// <param name="width">Vertical dimension of a gamefield.</param>
        /// <param name="inputField">An array of a gamefield.</param>
        /// <param name="loaded">Boolean parameter that represents whether the field was loaded from the file.</param>
        /// <param name="gliderGunMode">Parameter to show whether the glider gun mode is on.</param>
        public static void InitialRender(int length, int width, string[,] inputField, bool loaded, bool gliderGunMode)
        {
            gameField = inputField;
            field = new Field(length, width);
            iteration = new Iteration();
            engine = new Engine();

            // This place needs to be had a look at.
            if (!loaded)
            {
                gameField = field.CreateField();
                Console.Clear();
                RenderField(gameField);
                gameField = field.SeedField(gliderGunMode);
            }
            Console.Clear();
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Method for rendering the gamefield between the generations.
        /// </summary>
        /// <param name="delay">Delay between generations in miliseconds</param>
        /// <param name="gliderGunMode">Parameter to enable the Glider Gun mode with dead borders rules.</param>
        /// <param name="resetGeneration">Parameter to rest the number of generation after restart.</param>
        /// <returns>Returns a tuple containing an array of the game field, number of alive and dead cells and the generation number.</returns>
        public static Tuple<string[,], int, int, int> RuntimeRender(int delay, bool gliderGunMode, bool resetGeneration)
        {
            aliveCells = engine.CountAlive(gameField);
            deadCells = engine.CountDead(gameField);
            if (resetGeneration)
            {
                generation = 1;
            }
            Console.WriteLine("# Press ESC to stop");
            Console.WriteLine("# Press Spacebar to pause");
            Console.WriteLine("# Change the delay using left and right arrows");
            Console.WriteLine($"\nGeneration: {generation}");
            Console.WriteLine($"Alive cells: {aliveCells}({(int)Math.Round(aliveCells / (double)(deadCells + aliveCells) * 100.0)}%)   ");
            Console.WriteLine($"Dead cells: {deadCells}   ");
            Console.WriteLine($"Current delay between generations: {delay / 1000.0} seconds  ");
            Console.WriteLine($"Number of generations per second: {Math.Round(1 / (delay / 1000.0), 2)}   ");
            if (gliderGunMode)
            {
                iteration.CheckCellsDeadBorder(gameField);
            }
            else
            {
                iteration.CheckCells(gameField);
            }
            gameField = iteration.FieldRefresh(gameField);
            RenderField(gameField);
            returnValues = new Tuple<string[,], int, int, int>(gameField, aliveCells, deadCells, generation);
            generation++;
            return returnValues;
        }

        /// <summary>
        /// Method for rendering the field seeding menu.
        /// </summary>
        public static void SeedFieldMenuRender()
        {
            Console.WriteLine("\n1. Seed the field manually");
            Console.WriteLine("2. Seed the field automatically and randomly");
            Console.WriteLine("3. Choose cell patterns from the library");
        }

        /// <summary>
        /// Method for rendering the library selection menu.
        /// </summary>
        public static void LibraryMenuRender()
        {
            Console.WriteLine("\n# To stop seeding press 'Esc'");
            Console.WriteLine("\n1. Spawn a glider");
            Console.WriteLine("2. Spawn a light-weight spaceship");
            Console.WriteLine("3. Spawn a middle-weight spaceship");
            Console.WriteLine("4. Spawn a heavy-weight spaceship");
        }

        /// <summary>
        /// Method for rendering field size and mode choosing menu.
        /// </summary>
        public static void FieldSizeMenuRender()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Game of Life!");
            Console.WriteLine("\nChoose the field size:");
            Console.WriteLine("1. 3x3");
            Console.WriteLine("2. 5x5");
            Console.WriteLine("3. 10x10");
            Console.WriteLine("4. 20x20");
            Console.WriteLine("5. 75x40");
            Console.WriteLine("6. Custom");
            Console.WriteLine("\n# To load the field from the file press 'L'");
            Console.WriteLine("# To load Glider Gun Mode press 'G'");
            Console.WriteLine("# Press 'F1' to read the rules and the description of the game");
        }

        /// <summary>
        /// Method for rendering the glider gun menu.
        /// </summary>
        public static void GliderGunMenuRender()
        {
            Console.Clear();
            Console.WriteLine("The Glider Gun Mode");
            Console.WriteLine("\n1. 40x30 (The best size for a glider gun)");
            Console.WriteLine("\n# Press 'G' to turn off the Glider Gun Mode");
        }

        /// <summary>
        /// Method for rendering the pause menu.
        /// </summary>
        public static void PauseRender()
        {
            Console.WriteLine("\n# To save the current game state to a file press 'S'");
            Console.WriteLine("# To restart the game press 'R'");
            Console.WriteLine("# Press any other key to cancel saving and continue with the game");
            Console.WriteLine("# Press 'Esc' to exit");
        }

        /// <summary>
        /// Method for rendering the exit menu.
        /// </summary>
        public static void ExitRender()
        {
            Console.WriteLine("\n# Press 'R' to restart");
            Console.WriteLine("# Press 'Esc' to exit");
        }
    }
}
