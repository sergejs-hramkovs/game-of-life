using GameOfLife.Interfaces;
using static GameOfLife.StringConstants;

namespace GameOfLife
{
    public class Render : IRender
    {
        private string[,] _gameField;
        private int _generation = 1;
        private int _aliveCells;
        private int _deadCells;
        private IField _field;
        private IRulesApplier _rulesApplier;
        private IEngine _engine;
        private Tuple<string[,], int, int, int> _returnValues;

        public Render(IEngine engine, IRulesApplier rulesApplier)
        {
            _engine = engine;
            _rulesApplier = rulesApplier;
        }

        /// <summary>
        /// Method that draws the field.
        /// </summary>
        /// <param name="field">An array of a gamefield.</param>
        public void RenderField(string[,] field)
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
        public void InitialRender(IField field, string[,] inputField, bool loaded, bool gliderGunMode)
        {
            _gameField = inputField;
            _field = field;

            if (!loaded)
            {
                _gameField = _field.CreateField();
                Console.Clear();
                RenderField(_gameField);
                _gameField = _field.SeedField(gliderGunMode);
            }
            Console.Clear();
        }

        /// <summary>
        /// Method for rendering the gamefield between the generations.
        /// </summary>
        /// <param name="delay">Delay between generations in miliseconds</param>
        /// <param name="gliderGunMode">Parameter to enable the Glider Gun mode with dead borders rules.</param>
        /// <param name="resetGeneration">Parameter to rest the number of generation after restart.</param>
        /// <returns>Returns a tuple containing an array of the game field, number of alive and dead cells and the generation number.</returns>
        public Tuple<string[,], int, int, int> RuntimeRender(int delay, bool gliderGunMode, bool resetGeneration, bool readGeneration, int generationFromFile)
        {
            _aliveCells = _engine.CountAliveCells(_gameField);
            _deadCells = _gameField.GetLength(0) * _gameField.GetLength(1) - _aliveCells;
            if (resetGeneration)
            {
                _generation = 1;
            }
            if (readGeneration)
            {
                _generation = generationFromFile;
                readGeneration = false;
            }
            Console.WriteLine("# Press ESC to stop");
            Console.WriteLine("# Press Spacebar to pause");
            Console.WriteLine("# Change the delay using left and right arrows");
            Console.WriteLine($"\nGeneration: {_generation}");
            Console.WriteLine($"Alive cells: {_aliveCells}({(int)Math.Round(_aliveCells / (double)(_deadCells + _aliveCells) * 100.0)}%)   ");
            Console.WriteLine($"Dead cells: {_deadCells}   ");
            Console.WriteLine($"Current delay between generations: {delay / 1000.0} seconds  ");
            Console.WriteLine($"Number of generations per second: {Math.Round(1 / (delay / 1000.0), 2)}   ");
            if (gliderGunMode)
            {
                _rulesApplier.CheckCellsDeadBorder(_gameField);
            }
            else
            {
                _rulesApplier.CheckCells(_gameField);
            }
            RenderField(_gameField);
            _gameField = _rulesApplier.FieldRefresh(_gameField); 
            _returnValues = new Tuple<string[,], int, int, int>(_gameField, _aliveCells, _deadCells, _generation);
            _generation++;
            return _returnValues;
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
        public void FieldSizeMenuRender(bool wrongInput)
        {
            Console.Clear();
            if (wrongInput)
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
            Console.WriteLine("# Press 'F1' to read the rules and the description of the game");
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
    }
}
