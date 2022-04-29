using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    public class Render : IRender
    {
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
                for (int i = 0; i < gameField.Width; i++)
                {
                    Console.Write(" ");
                    for (int j = 0; j < gameField.Length; j++)
                    {
                        Console.Write(" " + gameField.GameField[j, i]);
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                for (int i = 0; i < gameField.Width; i++)
                {
                    for (int j = 0; j < gameField.Length; j++)
                    {
                        Console.Write(" " + GameOverCellSymbol);
                    }
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Method for rendering the field seeding menu.
        /// </summary>
        public void SeedFieldMenuRender()
        {
            Console.WriteLine("\n ### Choose the field seeding type ###");
            Console.WriteLine("\n 1. Seed the field manually");
            Console.WriteLine(" 2. Seed the field automatically and randomly");
            Console.WriteLine(" 3. Choose cell patterns from the library");
        }

        /// <summary>
        /// Method for rendering the library selection menu.
        /// </summary>
        public void LibraryMenuRender(bool wrongInput)
        {    
            if (wrongInput)
            {
                Console.WriteLine();
                Console.WriteLine(WrongInputPhrase);
            }
            else
            {
                Console.WriteLine("\n ### Choose an object from the library ###");
            }
            Console.WriteLine("\n 1. Spawn a glider");
            Console.WriteLine(" 2. Spawn a light-weight spaceship");
            Console.WriteLine(" 3. Spawn a middle-weight spaceship");
            Console.WriteLine(" 4. Spawn a heavy-weight spaceship");
            Console.WriteLine("\n # To stop seeding press 'Esc'");
        }

        /// <summary>
        /// Method for rendering the main menu.
        /// </summary>
        /// <param name="wrongInput">Parameter that represents if there was an attempt of wrong input.</param>
        /// <param name="fileReadingError">Parameter that represents if there was an error during loading from the file.</param>
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
                Console.WriteLine(" ### Welcome to the Game of Life! ###");
            }
            Console.WriteLine("\n # Choose the field size:");
            Console.WriteLine(" 1. 3x3");
            Console.WriteLine(" 2. 5x5");
            Console.WriteLine(" 3. 10x10");
            Console.WriteLine(" 4. 20x20");
            Console.WriteLine(" 5. 75x40");
            Console.WriteLine(" 6. Custom");
            Console.WriteLine("\n # To load the field from the file press 'L'");
            Console.WriteLine(" # To load Glider Gun Mode press 'G'");
            Console.WriteLine("\n # Press 'F1' to read the rules and the description of the game");
        }

        /// <summary>
        /// Method for rendering the glider gun mode menu.
        /// </summary>
        public void GliderGunModeRender(bool wrongInput)
        {
            Console.Clear();
            if (wrongInput)
            {
                Console.WriteLine(WrongInputPhrase);
            }
            else
            {
                Console.WriteLine(" ### The Glider Gun Mode ###");
            }
            Console.WriteLine("\n Choose the type of the glider gun:");
            Console.WriteLine("\n 1. Gosper's glider gun");
            Console.WriteLine(" 2. Simkin's glider gun.");
            Console.WriteLine("\n # Press 'G' to turn off the Glider Gun Mode");
        }

        /// <summary>
        /// Method for rendering the pause menu.
        /// </summary>
        public void PauseMenuRender()
        {
            Console.WriteLine("\n # To save the current game state to a file press 'S'");
            Console.WriteLine(" # To restart the game press 'R'");
            Console.WriteLine(" # Press any other key to cancel saving and continue with the game");
            Console.WriteLine(" # Press 'Esc' to exit");
        }

        /// <summary>
        /// Method for rendering the exit menu.
        /// </summary>
        public void ExitMenuRender()
        {
            Console.WriteLine("\n # Press 'R' to restart");
            Console.WriteLine(" # Press 'Esc' to exit");
        }

        /// <summary>
        /// Method for rendering the UI during the runtime.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class.</param>
        /// <param name="delay">Delay between generations in miliseconds.</param>
        public void RuntimeUIRender(GameFieldModel gameField, int delay)
        {
            Console.WriteLine(" # Press ESC to stop");
            Console.WriteLine(" # Press Spacebar to pause");
            Console.WriteLine(" # Change the delay using left and right arrows");
            Console.WriteLine($"\n Generation: {gameField.Generation}");
            Console.WriteLine($" Alive cells: {gameField.AliveCellsNumber}({(int)Math.Round(gameField.AliveCellsNumber / (double)gameField.Area * 100.0)}%)   ");
            Console.WriteLine($" Dead cells: {gameField.DeadCellsNumber}   ");
            Console.WriteLine($" Current delay between generations: {delay / 1000.0} seconds  ");
            Console.WriteLine($" Number of generations per second: {Math.Round(1 / (delay / 1000.0), 2)}   ");
        }

        /// <summary>
        /// Method for rendering blank UI for 0 generation.
        /// </summary>
        public void BlankUIRender()
        {
            Console.WriteLine(" Loading...");
            Console.WriteLine("\n\n\n Generation: 0");
            Console.WriteLine("\n\n\n");
        }

        /// <summary>
        /// Method to print the rules and the description of the game.
        /// </summary>
        public void PrintRules()
        {
            Console.Clear();
            Console.WriteLine("\n ### Game of Life ###");
            Console.WriteLine("\n The Game of Life, also known simply as Life, is a cellular automaton devised by the British mathematician John Horton Conway in 1970." +
                "\n It is a zero - player game, meaning that its evolution is determined by its initial state, requiring no further input. " +
                "\n One interacts with the Game of Life by creating an initial configuration and observing how it evolves." +
                "\n It is Turing complete and can simulate a universal constructor or any other Turing machine.");
            Console.WriteLine("\n ## Rules ##");
            Console.WriteLine("\n The universe of the Game of Life is an infinite, two - dimensional orthogonal grid of square cells, " +
                "\n each of which is in one of two possible states, live or dead(or populated and unpopulated, respectively)." +
                "\n Every cell interacts with its eight neighbours, which are the cells that are horizontally, vertically, or diagonally adjacent. " +
                "\n At each step in time, the following transitions occur:");
            Console.WriteLine("\n # Any live cell with fewer than two live neighbours dies, as if by underpopulation.");
            Console.WriteLine(" # Any live cell with two or three live neighbours lives on to the next generation.");
            Console.WriteLine(" # Any live cell with more than three live neighbours dies, as if by overpopulation.");
            Console.WriteLine(" # Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.");
            Console.WriteLine("\n These rules, which compare the behavior of the automaton to real life, can be condensed into the following:");
            Console.WriteLine("\n Any live cell with two or three live neighbours survives.");
            Console.WriteLine(" # Any dead cell with three live neighbours becomes a live cell.");
            Console.WriteLine(" # All other live cells die in the next generation.Similarly, all other dead cells stay dead.");
            Console.WriteLine("\n The initial pattern constitutes the seed of the system." +
                "\n The first generation is created by applying the above rules simultaneously to every cell in the seed, live or dead; " +
                "\n births and deaths occur simultaneously, and the discrete moment at which this happens is sometimes called a tick. " +
                "\n Each generation is a pure function of the preceding one.The rules continue to be applied repeatedly to create further generations.");
            Console.WriteLine("\n # Press any key to go back");
            Console.ReadKey();
        }

        /// <summary>
        /// Method for rendering the UI when all the cells on the field are dead.
        /// </summary>
        /// <param name="generation">Parameter that represents the generation number.</param>
        public void GameOverRender(int generation)
        {
            Console.Clear();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("---------------------------------");
            }
            Console.WriteLine("\n ### THE WHOLE FIELD IS DEAD! ###");
            Console.WriteLine($"\n Generations survived: {generation - 1}");
        }

        /// <summary>
        /// Method for rendering the UI for choosing which saved game file to load.
        /// </summary>
        /// <param name="numberOfFiles">The number of saved game files currently present in the folder.</param>
        public void ChooseFileToLoadMenuRender(int numberOfFiles, string filePath, bool wrongInput)
        {
            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine(" ### Choose which saved game to load###");
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
