using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// The MenuNavigator class deals with transitions between the UI menus.
    /// </summary>
    public interface IMenuNavigator
    {
        /// <summary>
        /// Method to inject the required objects into the MenuNavigator class.
        /// </summary>
        /// <param name="renderer">An object of the Renderer class.</param>
        /// <param name="inputController">An object of the InputController class.</param>
        /// <param name="engine">An object of the Engine class.</param>
        /// <param name="fieldOperations">An object of the FieldOperations class.</param>
        /// <param name="file">An object of the FileIO class.</param>
        /// <param name="userInterfaceFiller">An object of the UserInterfaceFiller class.</param>
        void Inject(IRenderer renderer, IInputController inputController, IMainEngine engine, IFieldOperations fieldOperations,
            IFileIO file, IUserInterfaceFiller userInterfaceFiller);

        /// <summary>
        /// General method to navigate through the menus.
        /// </summary>
        /// <param name="menu">An instance of a menu to be displayed.</param>
        /// <param name="HandleInput">Method to handle the user's input in the menu.</param>
        /// <param name="clearScr">Parameter that defines if the screen is cleared, 'true' by default.</param>
        /// <param name="Render">Optional parameter to pass the field rendering method.</param>
        public void NavigateMenu(string[] menu, Action HandleInput, bool clearScr = true, Action<MultipleGamesModel, bool>? Render = null, bool fileMissing = false);

        /// <summary>
        /// Method to navigate through all the Multiple Games Mode menus.
        /// </summary>
        void NavigateMultipleGamesMenu();

        /// <summary>
        /// Method to navigate through the Exit Menu.
        /// </summary>
        /// <param name="gameIsOver">Parameter that represents if the 'Game Over' state has been reached.</param>
        void NavigateExitMenu(bool gameIsOver);

        /// <summary>
        /// Method to navigate through the Saved Games Menu.
        /// </summary>
        /// <param name="filePath">Parameter that stores the path to the folder with the Saved Games.</param>
        void NavigateSavedGameMenu(string filePath);
    }
}
