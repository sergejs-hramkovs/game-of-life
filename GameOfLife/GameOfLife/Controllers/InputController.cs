using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    public class InputController : IInputController
    {
        private GameFieldModel _gameField;
        private IEngine _engine;
        private IFileIO _file;
        private IRender _render;
        private IFieldOperations _fieldOperations;
        private ILibrary _library;
        private bool _wrongInput = false;
        private bool _correctKeyPressed = false;
        public bool WrongInput
        {
            get => _wrongInput;
            set => _wrongInput = value;
        }
        public bool CorrectKeyPressed
        {
            get => _correctKeyPressed;
            set => _correctKeyPressed = value;
        }
        public GameFieldModel GameField
        {
            get => _gameField;
            set => _gameField = value;
        }

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
        /// <returns>Returns and instance of the GameFieldModel class.</returns>
        public GameFieldModel CheckInputMainMenu(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                    CorrectKeyPressed = true;
                    return _gameField = new(3, 3);

                case ConsoleKey.D2:
                    CorrectKeyPressed = true;
                    return _gameField = new(5, 5);

                case ConsoleKey.D3:
                    CorrectKeyPressed = true;
                    return _gameField = new(10, 10);

                case ConsoleKey.D4:
                    CorrectKeyPressed = true;
                    return _gameField = new(20, 20);

                case ConsoleKey.D5:
                    CorrectKeyPressed = true;
                    return _gameField = new(75, 40);

                case ConsoleKey.D6:
                    CorrectKeyPressed = true;
                    return EnterFieldDimensions(WrongInput);

                case ConsoleKey.L:
                    _file.InitiateLoadingFromFile();
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
        /// <returns>Returns and instance of the GameFieldModel class.</returns>
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
                    WrongInput = false;
                    return null;

                default:
                    WrongInput = true;
                    return null;
            }
        }

        /// <summary>
        /// Method to process user input field dimensions.
        /// </summary>
        /// <param name="wrongInput">Parameter that represent if there was wrong input.</param>
        /// <returns>Returns and instance of the GameFieldModel class.</returns>
        public GameFieldModel EnterFieldDimensions(bool wrongInput)
        {
            Console.CursorVisible = true;

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
                        Console.CursorVisible = false;
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

            Console.CursorVisible = true;
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
            Console.CursorVisible = false;
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
                    WrongInput = true;
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
                    WrongInput = true;
                    return false;
            }
        }

        /// <summary>
        /// Method to check for user input when choosing which saved game file to load.
        /// </summary>
        /// <param name="numberOfFiles">The number of saved game files currently in the folder.</param>
        /// <returns>Returns the number of saved game file to load.</returns>
        public int CheckInputSavedGameMenu(int numberOfFiles)
        {
            string userInput;
            int chosenFile = -1;
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out var fileNumber) && fileNumber > 0 && fileNumber <= numberOfFiles)
            {
                chosenFile = fileNumber;
                WrongInput = false;
            }
            else
            {
                WrongInput = true;
            }
            return chosenFile;
        }

        /// <summary>
        /// Method to change the time delay if LeftArrow or RightArrow keys are pressed.
        /// </summary>
        /// <param name="keyPressed">Parameters which stores Left and Right Arrow key presses.</param>
        public void ChangeDelay(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (_engine.Delay <= 100 && _engine.Delay > 10)
                    {
                        _engine.Delay -= 10;
                    }
                    else if (_engine.Delay > 100)
                    {
                        _engine.Delay -= 100;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (_engine.Delay < 2000)
                    {
                        if (_engine.Delay < 100)
                        {
                            _engine.Delay += 10;
                        }
                        else
                        {
                            _engine.Delay += 100;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Method to check user input in the pause menu.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores the key pressed in the pause menu.</param>
        public void CheckInputPauseMenu(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.S:
                    _file.SaveGameFieldToFile(_gameField);
                    Console.Clear();
                    _render.RuntimeUIRender(GameField, _engine.Delay);
                    _render.RenderField(GameField);
                    Console.WriteLine(SuccessfullySavedPhrase);
                    Console.ReadKey();
                    Console.Clear();
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;

                case ConsoleKey.R:
                    _engine.RestartGame();
                    break;

                default:
                    Console.Clear();
                    break;
            }
        }

        /// <summary>
        /// Method to pause the game by pressing the Spacebar.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores Spacebar key press.</param>
        public void PauseGame(ConsoleKeyInfo keyPressed)
        {
            ConsoleKeyInfo pauseMenuKeyPress;

            if (keyPressed.Key == ConsoleKey.Spacebar)
            {
                _render.PauseMenuRender();
                pauseMenuKeyPress = Console.ReadKey(true);
                CheckInputPauseMenu(pauseMenuKeyPress);
            }
        }

        /// <summary>
        /// Method to check user input in the exit menu.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores the key pressed in the exit menu.</param>
        public void CheckInputExitMenu(ConsoleKeyInfo keyPressed)
        {
            if (keyPressed.Key == ConsoleKey.R)
            {
                _engine.RestartGame();
            }
            else if (keyPressed.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }
    }
}
