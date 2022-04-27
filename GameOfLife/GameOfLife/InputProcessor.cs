using GameOfLife.Interfaces;
using static GameOfLife.StringConstants;

namespace GameOfLife
{
    public class InputProcessor : IInputProcessor
    {
        private IEngine _engine;
        private IFileIO _file;
        private IRender _render;
        private IField _field;
        private ILibrary _library;

        public InputProcessor(IEngine engine, IFileIO file, IRender render, IField field, ILibrary library)
        {
            _engine = engine;
            _file = file;
            _render = render;
            _field = field;
            _library = library;
        }
        /// <summary>
        /// Method to check user input in the main menu.
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        public void CheckInputMainMenu(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                    _engine.Length = 3;
                    _engine.Width = 3;
                    _engine.CorrectKeyPressed = true;
                    break;

                case ConsoleKey.D2:
                    _engine.Length = 5;
                    _engine.Width = 5;
                    _engine.CorrectKeyPressed = true;
                    break;

                case ConsoleKey.D3:
                    _engine.Length = 10;
                    _engine.Width = 10;
                    _engine.CorrectKeyPressed = true;
                    break;

                case ConsoleKey.D4:
                    _engine.Length = 20;
                    _engine.Width = 20;
                    _engine.CorrectKeyPressed = true;
                    break;

                case ConsoleKey.D5:
                    _engine.Length = 75;
                    _engine.Width = 40;
                    _engine.CorrectKeyPressed = true;
                    break;

                case ConsoleKey.D6:
                    EnterFieldDimensions(_engine.WrongInput);
                    _engine.CorrectKeyPressed = true;
                    break;

                case ConsoleKey.L:
                    _engine.GameField = _file.LoadGameFieldFromFile();
                    if (!_file.FileReadingError)
                    {
                        _engine.Generation = _file.Generation;
                        _engine.Loaded = true;
                        _engine.ReadGeneration = true;
                        _engine.CorrectKeyPressed = true;
                    }
                    break;

                case ConsoleKey.G:
                    _engine.GliderGunMode = true;
                    break;

                case ConsoleKey.F1:
                    _render.PrintRules();
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;

                default:
                    _engine.Length = 10;
                    _engine.Width = 10;
                    break;
            }
        }

        /// <summary>
        /// Method to check user input in the glider gun menu,
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        public void CheckInputGliderGunMenu(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                    _engine.GliderGunType = 1;
                    _engine.Length = 40;
                    _engine.Width = 30;
                    break;

                case ConsoleKey.D2:
                    _engine.GliderGunType = 2;
                    _engine.Length = 37;
                    _engine.Width = 40;
                    break;

                case ConsoleKey.G:
                    Console.Clear();
                    _engine.GliderGunMode = false;
                    break;

                default:
                    Console.WriteLine(WrongInputPhrase);
                    break;
            }
        }

        /// <summary>
        /// Method to process user input field dimensions.
        /// </summary>
        /// <param name="wrongInput">Parameter that represent if there was wrong input.</param>
        public void EnterFieldDimensions(bool wrongInput)
        {
            while (true)
            {
                if (wrongInput)
                {
                    Console.Clear();
                    Console.WriteLine(WrongInputPhrase);
                }
                Console.Write(EnterLengthPhrase);
                if (int.TryParse(Console.ReadLine(), out int length) && length > 0)
                {
                    _engine.Length = length;
                    Console.Write(EnterWidthPhrase);
                    if (int.TryParse(Console.ReadLine(), out int width) && width > 0)
                    {
                        _engine.Width = width;
                        break;
                    }
                    else
                    {
                        wrongInput = true;
                    }
                }
                else
                {
                    wrongInput = true;
                }
            }
        }

        /// <summary>
        /// Method to process user input coordinates.
        /// </summary>
        /// <returns>Returns "stop = true" if the process of entering coordinates was stopped. Returns false if there is wrong input.</returns>
        public bool EnterCoordinates()
        {
            string inputCoordinate;

            Console.WriteLine(StopSeedingPhrase);
            Console.Write(EnterXPhrase);
            inputCoordinate = Console.ReadLine();

            if (inputCoordinate == StopWord)
            {
                _field.Stop = true;
            }
            else if (int.TryParse(inputCoordinate, out var resultX) && resultX >= 0 && resultX < _field.FieldArray.GetLength(0))
            {
                _field.CoordinateX = resultX;
                Console.Write(EnterYPhrase);
                inputCoordinate = Console.ReadLine();

                if (inputCoordinate == StopWord)
                {
                    _field.Stop = true;
                }
                else if (int.TryParse(inputCoordinate, out var resultY) && resultY >= 0 && resultY < _field.FieldArray.GetLength(1))
                {
                    _field.CoordinateY = resultY;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Method to check user input in the populate the field menu.
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        /// <returns>Returns 'true' if the correct key is pressed, otherwise 'false'</returns>
        public bool CheckInputPopulateFieldMenu(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                    _field.ManualSeeding();
                    return true;

                case ConsoleKey.D2:
                    _field.RandomSeeding(_field.FieldArray.GetLength(0), _field.FieldArray.GetLength(1));
                    return true;

                case ConsoleKey.D3:
                    Console.Clear();
                    _field.LibrarySeeding(_engine.GliderGunMode, _engine.GliderGunType);
                    return true;

                default:
                    _engine.WrongInput = true;
                    return false;
            }
        }

        /// <summary>
        /// Method to check user input in the library menu.
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        /// <returns>Returns 'true' if the 'Escape' key is pressed, otherwise 'false'</returns>
        public bool CheckInputLibraryMenu(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.Escape:
                    return true;

                case ConsoleKey.D1:
                    _field.CallSpawningMethod(_library.SpawnGlider);
                    return false;

                case ConsoleKey.D2:
                    _field.CallSpawningMethod(_library.SpawnLightWeight);
                    return false;

                case ConsoleKey.D3:
                    _field.CallSpawningMethod(_library.SpawnMiddleWeight);
                    return false;

                case ConsoleKey.D4:
                    _field.CallSpawningMethod(_library.SpawnHeavyWeight);
                    return false;

                default:
                    _engine.WrongInput = true;
                    return false;
            }
        }
    }
}
