using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    /// <summary>
    /// InputController class takes input from the user and deals with it accordingly.
    /// </summary>
    public class InputController : IInputController
    {
        private IEngine _engine;
        private IFileIO _file;
        private IRender _render;
        private IFieldOperations _fieldOperations;
        private ILibrary _library;
        public bool WrongInput { get; set; }
        public bool CorrectKeyPressed { get; set; }
        public GameFieldModel GameField { get; set; }
        public MultipleGamesModel MultipleGames { get; set; }

        /// <summary>
        /// Method to inject objects in the InputController class.
        /// </summary>
        /// <param name="engine">Engine class parameter.</param>
        /// <param name="file">File class parameter.</param>
        /// <param name="render">Render class parameter.</param>
        /// <param name="operations">FieldOperations class parameter.</param>
        /// <param name="library">Library class parameter.</param>
        public void Injection(IEngine engine, IFileIO? file = null, IRender? render = null, IFieldOperations? operations = null, ILibrary? library = null)
        {
            _engine = engine;
            _file = file;
            _render = render;
            _fieldOperations = operations;
            _library = library;
        }

        /// <summary>
        /// Method to take and process user's input in the Main Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        /// <returns>Returns an instance of the GameFieldModel class.</returns>
        public GameFieldModel CheckInputMainMenu(ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.D1:
                    CorrectKeyPressed = true;
                    return GameField = new(3, 3);

                case ConsoleKey.D2:
                    CorrectKeyPressed = true;
                    return GameField = new(5, 5);

                case ConsoleKey.D3:
                    CorrectKeyPressed = true;
                    return GameField = new(10, 10);

                case ConsoleKey.D4:
                    CorrectKeyPressed = true;
                    return GameField = new(20, 20);

                case ConsoleKey.D5:
                    CorrectKeyPressed = true;
                    return GameField = new(75, 40);

                case ConsoleKey.D6:
                    CorrectKeyPressed = true;
                    return EnterFieldDimensions(WrongInput);

                case ConsoleKey.L:
                    _file.InitiateLoadingFromFile();
                    return GameField;

                case ConsoleKey.G:
                    _engine.GliderGunMode = true;
                    return null;

                case ConsoleKey.M:
                    _engine.MultipleGamesMode = true;
                    CorrectKeyPressed = true;
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
        /// Method to take and process user's input in the Glider Gun Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        /// <returns>Returns an instance of the GameFieldModel class.</returns>
        public GameFieldModel CheckInputGliderGunMenu(ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.D1:
                    _engine.GliderGunType = 1;
                    return GameField = new(40, 30);


                case ConsoleKey.D2:
                    _engine.GliderGunType = 2;
                    return GameField = new(37, 40);

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
        /// Method to take and process the Game Field dimensions entered by the user.
        /// </summary>
        /// <param name="wrongInput">Parameter that represents if there was wrong input.</param>
        /// <returns>Returns an instance of the GameFieldModel class.</returns>
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
                        return GameField = new(length, width);
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
        /// Method to take and process the coordinates of cells or library objects entered by the user.
        /// </summary>
        /// <returns>Returns "stop = true" if the process of entering coordinates was stopped. Returns false if there was wrong input.</returns>
        public bool EnterCoordinates()
        {
            string inputCoordinate;
            Console.CursorVisible = true;
            Console.WriteLine(StopSeedingPhrase);
            Console.Write(EnterXPhrase);
            inputCoordinate = Console.ReadLine();
            if (inputCoordinate == StopWord)
            {
                _fieldOperations.StopDataInput = true;
            }
            else if (int.TryParse(inputCoordinate, out var resultX) && resultX >= 0 && resultX < GameField.Length)
            {
                _fieldOperations.CoordinateX = resultX;
                Console.Write(EnterYPhrase);
                inputCoordinate = Console.ReadLine();
                if (inputCoordinate == StopWord)
                {
                    _fieldOperations.StopDataInput = true;
                }
                else if (int.TryParse(inputCoordinate, out var resultY) && resultY >= 0 && resultY < GameField.Width)
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
        /// Method to take and process user's input in the Field Seeding Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        /// <returns>Returns 'true' if the correct key is pressed, otherwise 'false'</returns>
        public bool CheckInputPopulateFieldMenu(ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.D1:
                    _fieldOperations.ManualSeeding(GameField);
                    return true;

                case ConsoleKey.D2:
                    _fieldOperations.RandomSeeding(GameField);
                    return true;

                case ConsoleKey.D3:
                    Console.Clear();
                    _fieldOperations.LibrarySeeding(GameField, _engine.GliderGunMode, _engine.GliderGunType);
                    return true;

                default:
                    WrongInput = true;
                    return false;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Library Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter that stores the key pressed by the user.</param>
        /// <returns>Returns 'true' if the 'Escape' key is pressed, otherwise 'false'</returns>
        public bool CheckInputLibraryMenu(ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.Escape:
                    return true;

                case ConsoleKey.D1:
                    _fieldOperations.CallSpawningMethod(GameField, _library.SpawnGlider);
                    return false;

                case ConsoleKey.D2:
                    _fieldOperations.CallSpawningMethod(GameField, _library.SpawnLightWeight);
                    return false;

                case ConsoleKey.D3:
                    _fieldOperations.CallSpawningMethod(GameField, _library.SpawnMiddleWeight);
                    return false;

                case ConsoleKey.D4:
                    _fieldOperations.CallSpawningMethod(GameField, _library.SpawnHeavyWeight);
                    return false;

                default:
                    WrongInput = true;
                    return false;
            }
        }

        /// <summary>
        /// Method to take and process the user's choice of the saved game file.
        /// </summary>
        /// <param name="numberOfFiles">The number of saved game files currently in the folder.</param>
        /// <returns>Returns the number of the saved game file to load.</returns>
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
        public void ChangeDelay(ConsoleKey keyPressed)
        {
            switch (keyPressed)
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
        /// Method to take and process user's input in the Pause Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores the key pressed in the Pause Menu.</param>
        public void CheckInputPauseMenu(ConsoleKey keyPressed, bool multipleGamesMode = false)
        {
            switch (keyPressed)
            {
                case ConsoleKey.S:
                    _file.SaveGameFieldToFile(GameField);
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

                case ConsoleKey.N:
                    if (multipleGamesMode)
                    {
                        EnterNumberOfGamesToBeDisplayed();
                        MultipleGames.GamesToBeDisplayed.Clear();
                        for (int gameNumbersEntered = 0; gameNumbersEntered < MultipleGames.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                        {
                            EnterGameNumber();
                        }
                    }

                    Console.Clear();
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
        public void PauseGame(ConsoleKey keyPressed, bool multipleGamesMode = false)
        {
            ConsoleKey pauseMenuKeyPress;
            if (keyPressed == ConsoleKey.Spacebar)
            {
                _render.PauseMenuRender(multipleGamesMode);
                pauseMenuKeyPress = Console.ReadKey(true).Key;
                CheckInputPauseMenu(pauseMenuKeyPress, multipleGamesMode);
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Exit Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores the key pressed in the Exit Menu.</param>
        public void CheckInputExitMenu(ConsoleKey keyPressed)
        {
            if (keyPressed == ConsoleKey.R)
            {
                _engine.RestartGame();
            }
            else if (keyPressed == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Method to take and process the numbers of the games entered by the user.
        /// </summary>
        public void EnterGameNumber()
        {
            string gameNumber;
            Console.CursorVisible = true;
            while (true)
            {
                Console.WriteLine(DashesConstant);
                Console.Write(EnterGameNumberPhrase);
                gameNumber = Console.ReadLine();
                if (int.TryParse(gameNumber, out var number) && number >= 0 && number < MultipleGames.TotalNumberOfGames)
                {
                    if (!MultipleGames.GamesToBeDisplayed.Contains(number))
                    {
                        MultipleGames.GamesToBeDisplayed.Add(number);
                        Console.CursorVisible = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(GameAlreadyChosenPhrase);
                    }
                }
                else
                {
                    Console.WriteLine(WrongInputPhrase);
                }
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Multiple Games Mode Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores user input.</param>
        public bool CheckInputMultipleGamesMenu(ConsoleKey keyPressed)
        {
            Random random = new();
            switch (keyPressed)
            {
                case ConsoleKey.D1:
                    for (int gameNumbersEntered = 0; gameNumbersEntered < MultipleGames.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                    {
                        EnterGameNumber();
                    }

                    return true;

                case ConsoleKey.D2:
                    for (int gameNumbersEntered = 0; gameNumbersEntered < MultipleGames.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                    {
                        MultipleGames.GamesToBeDisplayed.Add(random.Next(0, MultipleGames.ListOfGames.Count));
                    }

                    return true;

                default:
                    Console.WriteLine(WrongInputPhrase);
                    return false;
            }
        }

        /// <summary>
        /// Method to take and process user's input of the number of games and Game Field sizes for the Multiple Games Mode.
        /// </summary>
        public MultipleGamesModel EnterMultipleGamesData(MultipleGamesModel multipleGames)
        {
            string userInput;
            MultipleGames = multipleGames;
            Console.CursorVisible = true;
            Console.Clear();
            while (true)
            {
                Console.Write(EnterTotalGamesNumberPhrase);
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out var totalNumberOfGames) && totalNumberOfGames > 1 && totalNumberOfGames <= 2000)
                {
                    MultipleGames.TotalNumberOfGames = totalNumberOfGames;
                    break;
                }
                else
                {
                    Console.WriteLine(WrongInputPhrase);
                }
            }

            while (true)
            {
                Console.Write(EnterLengthMultipleGamesPhrase);
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out var length) && length >= 3 && length <= 30)
                {
                    MultipleGames.Length = length;
                    break;
                }
                else
                {
                    Console.WriteLine(WrongInputPhrase);
                }
            }

            while (true)
            {
                Console.Write(EnterWidthMultipleGamesPhrase);
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out var width) && width >= 3 && width <= 10)
                {
                    MultipleGames.Width = width;
                    break;
                }
                else
                {
                    Console.WriteLine(WrongInputPhrase);
                }
            }

            EnterNumberOfGamesToBeDisplayed();
            Console.CursorVisible = false;
            return MultipleGames;
        }

        /// <summary>
        /// Method to take and process the number of the games that the user wants to be displayed.
        /// </summary>
        private void EnterNumberOfGamesToBeDisplayed()
        {
            string userInput;
            Console.CursorVisible = true;
            while (true)
            {
                Console.Write(EnterNumberOfGamesDisplayedPhrase);
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out var gamesToBeDisplayed) && gamesToBeDisplayed >= 2 && gamesToBeDisplayed <= 4)
                {
                    MultipleGames.NumberOfGamesToBeDisplayed = gamesToBeDisplayed;
                    break;
                }
                else
                {
                    Console.WriteLine(WrongInputPhrase);
                }
            }

            Console.CursorVisible = false;
        }
    }
}
