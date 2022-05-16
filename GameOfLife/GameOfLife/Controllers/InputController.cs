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
        private IMainEngine _engine;
        private IFileIO _file;
        private IRenderer _renderer;
        private IFieldOperations _fieldOperations;
        private ILibrary _library;
        private IUserInterfaceFiller _userInterfaceViews;
        private IMenuNavigator _menuNavigator;
        public bool WrongInput { get; set; }
        public bool CorrectKeyPressed { get; set; }
        public GameFieldModel GameField { get; set; }

        /// <summary>
        /// Method to inject objects in the InputController class.
        /// </summary>
        /// <param name="engine">Engine class parameter.</param>
        /// <param name="file">File class parameter.</param>
        /// <param name="renderer">Render class parameter.</param>
        /// <param name="operations">FieldOperations class parameter.</param>
        /// <param name="library">Library class parameter.</param>
        public void Injection(IMainEngine engine, IUserInterfaceFiller? userInterfaceViews = null, IFileIO? file = null,
            IRenderer? renderer = null, IFieldOperations? operations = null, ILibrary? library = null, IMenuNavigator? menuNavigator = null)
        {
            _engine = engine;
            _file = file;
            _renderer = renderer;
            _fieldOperations = operations;
            _library = library;
            _userInterfaceViews = userInterfaceViews;
            _menuNavigator = menuNavigator;
        }

        /// <summary>
        /// Method to take and process user's input in the main menu.
        /// </summary>
        public void HandleInputMainMenu()
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                // Single game.
                case ConsoleKey.D1:
                    _menuNavigator.NavigateMenu(MenuViews.SingleGameMenu, HandleInputSingleGameMenu, clearScr: true);
                    _engine.MultipleGames.InitializeSingleGameParameters();
                    if (!_engine.MultipleGamesMode && !_engine.SavedGameLoaded && !_engine.GliderGunMode)
                    {
                        _menuNavigator.NavigateMenu(MenuViews.SeedingTypeMenu, HandleInputSeedingTypeMenu, clearScr: false, _renderer.GridOfFieldsRenderer);
                    }
                    break;

                // Multiple games.
                case ConsoleKey.D2:
                    _engine.MultipleGamesMode = true;
                    _menuNavigator.NavigateMultipleGamesMenu();
                    break;

                // Load game(s)
                case ConsoleKey.D3:
                    _engine.SavedGameLoaded = true;
                    _menuNavigator.NavigateMenu(MenuViews.LoadGameMenu, LoadGameMenuInputProcessor);
                    break;

                // Glider Gun Mode
                case ConsoleKey.D4:
                    _engine.GliderGunMode = true;
                    _menuNavigator.NavigateMenu(MenuViews.GliderGunModeMenu, HandleInputGliderGunMenu);
                    _engine.MultipleGames.InitializeSingleGameParameters();
                    break;

                // Rules and description page.
                case ConsoleKey.F1:
                    _renderer.MenuRenderer(MenuViews.RulesPage);
                    Console.ReadKey();
                    _engine.StartGame(false);
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to to take process user's input in the single game menu.
        /// </summary>
        public void HandleInputSingleGameMenu()
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    GameField = new(3, 3);
                    _engine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.D2:
                    GameField = new(5, 5);
                    _engine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.D3:
                    GameField = new(10, 10);
                    _engine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.D4:
                    GameField = new(20, 20);
                    _engine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.D5:
                    GameField = new(75, 40);
                    _engine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.D6:
                    EnterFieldDimensions(WrongInput);
                    _engine.MultipleGames.ListOfGames.Add(GameField);
                    break;

                case ConsoleKey.Escape:
                    _engine.StartGame(false);
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Field Seeding Menu.
        /// </summary>
        /// <returns>Returns 'true' if the correct key is pressed, otherwise 'false'</returns>
        public void HandleInputSeedingTypeMenu()
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    _fieldOperations.ManualSeeding(_engine.MultipleGames);
                    break;

                case ConsoleKey.D2:
                    _fieldOperations.RandomSeeding(GameField);
                    break;

                case ConsoleKey.D3:
                    _fieldOperations.LibrarySeeding(_engine.MultipleGames);
                    break;

                case ConsoleKey.Escape:
                    _engine.StartGame(false);
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
                    _engine.GliderGunType = 1;
                    GameField = new(40, 30);
                    _engine.MultipleGames.ListOfGames.Add(GameField);
                    _library.SpawnGosperGliderGun(_engine.MultipleGames.ListOfGames[0], 1, 1);
                    break;


                case ConsoleKey.D2:
                    _engine.GliderGunType = 2;
                    GameField = new(37, 40);
                    _engine.MultipleGames.ListOfGames.Add(GameField);
                    _library.SpawnSimkinGliderGun(_engine.MultipleGames.ListOfGames[0], 0, 16);
                    break;

                case ConsoleKey.Escape:
                    _engine.StartGame(false);
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
        /// <returns>Returns an instance of the GameFieldModel class.</returns>
        public void EnterFieldDimensions(bool wrongInput)
        {
            Console.CursorVisible = true;
            Console.Clear();
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
        public bool LibraryMenuInputProcessor()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Escape:
                    return true;

                case ConsoleKey.D1:
                    _fieldOperations.CallSpawningMethod(_engine.MultipleGames, _library.SpawnGlider);
                    return false;

                case ConsoleKey.D2:
                    _fieldOperations.CallSpawningMethod(_engine.MultipleGames, _library.SpawnLightWeight);
                    return false;

                case ConsoleKey.D3:
                    _fieldOperations.CallSpawningMethod(_engine.MultipleGames, _library.SpawnMiddleWeight);
                    return false;

                case ConsoleKey.D4:
                    _fieldOperations.CallSpawningMethod(_engine.MultipleGames, _library.SpawnHeavyWeight);
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
                        _userInterfaceViews.SingleGameRuntimeUICreator(_engine.MultipleGames, _engine.Delay);
                        _renderer.MenuRenderer(MenuViews.SingleGameUI, clearScreen: true);
                        _renderer.GridOfFieldsRenderer(_engine.MultipleGames);
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
        public void PauseGame(ConsoleKey keyPressed, bool multipleGamesMode = false)
        {
            ConsoleKey pauseMenuKeyPress;
            if (keyPressed == ConsoleKey.Spacebar)
            {
                _renderer.MenuRenderer(MenuViews.PauseMenu, multipleGames: multipleGamesMode, clearScreen: false);
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
        public void EnterGameNumbersToBeDisplayed()
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
        public void ChooseMultipleGameNumbersMenuInputProcessor()
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    for (int gameNumbersEntered = 0; gameNumbersEntered < _engine.MultipleGames.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                    {
                        EnterGameNumbersToBeDisplayed();
                    }

                    break;

                case ConsoleKey.D2:
                    for (int gameNumbersEntered = 0; gameNumbersEntered < _engine.MultipleGames.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                    {
                        _engine.MultipleGames.GamesToBeDisplayed.Add(gameNumbersEntered);
                    }

                    break;

                case ConsoleKey.Escape:
                    _engine.StartGame(false);
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
                    _engine.MultipleGames.Length = 10;
                    _engine.MultipleGames.Width = 10;
                    _engine.MultipleGames.NumberOfGamesToBeDisplayed = 24;
                    break;

                case ConsoleKey.D2:
                    _engine.MultipleGames.Length = 15;
                    _engine.MultipleGames.Width = 15;
                    _engine.MultipleGames.NumberOfGamesToBeDisplayed = 12;
                    break;

                case ConsoleKey.D3:
                    _engine.MultipleGames.Length = 20;
                    _engine.MultipleGames.Width = 20;
                    _engine.MultipleGames.NumberOfGamesToBeDisplayed = 6;
                    break;

                case ConsoleKey.D4:
                    _engine.MultipleGames.Length = 25;
                    _engine.MultipleGames.Width = 25;
                    _engine.MultipleGames.NumberOfGamesToBeDisplayed = 6;
                    break;

                case ConsoleKey.Escape:
                    _engine.StartGame(false);
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
                if (int.TryParse(Console.ReadLine(), out var totalNumberOfGames) && totalNumberOfGames >= 24 && totalNumberOfGames <= 10000)
                {
                    _engine.MultipleGames.TotalNumberOfGames = totalNumberOfGames;
                    break;
                }
                else
                {
                    Console.WriteLine(StringConstants.WrongInputPhrase);
                }
            }

            Console.CursorVisible = false;
        }

        /// <summary>
        /// Method to take and process user's input in the Load Game Menu.
        /// </summary>
        public void LoadGameMenuInputProcessor()
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    _file.InitiateLoadingFromFile();
                    _engine.MultipleGames.InitializeSingleGameParameters();
                    break;

                case ConsoleKey.D2:
                    _engine.MultipleGamesMode = true;
                    _engine.SavedGameLoaded = true;
                    _file.InitiateLoadingFromFile(loadMultipleGames: true);
                    break;

                case ConsoleKey.Escape:
                    _engine.StartGame(false);
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
