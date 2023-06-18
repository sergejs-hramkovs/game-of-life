using GameOfLife.Entities.Models;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// InputController class takes and processes user's input.
    /// </summary>
    public interface IInputProcessorService
    {
        bool WrongInput { get; set; }

        bool CorrectKeyPressed { get; set; }

        /// <summary>
        /// Method to take and process user's input in the Main Menu.
        /// </summary>
        void HandleInputMainMenu(GameModel game);

        /// <summary>
        /// Method to take and process the coordinates of cells or library objects entered by the user.
        /// </summary>
        /// <returns>Returns "stop = true" if the process of entering coordinates was stopped. Returns false if there was wrong input.</returns>
        bool EnterCoordinates(GameModel game);

        /// <summary>
        /// Method to take and process user's input in the Library Menu.
        /// </summary>
        /// <returns>Returns 'true' if the 'Escape' key is pressed, otherwise 'false'</returns>
        bool HandleInputLibraryMenu(GameModel game);

        /// <summary>
        /// Method to take and process user's input in the Pause Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores the key pressed in the Pause Menu.</param>
        /// <param name="multipleGamesMode">Parameter that represents if the Multiple Games Mode is enabled, 'false' by default.</param>
        void HandleInputPauseMenu(ConsoleKey keyPressed, bool multipleGamesMode = false);

        /// <summary>
        /// Method to take and process the user's choice of the Saved Game file.
        /// </summary>
        /// <param name="numberOfFiles">The number of saved game files currently in the folder.</param>
        /// <returns>Returns the number of the Saved Game file to load.</returns>
        int HandleInputSavedGameMenu(int numberOfFiles);

        /// <summary>
        /// Method to take and process user's input in the Exit Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores the key pressed in the Exit Menu.</param>
        void HandleInputExitMenu(ConsoleKey keyPressed);

        /// <summary>
        /// Method to take and process user's input of the number of games and Game Field sizes for the Multiple Games Mode.
        /// </summary>
        void EnterMultipleGamesQuantity(GameModel game);

        ConsoleKey ProcessRuntimeKeypress(GameModel game);
    }
}
