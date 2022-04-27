using GameOfLife.Interfaces;
using static GameOfLife.StringConstants;

namespace GameOfLife
{
    public class InputProcessor : IInputProcessor
    {
        private IEngine _engine;
        private IFileIO _file;
        private IRender _render;

        public InputProcessor(IEngine engine, IFileIO file, IRender render)
        {
            _engine = engine;
            _file = file;
            _render = render;
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
        /// <param name="wrongInput">Parameter that represent if there had been wrong input.</param>
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
    }
}
