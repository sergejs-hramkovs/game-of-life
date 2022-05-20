using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Views;

namespace GameOfLife.Services
{
    /// <summary>
    /// The MenuNavigator class deals with transitions between the UI menus.
    /// </summary>
    public class MenuNavigator : IMenuNavigator
    {
        private IRenderer _renderer;
        private IInputController _inputController;
        private IMainEngine _engine;
        private IFieldOperations _fieldOperations;
        private IFileIO _file;
        private IUserInterfaceFiller _userInterfaceFiller;

        /// <summary>
        /// Method to inject the required objects into the MenuNavigator class.
        /// </summary>
        /// <param name="renderer">An object of the Renderer class.</param>
        /// <param name="inputController">An object of the InputController class.</param>
        /// <param name="engine">An object of the Engine class.</param>
        /// <param name="fieldOperations">An object of the FieldOperations class.</param>
        /// <param name="file">An object of the FileIO class.</param>
        /// <param name="userInterfaceFiller">An object of the UserInterfaceFiller class.</param>
        public void Inject(IRenderer renderer, IInputController inputController, IMainEngine engine, IFieldOperations fieldOperations,
            IFileIO file, IUserInterfaceFiller userInterfaceFiller)
        {
            _renderer = renderer;
            _inputController = inputController;
            _engine = engine;
            _fieldOperations = fieldOperations;
            _file = file;
            _userInterfaceFiller = userInterfaceFiller;
        }

        /// <summary>
        /// General method to navigate through the menus.
        /// </summary>
        /// <param name="menu">An instance of a menu to be displayed.</param>
        /// <param name="HandleInput">Method to handle the user's input in the menu.</param>
        /// <param name="clearScreen">Parameter that defines if the screen is cleared, 'true' by default.</param>
        /// <param name="Render">Optional parameter to pass the field rendering method.</param>
        public void NavigateMenu(string[] menu, Action HandleInput, bool clearScreen = true, Action<MultipleGamesModel, bool>? Render = null, bool fileMissing = false)
        {
            do
            {
                Render?.Invoke(_engine.MultipleGames, !clearScreen);
                _renderer.RenderMenu(menu, wrongInput: _inputController.WrongInput, clearScreen: clearScreen, noSavedGames: fileMissing);
                HandleInput();
            } while (_inputController.WrongInput);
        }

        /// <summary>
        /// Method to navigate through all the Multiple Games Mode menus.
        /// </summary>
        public void NavigateMultipleGamesMenu()
        {
            _inputController.EnterMultipleGamesQuantity();
            NavigateMenu(MenuViews.MultipleGamesModeFieldSizeChoiceMenu, _inputController.HandleInputMultipleGamesMenuFieldSize);
            _engine.MultipleGames.InitializeGames(_fieldOperations);
            NavigateMenu(MenuViews.MultipleGamesModeMenu, _inputController.HandleInputMultipleGameNumbersMenu);
        }

        /// <summary>
        /// Method to navigate through the Exit Menu.
        /// </summary>
        /// <param name="gameIsOver">Parameter that represents if the 'Game Over' state has been reached.</param>
        public void NavigateExitMenu(bool gameIsOver)
        {
            ConsoleKey runTimeKeyPress;
            _renderer.RenderMenu(MenuViews.ExitMenu, clearScreen: false, gameOver: gameIsOver);
            do
            {
                runTimeKeyPress = Console.ReadKey(true).Key;
                _inputController.HandleInputExitMenu(runTimeKeyPress);
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
                _userInterfaceFiller.CreateFileChoosingMenu(_file.NumberOfFiles, MenuViews.FileNames);
                _renderer.RenderMenu(MenuViews.ChooseFileMenu, newLine: false, clearScreen: true, wrongInput: _inputController.WrongInput);
                MenuViews.FileNames.Clear();
                _file.FileNumber = _inputController.HandleInputSavedGameMenu(_file.NumberOfFiles);
                Console.CursorVisible = false;
            } while (_inputController.WrongInput);
        }
    }
}
