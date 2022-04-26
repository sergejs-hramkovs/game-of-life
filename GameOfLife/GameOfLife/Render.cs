using GameOfLife.Interfaces;
using static GameOfLife.StringConstants;

namespace GameOfLife
{
    public class Render : IRender
    {
        /// <summary>
        /// Method that draws the field.
        /// </summary>
        /// <param name="field">An array of a gamefield.</param>
        public void RenderField(string[,] field, bool dead = false)
        {
            Console.WriteLine();
            for (int i = 0; i < field.GetLength(1); i++)
            {
                for (int j = 0; j < field.GetLength(0); j++)
                {
                    if (!dead)
                    {
                        Console.Write(" " + field[j, i]);
                    }
                    else
                    {
                        Console.Write(" +");
                    }

                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Method for rendering the field seeding menu.
        /// </summary>
        public void SeedFieldMenuRender()
        {
            Console.WriteLine("\n1. Seed the field manually");
            Console.WriteLine("2. Seed the field automatically and randomly");
            Console.WriteLine("3. Choose cell patterns from the library");
        }

        /// <summary>
        /// Method for rendering the library selection menu.
        /// </summary>
        public void LibraryMenuRender()
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
        public void MainMenuRender(bool wrongInput, bool fileReadingError)
        {
            Console.Clear();
            if (fileReadingError)
            {
                Console.WriteLine(FileNotFoundPhrase);
                Thread.Sleep(1000);
            }
            else if (wrongInput)
            {
                Console.WriteLine(WrongInputPhrase);
            }
            else
            {
                Console.WriteLine("Welcome to the Game of Life!");
            }
            Console.WriteLine("\nChoose the field size:");
            Console.WriteLine("1. 3x3");
            Console.WriteLine("2. 5x5");
            Console.WriteLine("3. 10x10");
            Console.WriteLine("4. 20x20");
            Console.WriteLine("5. 75x40");
            Console.WriteLine("6. Custom");
            Console.WriteLine("\n# To load the field from the file press 'L'");
            Console.WriteLine("# To load Glider Gun Mode press 'G'");
            Console.WriteLine("\n# Press 'F1' to read the rules and the description of the game");
        }

        /// <summary>
        /// Method for rendering the glider gun menu.
        /// </summary>
        public void GliderGunMenuRender()
        {
            Console.Clear();
            Console.WriteLine("The Glider Gun Mode");
            Console.WriteLine("\n1. 40x30 (The best size for a glider gun)");
            Console.WriteLine("\n# Press 'G' to turn off the Glider Gun Mode");
        }

        /// <summary>
        /// Method for rendering the pause menu.
        /// </summary>
        public void PauseMenuRender()
        {
            Console.WriteLine("\n# To save the current game state to a file press 'S'");
            Console.WriteLine("# To restart the game press 'R'");
            Console.WriteLine("# Press any other key to cancel saving and continue with the game");
            Console.WriteLine("# Press 'Esc' to exit");
        }

        /// <summary>
        /// Method for rendering the exit menu.
        /// </summary>
        public void ExitMenuRender()
        {
            Console.WriteLine("\n# Press 'R' to restart");
            Console.WriteLine("# Press 'Esc' to exit");
        }

        /// <summary>
        /// Method for rendering the UI during the runtime.
        /// </summary>
        /// <param name="aliveCells">The number of alive cells on the field.</param>
        /// <param name="deadCells">The number of dead cells on the field.</param>
        /// <param name="generation">The number of the current generation.</param>
        /// <param name="delay">Delay between generations in miliseconds.</param>
        public void RuntimeUIRender(int aliveCells, int deadCells, int generation, int delay)
        {
            Console.WriteLine("# Press ESC to stop");
            Console.WriteLine("# Press Spacebar to pause");
            Console.WriteLine("# Change the delay using left and right arrows");
            Console.WriteLine($"\nGeneration: {generation}");
            Console.WriteLine($"Alive cells: {aliveCells}({(int)Math.Round(aliveCells / (double)(deadCells + aliveCells) * 100.0)}%)   ");
            Console.WriteLine($"Dead cells: {deadCells}   ");
            Console.WriteLine($"Current delay between generations: {delay / 1000.0} seconds  ");
            Console.WriteLine($"Number of generations per second: {Math.Round(1 / (delay / 1000.0), 2)}   ");
        }

        /// <summary>
        /// Method for rendering blank UI for 0 generation.
        /// </summary>
        public void BlankUIRender()
        {
            Console.WriteLine("Loading...");
            Console.WriteLine("\n\n\n\n\n\n\nGeneration: 0");
        }

        /// <summary>
        /// Method to print the rules and the description of the game.
        /// </summary>
        public void PrintRules()
        {
            Console.Clear();
            Console.WriteLine("\n### Game of Life ###");
            Console.WriteLine("\nThe Game of Life, also known simply as Life, is a cellular automaton devised by the British mathematician John Horton Conway in 1970." +
                "\nIt is a zero - player game, meaning that its evolution is determined by its initial state, requiring no further input. " +
                "\nOne interacts with the Game of Life by creating an initial configuration and observing how it evolves." +
                "\nIt is Turing complete and can simulate a universal constructor or any other Turing machine.");
            Console.WriteLine("\n## Rules ##");
            Console.WriteLine("\nThe universe of the Game of Life is an infinite, two - dimensional orthogonal grid of square cells, " +
                "\neach of which is in one of two possible states, live or dead(or populated and unpopulated, respectively)." +
                "\nEvery cell interacts with its eight neighbours, which are the cells that are horizontally, vertically, or diagonally adjacent. " +
                "\nAt each step in time, the following transitions occur:");
            Console.WriteLine("\n# Any live cell with fewer than two live neighbours dies, as if by underpopulation.");
            Console.WriteLine("# Any live cell with two or three live neighbours lives on to the next generation.");
            Console.WriteLine("# Any live cell with more than three live neighbours dies, as if by overpopulation.");
            Console.WriteLine("# Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.");
            Console.WriteLine("\nThese rules, which compare the behavior of the automaton to real life, can be condensed into the following:");
            Console.WriteLine("\nAny live cell with two or three live neighbours survives.");
            Console.WriteLine("# Any dead cell with three live neighbours becomes a live cell.");
            Console.WriteLine("# All other live cells die in the next generation.Similarly, all other dead cells stay dead.");
            Console.WriteLine("\nThe initial pattern constitutes the seed of the system." +
                "\nThe first generation is created by applying the above rules simultaneously to every cell in the seed, live or dead; " +
                "\nbirths and deaths occur simultaneously, and the discrete moment at which this happens is sometimes called a tick. " +
                "\nEach generation is a pure function of the preceding one.The rules continue to be applied repeatedly to create further generations.");
            Console.WriteLine("\n# Press any key to go back");
            Console.ReadKey();
        }

        /// <summary>
        /// Method for rendering the UI when all the cells on the field are dead.
        /// </summary>
        /// <param name="generation"></param>
        public void GameOverRender(int generation)
        {
            Console.Clear();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("--------------------------------");
            }
            Console.WriteLine("\n### THE WHOLE FIELD IS DEAD! ###");
            Console.WriteLine($"\nGenerations survived: {generation - 1}");
        }
    }
}
