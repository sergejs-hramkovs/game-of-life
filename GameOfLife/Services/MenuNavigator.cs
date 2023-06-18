using GameOfLife.Interfaces;
using GameOfLife.Views;

namespace GameOfLife.Services
{
    /// <summary>
    /// The MenuNavigator class deals with transitions between the UI menus.
    /// </summary>
    public class MenuNavigator : IMenuNavigator
    {
        private readonly IRenderingService _renderingService;
        private readonly IInputProcessorService _inputProcessorService;
        private readonly IMainEngine _engine;
        private readonly IFieldOperations _fieldOperations;
        private readonly IFileIO _file;
        private readonly IUIService _userInterfaceService;

        /// <summary>
        /// Method to inject the required objects into the MenuNavigator class.
        /// </summary>
        /// <param name="renderer">An object of the Renderer class.</param>
        /// <param name="inputController">An object of the InputController class.</param>
        /// <param name="engine">An object of the Engine class.</param>
        /// <param name="fieldOperations">An object of the FieldOperations class.</param>
        /// <param name="file">An object of the FileIO class.</param>
        /// <param name="userInterfaceService">An object of the UserInterfaceFiller class.</param>
        public MenuNavigator(
            IRenderingService renderer,
            IInputProcessorService inputController,
            IMainEngine engine,
            IFieldOperations fieldOperations,
            IFileIO file,
            IUIService userInterfaceService)
        {
            _renderingService = renderer;
            _inputProcessorService = inputController;
            _engine = engine;
            _fieldOperations = fieldOperations;
            _file = file;
            _userInterfaceService = userInterfaceService;
        }

        /// <summary>
        /// General method to navigate through the menus.
        /// </summary>
        /// <param name="menu">An instance of a menu to be displayed.</param>
        /// <param name="clearMenuFromScreen">Parameter that defines if the screen is cleared, 'true' by default.</param>
        /// <param name="Render">Optional parameter to pass the field rendering method.</param>
        public void NavigateMenu(string[] menu, bool clearMenuFromScreen = true, bool fileMissing = false)
        {
            do
            {
                _renderingService.RenderMenu(menu, wrongInput: _inputProcessorService.WrongInput, clearScreen: clearMenuFromScreen, noSavedGames: fileMissing);
                _inputProcessorService.HandleInputMainMenu();
            } while (_inputProcessorService.WrongInput);
        }

        /// <summary>
        /// Method to navigate through all the Multiple Games Mode menus.
        /// </summary>
        public void NavigateMultipleGamesMenu()
        {
            _inputProcessorService.EnterMultipleGamesQuantity();
            NavigateMenu(MenuViews.MultipleGamesModeFieldSizeChoiceMenu);
            _engine.MultipleGames.InitializeGames(_fieldOperations);
            NavigateMenu(MenuViews.MultipleGamesModeMenu);
        }

        /// <summary>
        /// Method to navigate through the Exit Menu.
        /// </summary>
        /// <param name="gameIsOver">Parameter that represents if the 'Game Over' state has been reached.</param>
        public void NavigateExitMenu(bool gameIsOver)
        {
            ConsoleKey runTimeKeyPress;
            _renderingService.RenderMenu(MenuViews.ExitMenu, clearScreen: false, gameOver: gameIsOver);
            do
            {
                runTimeKeyPress = Console.ReadKey(true).Key;
                _inputProcessorService.HandleInputExitMenu(runTimeKeyPress);
            } while (runTimeKeyPress != ConsoleKey.Escape || runTimeKeyPress != ConsoleKey.R);
        }

        /// <summary>
        /// Method to navigate through the Saved Games Menu.
        /// </summary>
        /// <param name="filePath">Parameter that stores the path to the folder with the Saved Games.</param>
        public void NavigateSavedGameMenu(string filePath)
        {
            do
            {
                Console.CursorVisible = true;
                _file.CreateListOfFileNames(filePath);
                _userInterfaceService.CreateFileChoosingMenu(_file.NumberOfFiles, MenuViews.FileNames);
                _renderingService.RenderMenu(MenuViews.ChooseFileMenu, newLine: false, clearScreen: true, wrongInput: _inputProcessorService.WrongInput);
                MenuViews.FileNames.Clear();
                _file.FileNumber = _inputProcessorService.HandleInputSavedGameMenu(_file.NumberOfFiles);
                Console.CursorVisible = false;
            } while (_inputProcessorService.WrongInput);
        }
    }
}
