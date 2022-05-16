using GameOfLife.Interfaces;
using GameOfLife.Models;

namespace GameOfLife.Views
{
    /// <summary>
    /// The UserInterfaceFiller class fills the UI with necessary parameters.
    /// </summary>
    [Serializable]
    public class UserInterfaceFiller : IUserInterfaceFiller
    {
        /// <summary>
        /// Method to fill the runtime UI with relevant and current information about the Game Field.
        /// </summary>
        /// <param name="gameField">A Game Field object filled with cells.</param>
        /// <param name="delay">Delay in miliseconds between generations.</param>
        public void SingleGameRuntimeUICreator(MultipleGamesModel multipleGames, int delay)
        {
            GameFieldModel gameField = multipleGames.ListOfGames[0];
            MenuViews.SingleGameUI = new string[]
            {
                " # Press ESC to stop",
                " # Press Spacebar to pause",
                " # Change the delay using left and right arrows",
                $"\n Generation: {multipleGames.Generation}",
                $" Alive cells: {gameField.AliveCellsNumber}({(int)Math.Round(gameField.AliveCellsNumber / (double)gameField.Area * 100.0)}%)   ",
                $" Dead cells: {gameField.DeadCellsNumber}   ",
                $" Current delay between generations: {delay / 1000.0} seconds  "
            };
        }

        /// <summary>
        /// Method to fill the runtime UI in the Multiple Games Mode with relevant information about the Game Fields.
        /// </summary>
        /// <param name="delay">Delay in miliseconds between generations.</param>
        /// <param name="multipleGames">An object with a list of Game Fields.</param>
        public void MultiGameRuntimeUICreator(MultipleGamesModel multipleGames, int delay)
        {
            MenuViews.MultiGameUI = new string[]
            {
                $"Delay: {delay}  ",
                $"Generation: {multipleGames.Generation}   ",
                $"Fields alive: {multipleGames.NumberOfFieldsAlive}   ",
                $"Total alive cells: {multipleGames.TotalCellsAlive}   "
            };
        }

        /// <summary>
        /// Method to fill the Choose File Menu with the list of file names.
        /// </summary>
        /// <param name="numberOfFiles">The number of saved games files.</param>
        /// <param name="fileNames">The list of names of saved games files.</param>
        public void ChooseFileMenuCreation(int numberOfFiles, List<string> fileNames)
        {
            MenuViews.ChooseFileMenu = new string[6 + fileNames.Count];
            MenuViews.ChooseFileMenu[0] = " ### Choose which saved game to load ###";
            MenuViews.ChooseFileMenu[1] = $"\n # There are currently {numberOfFiles} files";
            MenuViews.ChooseFileMenu[2] = StringConstants.DashesConstant;
            for (int i = 0; i < fileNames.Count; i++)
            {
                MenuViews.ChooseFileMenu[i + 3] = " - " + fileNames[i];
            }

            MenuViews.ChooseFileMenu[3 + fileNames.Count] = StringConstants.DashesConstant;
            MenuViews.ChooseFileMenu[4 + fileNames.Count] = "\n # Choose the number of the file";
            MenuViews.ChooseFileMenu[5 + fileNames.Count] = "\n # Choice: ";
        }
    }
}
