using GameOfLife.Entities.Models;
using GameOfLife.Interfaces;
using GameOfLife.Views;

namespace GameOfLife
{
    /// <summary>
    /// InputController class takes and processes user's input.
    /// </summary>
    [Serializable]
    public class InputProcessorService : IInputProcessorService
    {
        private readonly IRenderingService _renderingService;
        private readonly IFieldSeedingService _fieldSeedingService;
        private readonly ILibrary _library;

        public InputProcessorService(
            IRenderingService renderer,
            IFieldSeedingService fieldOperations,
            ILibrary library)
        {
            _renderingService = renderer;
            _fieldSeedingService = fieldOperations;
            _library = library;
        }

        /// <summary>
        /// Method to take and process user's input in the Main Menu.
        /// </summary>
        public void HandleInputMainMenu(GameModel game)
        {
            var inputDetails = game.InputDetails;
            var fileDetails = game.FileDetails;

            inputDetails.WrongInput = false;
            fileDetails.NoSavedGames = false;

            switch (Console.ReadKey(true).Key)
            {
                // Single game.
                case ConsoleKey.D1:

                    // Move this somewhere later.
                    do
                    {
                        _renderingService.RenderMenu(MenuViews.SingleGameMenu, wrongInput: inputDetails.WrongInput, clearScreen: true, noSavedGames: false);
                        HandleInputSingleGameMenu(game);
                    } while (inputDetails.WrongInput);

                    // Move this somewhere later.
                    game.MultipleGamesField.Length = game.MultipleGamesField.ListOfGames[0].Length;
                    game.MultipleGamesField.Width = game.MultipleGamesField.ListOfGames[0].Width;
                    game.MultipleGamesField.TotalNumberOfGames = 1;
                    game.MultipleGamesField.NumberOfGamesToBeDisplayed = 1;
                    game.MultipleGamesField.GamesToBeDisplayed.Add(0);

                    if (!game.GameDetails.IsMultipleGamesMode && !game.GameDetails.IsSavedGameLoaded && !game.GameDetails.IsGliderGunMode)
                    {
                        // Move this somewhere later.
                        do
                        {
                            _renderingService.RenderMenu(MenuViews.SeedingTypeMenu, wrongInput: inputDetails.WrongInput, clearScreen: false, noSavedGames: false);
                            HandleInputSeedingTypeMenu(game);
                        } while (inputDetails.WrongInput);

                        _renderingService.RenderGridOfFields(game);
                    }
                    break;

                // Multiple games.
                case ConsoleKey.D2:
                    game.GameDetails.IsMultipleGamesMode = true;

                    EnterMultipleGamesQuantity(game);

                    // Move this somewhere later.
                    do
                    {
                        _renderingService.RenderMenu(MenuViews.MultipleGamesModeFieldSizeChoiceMenu, wrongInput: inputDetails.WrongInput, clearScreen: true, noSavedGames: false);
                        HandleInputMultipleGamesMenuFieldSize(game);
                    } while (inputDetails.WrongInput);

                    // Move this somewhere later.
                    for (int gameNumber = 0; gameNumber < game.MultipleGamesField.TotalNumberOfGames; gameNumber++)
                    {
                        game.MultipleGamesField.ListOfGames.Add(new(game.MultipleGamesField.Length, game.MultipleGamesField.Width));
                        _fieldSeedingService.PopulateFieldRandomly(game.MultipleGamesField.ListOfGames[gameNumber]);
                    }

                    game.MultipleGamesField.NumberOfFieldsAlive = game.MultipleGamesField.ListOfGames.Count;

                    // Move this somewhere later.
                    do
                    {
                        _renderingService.RenderMenu(MenuViews.MultipleGamesModeMenu, wrongInput: inputDetails.WrongInput, clearScreen: true, noSavedGames: false);
                        HandleInputMultipleGameNumbersMenu(game);
                    } while (inputDetails.WrongInput);

                    break;

                // Load game(s)
                case ConsoleKey.D3:

                    // Move this somewhere later.
                    do
                    {
                        _renderingService.RenderMenu(MenuViews.LoadGameMenu, wrongInput: inputDetails.WrongInput, clearScreen: true, noSavedGames: false);
                        HandleInputMainMenu(game);
                    } while (inputDetails.WrongInput);
                    break;

                // Glider Gun Mode
                case ConsoleKey.D4:
                    game.GameDetails.IsGliderGunMode = true;

                    // Move this somewhere later.
                    do
                    {
                        _renderingService.RenderMenu(MenuViews.GliderGunModeMenu, wrongInput: inputDetails.WrongInput, clearScreen: true, noSavedGames: false);
                        HandleInputMainMenu(game);
                    } while (inputDetails.WrongInput);

                    // Move this somewhere later.
                    game.MultipleGamesField.Length = game.MultipleGamesField.ListOfGames[0].Length;
                    game.MultipleGamesField.Width = game.MultipleGamesField.ListOfGames[0].Width;
                    game.MultipleGamesField.TotalNumberOfGames = 1;
                    game.MultipleGamesField.NumberOfGamesToBeDisplayed = 1;
                    game.MultipleGamesField.GamesToBeDisplayed.Add(0);

                    break;

                // Rules and description page.
                case ConsoleKey.F1:
                    _renderingService.RenderMenu(MenuViews.RulesPage);
                    Console.ReadKey();
                    //_mainEngine.StartGame(false);
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;

                default:
                    inputDetails.WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Single Game Menu.
        /// </summary>
        public void HandleInputSingleGameMenu(GameModel game)
        {
            var inputDetails = game.InputDetails;

            inputDetails.WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    game.SingleGame = new(3, 3);
                    game.MultipleGamesField.ListOfGames.Add(game.SingleGame);
                    break;

                case ConsoleKey.D2:
                    game.SingleGame = new(5, 5);
                    game.MultipleGamesField.ListOfGames.Add(game.SingleGame);
                    break;

                case ConsoleKey.D3:
                    game.SingleGame = new(10, 10);
                    game.MultipleGamesField.ListOfGames.Add(game.SingleGame);
                    break;

                case ConsoleKey.D4:
                    game.SingleGame = new(20, 20);
                    game.MultipleGamesField.ListOfGames.Add(game.SingleGame);
                    break;

                case ConsoleKey.D5:
                    game.SingleGame = new(75, 40);
                    game.MultipleGamesField.ListOfGames.Add(game.SingleGame);
                    break;

                case ConsoleKey.D6:
                    EnterFieldDimensions(game, inputDetails.WrongInput);
                    game.MultipleGamesField.ListOfGames.Add(game.SingleGame);
                    break;

                case ConsoleKey.Escape:
                    //_mainEngine.StartGame(false);
                    break;

                default:
                    inputDetails.WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Field Seeding Menu.
        /// </summary>
        public void HandleInputSeedingTypeMenu(GameModel game)
        {
            var inputDetails = game.InputDetails;
            inputDetails.WrongInput = false;

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    _fieldSeedingService.PopulateFieldManually(game);
                    break;

                case ConsoleKey.D2:
                    _fieldSeedingService.PopulateFieldRandomly(game.SingleGame); // ???
                    break;

                case ConsoleKey.D3:
                    _fieldSeedingService.PopulateFieldFromLibrary(game);
                    break;

                case ConsoleKey.Escape:
                    //_mainEngine.StartGame(false);
                    break;

                default:
                    inputDetails.WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Glider Gun Menu.
        /// </summary>
        public void HandleInputGliderGunMenu(GameModel game)
        {
            var inputDetails = game.InputDetails;
            inputDetails.WrongInput = false;

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    game.GameDetails.GliderGunType = 1;
                    game.SingleGame = new(40, 30);
                    game.MultipleGamesField.ListOfGames.Add(game.SingleGame);
                    _library.SpawnGosperGliderGun(game.MultipleGamesField.ListOfGames[0], 1, 1);
                    break;


                case ConsoleKey.D2:
                    game.GameDetails.GliderGunType = 2;
                    game.SingleGame = new(37, 40);
                    game.MultipleGamesField.ListOfGames.Add(game.SingleGame);
                    _library.SpawnSimkinGliderGun(game.MultipleGamesField.ListOfGames[0], 0, 16);
                    break;

                case ConsoleKey.Escape:
                    //_mainEngine.StartGame(false);
                    break;

                default:
                    inputDetails.WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process the Game Field dimensions entered by the user.
        /// </summary>
        /// <param name="wrongInput">Parameter that represents if there was wrong input.</param>
        private void EnterFieldDimensions(GameModel game, bool wrongInput)
        {
            Console.CursorVisible = true;
            Console.Clear();
            while (true)
            {
                if (wrongInput)
                {
                    Console.Clear();
                    _renderingService.ChangeColorWrite(StringConstants.WrongInputPhrase, newLine: false);
                }

                Console.Write(StringConstants.EnterLengthPhrase);
                if (int.TryParse(Console.ReadLine(), out int length) && length > 0)
                {
                    Console.Write(StringConstants.EnterWidthPhrase);
                    if (int.TryParse(Console.ReadLine(), out int width) && width > 0)
                    {
                        Console.CursorVisible = false;
                        game.SingleGame = new(length, width);
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
        public bool EnterCoordinates(GameModel game)
        {
            Console.CursorVisible = true;
            Console.WriteLine(StringConstants.StopSeedingPhrase);
            Console.Write(StringConstants.EnterXPhrase);

            var inputCoordinate = Console.ReadLine();
            if (inputCoordinate == StringConstants.StopWord)
            {
                _fieldSeedingService.StopDataInput = true;
            }
            else if (int.TryParse(inputCoordinate, out var resultX) && resultX >= 0 && resultX < game.SingleGame.Length)
            {
                _fieldSeedingService.CoordinateX = resultX;
                Console.Write(StringConstants.EnterYPhrase);
                inputCoordinate = Console.ReadLine();
                if (inputCoordinate == StringConstants.StopWord)
                {
                    _fieldSeedingService.StopDataInput = true;
                }
                else if (int.TryParse(inputCoordinate, out var resultY) && resultY >= 0 && resultY < game.SingleGame.Width)
                {
                    _fieldSeedingService.CoordinateY = resultY;
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
        public bool HandleInputLibraryMenu(GameModel game)
        {
            var inputDetails = game.InputDetails;

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    _fieldSeedingService.CallSpawningMethod(game, _library.SpawnGlider);
                    return false;

                case ConsoleKey.D2:
                    _fieldSeedingService.CallSpawningMethod(game, _library.SpawnLightWeight);
                    return false;

                case ConsoleKey.D3:
                    _fieldSeedingService.CallSpawningMethod(game, _library.SpawnMiddleWeight);
                    return false;

                case ConsoleKey.D4:
                    _fieldSeedingService.CallSpawningMethod(game, _library.SpawnHeavyWeight);
                    return false;

                case ConsoleKey.Escape:
                    return true;

                default:
                    inputDetails.WrongInput = true;
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
            var inputDetails =
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
        private static void ChangeDelay(GameModel game, ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.LeftArrow:
                    if (game.GameDetails.Delay <= 100 && game.GameDetails.Delay > 10)
                    {
                        game.GameDetails.Delay -= 10;
                    }
                    else if (game.GameDetails.Delay > 100)
                    {
                        game.GameDetails.Delay -= 100;
                    }

                    break;

                case ConsoleKey.RightArrow:
                    if (game.GameDetails.Delay < 2000)
                    {
                        if (game.GameDetails.Delay < 100)
                        {
                            game.GameDetails.Delay += 10;
                        }
                        else
                        {
                            game.GameDetails.Delay += 100;
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
                //case ConsoleKey.S:
                //    if (!_mainEngine.MultipleGamesMode)
                //    {
                //        _mainEngine.MultipleGames.ListOfGames[0].Generation = _mainEngine.MultipleGames.Generation;
                //        _file.SaveGameFieldToFile(_mainEngine.MultipleGames.ListOfGames[0]);
                //        _userInterfaceService.CreateSingleGameRuntimeUI(_mainEngine.MultipleGames, _mainEngine.Delay);
                //        _renderer.RenderMenu(MenuViews.SingleGameUI, clearScreen: true);
                //        _renderer.RenderGridOfFields(_mainEngine.MultipleGames);
                //    }
                //    else
                //    {
                //        _file.SaveMultipleGamesToFile(_mainEngine.MultipleGames);
                //    }

                //    Console.WriteLine(StringConstants.SuccessfullySavedPhrase);
                //    Console.ReadKey();
                //    Console.Clear();
                //    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;

                //case ConsoleKey.R:
                //    _mainEngine.RestartGame();
                //    break;

                //case ConsoleKey.N:
                //    if (multipleGamesMode)
                //    {
                //        _mainEngine.MultipleGames.GamesToBeDisplayed.Clear();
                //        for (int gameNumbersEntered = 0; gameNumbersEntered < _mainEngine.MultipleGames.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                //        {
                //            EnterGameNumbersToBeDisplayed();
                //        }
                //    }

                //    Console.Clear();
                //    break;

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
        private void PauseGame(ConsoleKey keyPressed, bool multipleGamesMode = false)
        {
            if (keyPressed == ConsoleKey.Spacebar)
            {
                _renderingService.RenderMenu(MenuViews.PauseMenu, isMultipleGamesMode: multipleGamesMode);
                var pauseMenuKeyPress = Console.ReadKey(true).Key;
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
                //_mainEngine.RestartGame();
            }
            else if (keyPressed == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Method to take and process the numbers of the games entered by the user.
        /// </summary>
        public void EnterGameNumbersToBeDisplayed(GameModel game)
        {
            string gameNumber;
            Console.CursorVisible = true;
            while (true)
            {
                Console.WriteLine(StringConstants.DashesConstant);
                Console.Write(StringConstants.EnterGameNumberPhrase);
                gameNumber = Console.ReadLine();
                if (int.TryParse(gameNumber, out var number) && number >= 0 && (number < game.MultipleGamesField.TotalNumberOfGames))
                {
                    if (!game.MultipleGamesField.GamesToBeDisplayed.Contains(number))
                    {
                        game.MultipleGamesField.GamesToBeDisplayed.Add(number);
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
        private void HandleInputMultipleGameNumbersMenu(GameModel game)
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    for (int gameNumbersEntered = 0; gameNumbersEntered < game.MultipleGamesField.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                    {
                        EnterGameNumbersToBeDisplayed(game);
                    }

                    break;

                case ConsoleKey.D2:
                    for (int gameNumbersEntered = 0; gameNumbersEntered < game.MultipleGamesField.NumberOfGamesToBeDisplayed; gameNumbersEntered++)
                    {
                        game.MultipleGamesField.GamesToBeDisplayed.Add(gameNumbersEntered);
                    }

                    break;

                case ConsoleKey.Escape:
                    //_mainEngine.StartGame(false);
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input in the Multiple Games Mode field size choosing Menu.
        /// </summary>
        public void HandleInputMultipleGamesMenuFieldSize(GameModel game)
        {
            WrongInput = false;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    game.MultipleGamesField.Length = 10;
                    game.MultipleGamesField.Width = 10;
                    game.MultipleGamesField.NumberOfGamesToBeDisplayed = 24;
                    break;

                case ConsoleKey.D2:
                    game.MultipleGamesField.Length = 15;
                    game.MultipleGamesField.Width = 15;
                    game.MultipleGamesField.NumberOfGamesToBeDisplayed = 12;
                    break;

                case ConsoleKey.D3:
                    game.MultipleGamesField.Length = 20;
                    game.MultipleGamesField.Width = 20;
                    game.MultipleGamesField.NumberOfGamesToBeDisplayed = 6;
                    break;

                case ConsoleKey.D4:
                    game.MultipleGamesField.Length = 25;
                    game.MultipleGamesField.Width = 25;
                    game.MultipleGamesField.NumberOfGamesToBeDisplayed = 6;
                    break;

                case ConsoleKey.Escape:
                    //_mainEngine.StartGame(false);
                    break;

                default:
                    WrongInput = true;
                    break;
            }
        }

        /// <summary>
        /// Method to take and process user's input of the number of games and Game Field sizes for the Multiple Games Mode.
        /// </summary>
        public void EnterMultipleGamesQuantity(GameModel game)
        {

            Console.CursorVisible = true;
            while (true)
            {
                _renderingService.RenderMenu(MenuViews.MultipleGamesModeGamesQuantityMenu, newLine: false, wrongInput: WrongInput);
                if (int.TryParse(Console.ReadLine(), out var totalNumberOfGames) && totalNumberOfGames >= 24 && totalNumberOfGames <= 1000)
                {
                    game.MultipleGamesField.TotalNumberOfGames = totalNumberOfGames;
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
        //public void HandleInputLoadGameMenu(GameModel game)
        //{
        //    WrongInput = false;
        //    switch (Console.ReadKey(true).Key)
        //    {
        //        case ConsoleKey.D1:
        //            _file.InitiateLoadingFromFile(game);

        //            // Move this somewhere later.
        //            game.MultipleGamesField.Length = game.MultipleGamesField.ListOfGames[0].Length;
        //            game.MultipleGamesField.Width = game.MultipleGamesField.ListOfGames[0].Width;
        //            game.MultipleGamesField.TotalNumberOfGames = 1;
        //            game.MultipleGamesField.NumberOfGamesToBeDisplayed = 1;
        //            game.MultipleGamesField.GamesToBeDisplayed.Add(0);

        //            break;

        //        case ConsoleKey.D2:
        //            _file.InitiateLoadingFromFile(game, loadMultipleGames: true);
        //            break;

        //        case ConsoleKey.Escape:
        //            //_mainEngine.StartGame(false);
        //            break;

        //        default:
        //            WrongInput = true;
        //            break;
        //    }
        //}

        public ConsoleKey ProcessRuntimeKeypress(GameModel game)
        {
            var keyPressed = Console.ReadKey(true).Key;
            PauseGame(keyPressed, game.GameDetails.IsMultipleGamesMode);
            ChangeDelay(game, keyPressed);
            return keyPressed;
        }
    }
}
