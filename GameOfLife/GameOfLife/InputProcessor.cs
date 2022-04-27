using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    public class InputProcessor : IInputProcessor
    {
        private GameFieldModel _gameField;
        private IEngine _engine;
        private IFileIO _file;
        private IRender _render;
        private IFieldOperations _fieldOperations;
        private ILibrary _library;

        public void Injection(IEngine engine, IFileIO file, IRender render, IFieldOperations operations, ILibrary library)
        {
            _engine = engine;
            _file = file;
            _render = render;
            _fieldOperations = operations;
            _library = library;
        }
        /// <summary>
        /// Method to check user input in the main menu.
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        public GameFieldModel CheckInputMainMenu(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                    _engine.CorrectKeyPressed = true;
                    return _gameField = new(3, 3);

                case ConsoleKey.D2:
                    _engine.CorrectKeyPressed = true;
                    return _gameField = new(5, 5);

                case ConsoleKey.D3:
                    _engine.CorrectKeyPressed = true;
                    return _gameField = new(10, 10);

                case ConsoleKey.D4:
                    _engine.CorrectKeyPressed = true;
                    return _gameField = new(20, 20);

                case ConsoleKey.D5:
                    _engine.CorrectKeyPressed = true;
                    return _gameField = new(75, 40);

                case ConsoleKey.D6:
                    _engine.CorrectKeyPressed = true;
                    return EnterFieldDimensions(_engine.WrongInput);

                case ConsoleKey.L:
                    _gameField = _file.LoadGameFieldFromFile();
                    if (!_file.FileReadingError)
                    {
                        _gameField.Generation = _file.Generation;
                        _engine.Loaded = true;
                        _engine.ReadGeneration = true;
                        _engine.CorrectKeyPressed = true;
                    }
                    return _gameField;

                case ConsoleKey.G:
                    _engine.GliderGunMode = true;
                    return null;

                case ConsoleKey.F1:
                    _render.PrintRules();
                    return null;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    return null;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Method to check user input in the glider gun menu,
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        public GameFieldModel CheckInputGliderGunMenu(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                    _engine.GliderGunType = 1;
                    return _gameField = new(40, 30);
                    

                case ConsoleKey.D2:
                    _engine.GliderGunType = 2;
                    return _gameField = new(37, 40);

                case ConsoleKey.G:
                    Console.Clear();
                    _engine.GliderGunMode = false;
                    return null;

                default:
                    Console.WriteLine(WrongInputPhrase);
                    return null;
            }
        }

        /// <summary>
        /// Method to process user input field dimensions.
        /// </summary>
        /// <param name="wrongInput">Parameter that represent if there was wrong input.</param>
        public GameFieldModel EnterFieldDimensions(bool wrongInput)
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
                    Console.Write(EnterWidthPhrase);
                    if (int.TryParse(Console.ReadLine(), out int width) && width > 0)
                    {
                        return _gameField = new(length, width);
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
                _fieldOperations.Stop = true;
            }
            else if (int.TryParse(inputCoordinate, out var resultX) && resultX >= 0 && resultX < _gameField.Length)
            {
                _fieldOperations.CoordinateX = resultX;
                Console.Write(EnterYPhrase);
                inputCoordinate = Console.ReadLine();

                if (inputCoordinate == StopWord)
                {
                    _fieldOperations.Stop = true;
                }
                else if (int.TryParse(inputCoordinate, out var resultY) && resultY >= 0 && resultY < _gameField.Width)
                {
                    _fieldOperations.CoordinateY = resultY;
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
                    _fieldOperations.ManualSeeding(_gameField);
                    return true;

                case ConsoleKey.D2:
                    _fieldOperations.RandomSeeding(_gameField);
                    return true;

                case ConsoleKey.D3:
                    Console.Clear();
                    _fieldOperations.LibrarySeeding(_gameField, _engine.GliderGunMode, _engine.GliderGunType);
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
                    _fieldOperations.CallSpawningMethod(_gameField, _library.SpawnGlider);
                    return false;

                case ConsoleKey.D2:
                    _fieldOperations.CallSpawningMethod(_gameField, _library.SpawnLightWeight);
                    return false;

                case ConsoleKey.D3:
                    _fieldOperations.CallSpawningMethod(_gameField, _library.SpawnMiddleWeight);
                    return false;

                case ConsoleKey.D4:
                    _fieldOperations.CallSpawningMethod(_gameField, _library.SpawnHeavyWeight);
                    return false;

                default:
                    _engine.WrongInput = true;
                    return false;
            }
        }
    }
}
