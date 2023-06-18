using GameOfLife.Entities.Models;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// The MenuNavigator class deals with transitions between the UI menus.
    /// </summary>
    public interface IMenuNavigator
    {
        /// <summary>
        /// General method to navigate through the menus.
        /// </summary>
        /// <param name="menu">An instance of a menu to be displayed.</param>
        /// <param name="HandleInput">Method to handle the user's input in the menu.</param>
        /// <param name="clearMenuFromScreen">Parameter that defines if the screen is cleared, 'true' by default.</param>
        /// <param name="Render">Optional parameter to pass the field rendering method.</param>
        public void NavigateMenu(GameModel game, string[] menu, bool clearMenuFromScreen = true, bool fileMissing = false);

        /// <summary>
        /// Method to navigate through all the Multiple Games Mode menus.
        /// </summary>
        void NavigateMultipleGamesMenu(GameModel game);

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
