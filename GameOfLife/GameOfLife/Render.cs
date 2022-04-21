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

        /// <summary>
        /// Method for initial rendering of the game field.
        /// </summary>
        /// <param name="length">Horizontal dimension of a gamefield.</param>
        /// <param name="width">Vertical dimension of a gamefield.</param>
        /// <param name="inputField">An array of a gamefield.</param>
        /// <param name="loaded">Boolean parameter that represents whether the field was loaded from the file.</param>
        public static void InitialRender(int length, int width, string[,] inputField, bool loaded)
        {
            gameField = inputField;
            field = new Field(length, width);
            iteration = new Iteration();
            engine = new Engine();
            
            // This place needs to be had a look.
            if (!loaded)
            {
                gameField = field.CreateField();
                Console.Clear();
                field.DrawField(gameField);
                gameField = field.SeedField();
            }
            Console.Clear();
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Method for rendering the gamefield between the generations.
        /// </summary>
        /// <param name="delay">Delay between generations in miliseconds</param>
        /// <returns>Returns a tuple containing an array of the game field, number of alive and dead cells and the generation number.</returns>
        public static Tuple<string[,], int, int, int> RuntimeRender(int delay)
        {
            aliveCells = engine.CountAlive(gameField);
            deadCells = engine.CountDead(gameField);
            
            Console.WriteLine("# Press ESC to stop");
            Console.WriteLine("# Press Spacebar to pause");
            Console.WriteLine("# Change the delay using left and right arrows");
            Console.WriteLine($"\nGeneration: {generation}");
            Console.WriteLine($"Alive cells: {aliveCells}({(int)Math.Round(aliveCells / (double)(deadCells + aliveCells) * 100.0)}%)   ");
            Console.WriteLine($"Dead cells: {deadCells}   ");
            Console.WriteLine($"Current delay between generations: {delay / 1000.0} seconds  ");
            Console.WriteLine($"Number of generations per second: {Math.Round(1 / (delay / 1000.0), 2)}   ");
            iteration.CheckCells(gameField);
            gameField = iteration.FieldRefresh(gameField);
            field.DrawField(gameField);
            generation++;
            return new Tuple<string[,], int, int, int>(gameField, aliveCells, deadCells, generation - 1);
        }
    }
}
