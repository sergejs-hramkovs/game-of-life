using GameOfLife.Entities.Models;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// The Render class deals with the rendering of the User Interface.
    /// </summary>
    public interface IRenderingService
    {
        /// <summary>
        /// Method to iterate through an array of UI menu lines and to dispaly them.
        /// </summary>
        /// <param name="menuLines">An array of UI menu lines.</param>
        /// <param name="wrongInput">Parameter that shows if the was wrong input attempt, 'false' by default.</param>
        /// <param name="noSavedGames">Parameter that shows if the Saved Games files are missing, ''false' by default.</param>
        /// <param name="clearScreen">Parameter that states if the screen is to be cleared, 'false' by default.</param>
        /// <param name="isMultipleGamesMode">Parameter that shows if the Multiple Games Mode is enabled, 'false' by default.</param>
        /// <param name="newLine">Parameter to disable jumping to a new line during input, 'true' by default.</param>
        /// <param name="gameOver">Parameter that represents if the 'Game Over' state has been reached, 'false' by default.</param>
        void RenderMenu(
            string[] menuLines,
            bool wrongInput = false,
            bool noSavedGames = false,
            bool clearScreen = false,
            bool isMultipleGamesMode = false,
            bool newLine = true,
            bool gameOver = false);

        /// <summary>
        /// Method to render several rows of Game Fields.
        /// </summary>
        /// <param name="multipleGames">An object that contains a list of Game Fields.</param>
        /// <param name="clearScreen">Parameter that states if the screen is to be cleared, 'false' by default.</param>
        void RenderGridOfFields(GameModel game, bool clearScreen = false);

        /// <summary>
        /// Method to change text color, write text and change the color back.
        /// </summary>
        /// <param name="textToWrite">Parameter that represents a string to be written.</param>
        public void ChangeColorWrite(string textToWrite, bool newLine);
    }
}
