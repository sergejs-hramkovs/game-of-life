using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// InputController class takes and processes user's input.
    /// </summary>
    public interface IInputProcessorService
    {
        bool WrongInput { get; set; }

        bool CorrectKeyPressed { get; set; }

        GameFieldModel GameField { get; set; }

        /// <summary>
        /// Method to take and process user's input in the Main Menu.
        /// </summary>
        void HandleInputMainMenu();

        /// <summary>
        /// Method to take and process user's input in the Field Seeding Menu.
        /// </summary>
        void HandleInputSeedingTypeMenu();

        /// <summary>
        /// Method to take and process user's input in the Glider Gun Menu.
        /// </summary>
        void HandleInputGliderGunMenu();

        /// <summary>
        /// Method to take and process the Game Field dimensions entered by the user.
        /// </summary>
        /// <param name="wrongInput">Parameter that represents if there was wrong input.</param>
        void EnterFieldDimensions(bool wrongInput);

        /// <summary>
        /// Method to take and process the coordinates of cells or library objects entered by the user.
        /// </summary>
        /// <returns>Returns "stop = true" if the process of entering coordinates was stopped. Returns false if there was wrong input.</returns>
        bool EnterCoordinates();

        /// <summary>
        /// Method to take and process the numbers of the games entered by the user.
        /// </summary>
        void EnterGameNumbersToBeDisplayed();

        /// <summary>
        /// Method to take and process user's input in the Library Menu.
        /// </summary>
        /// <returns>Returns 'true' if the 'Escape' key is pressed, otherwise 'false'</returns>
        bool HandleInputLibraryMenu();

        /// <summary>
        /// Method to take and process user's input in the Pause Menu.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores the key pressed in the Pause Menu.</param>
        /// <param name="multipleGamesMode">Parameter that represents if the Multiple Games Mode is enabled, 'false' by default.</param>
        void HandleInputPauseMenu(ConsoleKey keyPressed, bool multipleGamesMode = false);

        /// <summary>
        /// Method to take and process user's input in the Multiple Games Mode Menu.
        /// </summary>
        void HandleInputMultipleGameNumbersMenu();

        /// <summary>
        /// Method to pause the game by pressing the Spacebar.
        /// </summary>
        /// <param name="keyPressed">Parameter which stores Spacebar key press.</param>
        /// <param name="multipleGamesMode">Parameter that represents if the Multiple Games Mode is enabled, 'false' by default.</param>
        void PauseGame(ConsoleKey keyPressed, bool multipleGamesMode = false);

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
        /// Method to change the time delay between generations if LeftArrow or RightArrow keys are pressed.
        /// </summary>
        /// <param name="keyPressed">Parameters which stores Left and Right Arrow key presses.</param>
        void ChangeDelay(ConsoleKey keyPressed);

        /// <summary>
        /// Method to take and process user's input of the number of games and Game Field sizes for the Multiple Games Mode.
        /// </summary>
        void EnterMultipleGamesQuantity();

        /// <summary>
        /// Method to take and process user's input in the Multiple Games Mode field size choosing Menu.
        /// </summary>
        void HandleInputMultipleGamesMenuFieldSize();

        /// <summary>
        /// Method to deal with key presses for pause or delay changing during the runtime.
        /// </summary>
        /// <param name="multipleGamesMode">Parameter that represents if the Multiple Games Mode is enabled, 'false' by default.</param>
        /// <returns>Returns the pressed key.</returns>
        ConsoleKey ReadKeyRuntime(bool multipleGamesMode = false);

        /// <summary>
        /// Method to take and process user's input in the Single Game Menu.
        /// </summary>
        void HandleInputSingleGameMenu();

        /// <summary>
        /// Method to take and process user's input in the Load Game Menu.
        /// </summary>
        void HandleInputLoadGameMenu();
    }
}
