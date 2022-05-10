using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Views;

namespace GameOfLife
{
    /// <summary>
    /// InputController class takes input from the user and deals with it accordingly.
    /// </summary>
    [Serializable]
    public class InputController : IInputController
    {
        private IEngine _engine;
        private IFileIO _file;
        private IRenderer _render;
        private IFieldOperations _fieldOperations;
        private ILibrary _library;
        private IUserInterfaceFiller _userInterfaceViews;
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
        public void Injection(IEngine engine, IUserInterfaceFiller? userInterfaceViews = null, IFileIO? file = null,
            IRenderer? render = null, IFieldOperations? operations = null, ILibrary? library = null)
        {
            _engine = engine;
            _file = file;
            _render = render;
            _fieldOperations = operations;
            _library = library;
            _userInterfaceViews = userInterfaceViews;
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
                    _render.MenuRenderer(MenuViews.LoadGameMenu, clearScreen: true);
                    ConsoleKey loadingTypeChoice = Console.ReadKey(true).Key;
                    CheckInputLoadGameMenu(loadingTypeChoice);
                    return GameField;

                case ConsoleKey.G:
                    _engine.GliderGunMode = true;
                    return null;

                case ConsoleKey.M:
                    _engine.MultipleGamesMode = true;
                    CorrectKeyPressed = true;
                    return null;

                case ConsoleKey.F1:
                    _render.MenuRenderer(MenuViews.RulesPage);
                    Console.ReadKey();
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
                    Console.WriteLine(StringConstants.WrongInputPhrase);
                }

                Console.Write(StringConstants.EnterLengthPhrase);
                if (int.TryParse(Console.ReadLine(), out int length) && length > 0)
                {
                    Console.Write(StringConstants.EnterWidthPhrase);
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
            Console.WriteLine(StringConstants.StopSeedingPhrase);
            Console.Write(StringConstants.EnterXPhrase);
            inputCoordinate = Console.ReadLine();
            if (inputCoordinate == StringConstants.StopWord)
            {
                _fieldOperations.StopDataInput = true;
            }
            else if (int.TryParse(inputCoordinate, out var resultX) && resultX >= 0 && resultX < GameField.Length)
            {
                _fieldOperations.CoordinateX = resultX;
                Console.Write(StringConstants.EnterYPhrase);
                inputCoordinate = Console.ReadLine();
                if (inputCoordinate == StringConstants.StopWord)
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
                    if (!_engine.MultipleGamesMode)
                    {
                        _file.SaveGameFieldToFile(GameField);
                        _userInterfaceViews.SingleGameRuntimeUICreation(GameField, _engine.Delay);
                        _render.MenuRenderer(MenuViews.SingleGameUI, clearScreen: true);
                        _render.RenderField(GameField);
                    }
                    else
                    {
                        _file.SaveMultipleGamesToFile(_engine.MultipleGames);
                    }

                    Console.WriteLine(StringConstants.SuccessfullySavedPhrase);
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
                        _engine.MultipleGames.GamesToBeDisplayed.Clear();
                        for (int gameNumbersEntered = 0; gameNumbersEntered < _engine.MultipleGames.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                        {
                            EnterGameNumbers();
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
                _render.MenuRenderer(MenuViews.PauseMenu, multipleGames: multipleGamesMode, clearScreen: false);
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
        public void EnterGameNumbers()
        {
            string gameNumber;
            Console.CursorVisible = true;
            while (true)
            {
                Console.WriteLine(StringConstants.DashesConstant);
                Console.Write(StringConstants.EnterGameNumberPhrase);
                gameNumber = Console.ReadLine();
                if (int.TryParse(gameNumber, out var number) && number >= 0 && number < _engine.MultipleGames.TotalNumberOfGames)
                {
                    if (!_engine.MultipleGames.GamesToBeDisplayed.Contains(number))
                    {
                        _engine.MultipleGames.GamesToBeDisplayed.Add(number);
                        Console.CursorVisible = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(StringConstants.GameAlreadyChosenPhrase);
                    }
                }
                else
                {
                    Console.WriteLine(StringConstants.WrongInputPhrase);
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
                        EnterGameNumbers();
                    }

                    return true;

                case ConsoleKey.D2:
                    for (int gameNumbersEntered = 0; gameNumbersEntered < MultipleGames.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                    {
                        MultipleGames.GamesToBeDisplayed.Add(random.Next(0, MultipleGames.ListOfGames.Count));
                    }

                    return true;

                default:
                    Console.WriteLine(StringConstants.WrongInputPhrase);
                    return false;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Multiple Games Mode field size choosing Menu.
        /// </summary>
        /// <param name="multipleGames">An object with a list of Game Fields.</param>
        /// <param name="keyPressed">Parameter which stores user input.</param>
        public MultipleGamesModel CheckInputMultipleGamesMenuFieldSize(MultipleGamesModel multipleGames, ConsoleKey keyPressed)
        {
            MultipleGames = multipleGames;
            switch (keyPressed)
            {
                case ConsoleKey.D1:
                    MultipleGames.Length = 10;
                    MultipleGames.Width = 10;
                    MultipleGames.NumberOfGamesToBeDisplayed = 24;
                    return MultipleGames;

                case ConsoleKey.D2:
                    MultipleGames.Length = 15;
                    MultipleGames.Width = 15;
                    MultipleGames.NumberOfGamesToBeDisplayed = 12;
                    return MultipleGames;

                case ConsoleKey.D3:
                    MultipleGames.Length = 20;
                    MultipleGames.Width = 20;
                    MultipleGames.NumberOfGamesToBeDisplayed = 6;
                    return MultipleGames;

                case ConsoleKey.D4:
                    MultipleGames.Length = 25;
                    MultipleGames.Width = 25;
                    MultipleGames.NumberOfGamesToBeDisplayed = 6;
                    return MultipleGames;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Method to take and process user's input of the number of games and Game Field sizes for the Multiple Games Mode.
        /// </summary>
        public MultipleGamesModel EnterNumberOfMultipleGames(MultipleGamesModel multipleGames)
        {
            string userInput;
            MultipleGames = multipleGames;
            Console.CursorVisible = true;
            while (true)
            {
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out var totalNumberOfGames) && totalNumberOfGames >= 100 && totalNumberOfGames <= 10000)
                {
                    MultipleGames.TotalNumberOfGames = totalNumberOfGames;
                    break;
                }
                else
                {
                    Console.WriteLine(StringConstants.WrongInputPhrase);
                }
            }

            Console.CursorVisible = false;
            return MultipleGames;
        }

        /// <summary>
        /// Method to take and process user's input in the Load Game Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores user's input.</param>
        private void CheckInputLoadGameMenu(ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.D1:
                    _file.InitiateLoadingFromFile();
                    break;

                case ConsoleKey.D2:
                    _engine.MultipleGamesMode = true;
                    _engine.MultipleGamesLoaded = true;
                    _file.InitiateLoadingFromFile(loadMultipleGames: true);
                    break;

                case ConsoleKey.Escape:
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to deal with key presses for pause or delay changing during the runtime.
        /// </summary>
        /// <returns>Returns the pressed key.</returns>
        public ConsoleKey RuntimeKeyReader(bool multipleGamesMode = false)
        {
            ConsoleKey keyPressed = Console.ReadKey(true).Key;
            PauseGame(keyPressed, multipleGamesMode);
            ChangeDelay(keyPressed);
            return keyPressed;
        }
    }
}
