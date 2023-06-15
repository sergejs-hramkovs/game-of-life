using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Views;

namespace GameOfLife
{
    /// <summary>
    /// InputController class takes and processes user's input.
    /// </summary>
    [Serializable]
    public class InputProcessorService : IInputProcessorService
    {
        private readonly IMainEngine _mainEngine;
        private readonly IFileIO _file;
        private readonly IRenderer _renderer;
        private readonly IFieldOperations _fieldOperations;
        private readonly ILibrary _library;
        private readonly IUserInterfaceFiller _userInterfaceFiller;
        private readonly IMenuNavigator _menuNavigator;

        public bool WrongInput { get; set; }
        public bool CorrectKeyPressed { get; set; }
        public GameFieldModel GameField { get; set; }

        public InputProcessorService(IMainEngine mainEngine, IFileIO file, IRenderer renderer, IFieldOperations fieldOperations, ILibrary library, IUserInterfaceFiller userInterfaceFiller, IMenuNavigator menuNavigator)
        {
            _mainEngine = mainEngine;
            _file = file;
            _renderer = renderer;
            _fieldOperations = fieldOperations;
            _library = library;
            _userInterfaceFiller = userInterfaceFiller;
            _menuNavigator = menuNavigator;
        }

        /// <summary>
        /// Method to take and process user's input in the Main Menu.
        /// </summary>
        public void HandleInputMainMenu()
        {
            WrongInput = false;
            _file.NoSavedGames = false;
            switch (Console.ReadKey(true).Key)
            {
                // Single game.
                case ConsoleKey.D1:
                    _menuNavigator.NavigateMenu(MenuViews.SingleGameMenu);
                    _mainEngine.MultipleGames.InitializeSingleGameParameters();
                    if (!_mainEngine.MultipleGamesMode && !_mainEngine.SavedGameLoaded && !_mainEngine.GliderGunMode)
                    {
                        _menuNavigator.NavigateMenu(MenuViews.SeedingTypeMenu, clearMenuFromScreen: false, _renderer.RenderGridOfFields);
                    }
                    break;

                // Multiple games.
                case ConsoleKey.D2:
                    _mainEngine.MultipleGamesMode = true;
                    _menuNavigator.NavigateMultipleGamesMenu();
                    break;

                // Load game(s)
                case ConsoleKey.D3:
                    _menuNavigator.NavigateMenu(MenuViews.LoadGameMenu);
                    break;

                // Glider Gun Mode
                case ConsoleKey.D4:
                    _mainEngine.GliderGunMode = true;
                    _menuNavigator.NavigateMenu(MenuViews.GliderGunModeMenu);
                    _mainEngine.MultipleGames.InitializeSingleGameParameters();
                    break;

                // Rules and description page.
                case ConsoleKey.F1:
                    _renderer.RenderMenu(MenuViews.RulesPage);
                    Console.ReadKey();
                    _mainEngine.StartGame(false);
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Single Game Menu.
        /// </summary>
        public void HandleInputSingleGameMenu()
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    GameField = new(3, 3);
                    _mainEngine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.D2:
                    GameField = new(5, 5);
                    _mainEngine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.D3:
                    GameField = new(10, 10);
                    _mainEngine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.D4:
                    GameField = new(20, 20);
                    _mainEngine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.D5:
                    GameField = new(75, 40);
                    _mainEngine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.D6:
                    EnterFieldDimensions(WrongInput);
                    _mainEngine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.Escape:
                    _mainEngine.StartGame(false);
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Field Seeding Menu.
        /// </summary>
        public void HandleInputSeedingTypeMenu()
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    _fieldOperations.PopulateFieldManually(_mainEngine.MultipleGames);
                    break;

                case ConsoleKey.D2:
                    _fieldOperations.PopulateFieldRandomly(GameField);
                    break;

                case ConsoleKey.D3:
                    _fieldOperations.PopulateFieldFromLibrary(_mainEngine.MultipleGames);
                    break;

                case ConsoleKey.Escape:
                    _mainEngine.StartGame(false);
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Glider Gun Menu.
        /// </summary>
        public void HandleInputGliderGunMenu()
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    _mainEngine.GliderGunType = 1;
                    GameField = new(40, 30);
                    _mainEngine.MultipleGames.ListOfGames.Add(GameField);
                    _library.SpawnGosperGliderGun(_mainEngine.MultipleGames.ListOfGames[0], 1, 1);
                    break;


                case ConsoleKey.D2:
                    _mainEngine.GliderGunType = 2;
                    GameField = new(37, 40);
                    _mainEngine.MultipleGames.ListOfGames.Add(GameField);
                    _library.SpawnSimkinGliderGun(_mainEngine.MultipleGames.ListOfGames[0], 0, 16);
                    break;

                case ConsoleKey.Escape:
                    _mainEngine.StartGame(false);
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process the Game Field dimensions entered by the user.
        /// </summary>
        /// <param name="wrongInput">Parameter that represents if there was wrong input.</param>
        public void EnterFieldDimensions(bool wrongInput)
        {
            Console.CursorVisible = true;
            Console.Clear();
            while (true)
            {
                if (wrongInput)
                {
                    Console.Clear();
                    _renderer.ChangeColorWrite(StringConstants.WrongInputPhrase, newLine: false);
                }

                Console.Write(StringConstants.EnterLengthPhrase);
                if (int.TryParse(Console.ReadLine(), out int length) && length > 0)
                {
                    Console.Write(StringConstants.EnterWidthPhrase);
                    if (int.TryParse(Console.ReadLine(), out int width) && width > 0)
                    {
                        Console.CursorVisible = false;
                        GameField = new(length, width);
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
        /// Method to take and process user's input in the Library Menu.
        /// </summary>
        /// <returns>Returns 'true' if the 'Escape' key is pressed, otherwise 'false'</returns>
        public bool HandleInputLibraryMenu()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    _fieldOperations.CallSpawningMethod(_mainEngine.MultipleGames, _library.SpawnGlider);
                    return false;

                case ConsoleKey.D2:
                    _fieldOperations.CallSpawningMethod(_mainEngine.MultipleGames, _library.SpawnLightWeight);
                    return false;

                case ConsoleKey.D3:
                    _fieldOperations.CallSpawningMethod(_mainEngine.MultipleGames, _library.SpawnMiddleWeight);
                    return false;

                case ConsoleKey.D4:
                    _fieldOperations.CallSpawningMethod(_mainEngine.MultipleGames, _library.SpawnHeavyWeight);
                    return false;

                case ConsoleKey.Escape:
                    return true;

                default:
                    WrongInput = true;
                    return false;
            }
        }

        /// <summary>
        /// Method to take and process the user's choice of the Saved Game file.
        /// </summary>
        /// <param name="numberOfFiles">The number of saved game files currently in the folder.</param>
        /// <returns>Returns the number of the Saved Game file to load.</returns>
        public int HandleInputSavedGameMenu(int numberOfFiles)
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
        /// Method to change the time delay between generations if LeftArrow or RightArrow keys are pressed.
        /// </summary>
        /// <param name="keyPressed">Parameters which stores Left and Right Arrow key presses.</param>
        public void ChangeDelay(ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.LeftArrow:
                    if (_mainEngine.Delay <= 100 && _mainEngine.Delay > 10)
                    {
                        _mainEngine.Delay -= 10;
                    }
                    else if (_mainEngine.Delay > 100)
                    {
                        _mainEngine.Delay -= 100;
                    }

                    break;

                case ConsoleKey.RightArrow:
                    if (_mainEngine.Delay < 2000)
                    {
                        if (_mainEngine.Delay < 100)
                        {
                            _mainEngine.Delay += 10;
                        }
                        else
                        {
                            _mainEngine.Delay += 100;
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Pause Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores the key pressed in the Pause Menu.</param>
        /// <param name="multipleGamesMode">Parameter that represents if the Multiple Games Mode is enabled, 'false' by default.</param>
        public void HandleInputPauseMenu(ConsoleKey keyPressed, bool multipleGamesMode = false)
        {
            switch (keyPressed)
            {
                case ConsoleKey.S:
                    if (!_mainEngine.MultipleGamesMode)
                    {
                        _mainEngine.MultipleGames.ListOfGames[0].Generation = _mainEngine.MultipleGames.Generation;
                        _file.SaveGameFieldToFile(_mainEngine.MultipleGames.ListOfGames[0]);
                        _userInterfaceFiller.CreateSingleGameRuntimeUI(_mainEngine.MultipleGames, _mainEngine.Delay);
                        _renderer.RenderMenu(MenuViews.SingleGameUI, clearScreen: true);
                        _renderer.RenderGridOfFields(_mainEngine.MultipleGames);
                    }
                    else
                    {
                        _file.SaveMultipleGamesToFile(_mainEngine.MultipleGames);
                    }

                    Console.WriteLine(StringConstants.SuccessfullySavedPhrase);
                    Console.ReadKey();
                    Console.Clear();
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;

                case ConsoleKey.R:
                    _mainEngine.RestartGame();
                    break;

                case ConsoleKey.N:
                    if (multipleGamesMode)
                    {
                        _mainEngine.MultipleGames.GamesToBeDisplayed.Clear();
                        for (int gameNumbersEntered = 0; gameNumbersEntered < _mainEngine.MultipleGames.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                        {
                            EnterGameNumbersToBeDisplayed();
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
        /// <param name="multipleGamesMode">Parameter that represents if the Multiple Games Mode is enabled, 'false' by default.</param>
        public void PauseGame(ConsoleKey keyPressed, bool multipleGamesMode = false)
        {
            ConsoleKey pauseMenuKeyPress;
            if (keyPressed == ConsoleKey.Spacebar)
            {
                _renderer.RenderMenu(MenuViews.PauseMenu, multipleGames: multipleGamesMode, clearScreen: false);
                pauseMenuKeyPress = Console.ReadKey(true).Key;
                HandleInputPauseMenu(pauseMenuKeyPress, multipleGamesMode);
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Exit Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores the key pressed in the Exit Menu.</param>
        public void HandleInputExitMenu(ConsoleKey keyPressed)
        {
            if (keyPressed == ConsoleKey.R)
            {
                _mainEngine.RestartGame();
            }
            else if (keyPressed == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Method to take and process the numbers of the games entered by the user.
        /// </summary>
        public void EnterGameNumbersToBeDisplayed()
        {
            string gameNumber;
            Console.CursorVisible = true;
            while (true)
            {
                Console.WriteLine(StringConstants.DashesConstant);
                Console.Write(StringConstants.EnterGameNumberPhrase);
                gameNumber = Console.ReadLine();
                if (int.TryParse(gameNumber, out var number) && number >= 0 && (number < _mainEngine.MultipleGames.TotalNumberOfGames))
                {
                    if (!_mainEngine.MultipleGames.GamesToBeDisplayed.Contains(number))
                    {
                        _mainEngine.MultipleGames.GamesToBeDisplayed.Add(number);
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
        public void HandleInputMultipleGameNumbersMenu()
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    for (int gameNumbersEntered = 0; gameNumbersEntered < _mainEngine.MultipleGames.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                    {
                        EnterGameNumbersToBeDisplayed();
                    }

                    break;

                case ConsoleKey.D2:
                    for (int gameNumbersEntered = 0; gameNumbersEntered < _mainEngine.MultipleGames.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                    {
                        _mainEngine.MultipleGames.GamesToBeDisplayed.Add(gameNumbersEntered);
                    }

                    break;

                case ConsoleKey.Escape:
                    _mainEngine.StartGame(false);
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Multiple Games Mode field size choosing Menu.
        /// </summary>
        public void HandleInputMultipleGamesMenuFieldSize()
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    _mainEngine.MultipleGames.Length = 10;
                    _mainEngine.MultipleGames.Width = 10;
                    _mainEngine.MultipleGames.NumberOfGamesToBeDisplayed = 24;
                    break;

                case ConsoleKey.D2:
                    _mainEngine.MultipleGames.Length = 15;
                    _mainEngine.MultipleGames.Width = 15;
                    _mainEngine.MultipleGames.NumberOfGamesToBeDisplayed = 12;
                    break;

                case ConsoleKey.D3:
                    _mainEngine.MultipleGames.Length = 20;
                    _mainEngine.MultipleGames.Width = 20;
                    _mainEngine.MultipleGames.NumberOfGamesToBeDisplayed = 6;
                    break;

                case ConsoleKey.D4:
                    _mainEngine.MultipleGames.Length = 25;
                    _mainEngine.MultipleGames.Width = 25;
                    _mainEngine.MultipleGames.NumberOfGamesToBeDisplayed = 6;
                    break;

                case ConsoleKey.Escape:
                    _mainEngine.StartGame(false);
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input of the number of games and Game Field sizes for the Multiple Games Mode.
        /// </summary>
        public void EnterMultipleGamesQuantity()
        {

            Console.CursorVisible = true;
            while (true)
            {
                _renderer.RenderMenu(MenuViews.MultipleGamesModeGamesQuantityMenu, newLine: false, wrongInput: WrongInput);
                if (int.TryParse(Console.ReadLine(), out var totalNumberOfGames) && totalNumberOfGames >= 24 && totalNumberOfGames <= 1000)
                {
                    _mainEngine.MultipleGames.TotalNumberOfGames = totalNumberOfGames;
                    break;
                }
                else
                {
                    WrongInput = true;
                }
            }

            WrongInput = false;
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Method to take and process user's input in the Load Game Menu.
        /// </summary>
        public void HandleInputLoadGameMenu()
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    _file.InitiateLoadingFromFile();
                    _mainEngine.MultipleGames.InitializeSingleGameParameters();
                    break;

                case ConsoleKey.D2:
                    _file.InitiateLoadingFromFile(loadMultipleGames: true);
                    break;

                case ConsoleKey.Escape:
                    _mainEngine.StartGame(false);
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to deal with key presses for pause or delay changing during the runtime.
        /// </summary>
        /// <param name="multipleGamesMode">Parameter that represents if the Multiple Games Mode is enabled, 'false' by default.</param>
        /// <returns>Returns the pressed key.</returns>
        public ConsoleKey ReadKeyRuntime(bool multipleGamesMode = false)
        {
            ConsoleKey keyPressed = Console.ReadKey(true).Key;
            PauseGame(keyPressed, multipleGamesMode);
            ChangeDelay(keyPressed);
            return keyPressed;
        }
    }
}
