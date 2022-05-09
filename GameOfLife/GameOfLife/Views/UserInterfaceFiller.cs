using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;
using static GameOfLife.Views.MenuViews;

namespace GameOfLife.Views
{
    /// <summary>
    /// The UserInterfaceFiller class fills the UI with necessary parameters.
    /// </summary>
    public class UserInterfaceFiller : IUserInterfaceFiller
    {
        /// <summary>
        /// Method to fill the runtime UI with relevant and current information about the Game Field.
        /// </summary>
        /// <param name="gameField">A Game Field object filled with cells.</param>
        /// <param name="delay">Delay in miliseconds between generations.</param>
        public void SingleGameRuntimeUICreation(GameFieldModel gameField, int delay)
        {
            SingleGameUI = new string[]
            {
                " # Press ESC to stop",
                " # Press Spacebar to pause",
                " # Change the delay using left and right arrows",
                $"\n Generation: {gameField.Generation}",
                $" Alive cells: {gameField.AliveCellsNumber}({(int)Math.Round(gameField.AliveCellsNumber / (double)gameField.Area * 100.0)}%)   ",
                $" Dead cells: {gameField.DeadCellsNumber}   ",
                $" Current delay between generations: {delay / 1000.0} seconds  ",
                $" Number of generations per second: {Math.Round(1 / (delay / 1000.0), 2)}   "
            };
        }

        /// <summary>
        /// Method to fill the runtime UI in the Multiple Games Mode with relevant information about the Game Fields.
        /// </summary>
        /// <param name="delay">Delay in miliseconds between generations.</param>
        /// <param name="multipleGames">An object with a list of Game Fields.</param>
        public void MultiGameRuntimeUICreation(int delay, MultipleGamesModel multipleGames)
        {
            MultiGameUI = new string[]
            {
                $"Delay: {delay}  ",
                $"Generation: {multipleGames.Generation}   ",
                $"Fields alive: {multipleGames.NumberOfFieldsAlive}   ",
                $"Total alive cells: {multipleGames.TotalCellsAlive}   "
            };
        }

        /// <summary>
        /// Method to fill the Game Over UI with the number of survived generations.
        /// </summary>
        /// <param name="generation">The number of survived generations.</param>
        public void GameOverUICreation(int generation)
        {
            GameOverUI[GameOverUI.Length - 1] = $"\n Generations survived: {generation}";
        }

        /// <summary>
        /// Method to fill the Choose File Menu with the list of file names.
        /// </summary>
        /// <param name="numberOfFiles">The number of saved games files.</param>
        /// <param name="fileNames">The list of names of saved games files.</param>
        public void ChooseFileMenuCreation(int numberOfFiles, List<string> fileNames)
        {
            ChooseFileMenu = new string[6 + fileNames.Count];
            ChooseFileMenu[0] = " ### Choose which saved game to load ###";
            ChooseFileMenu[1] = $"\n # There are currently {numberOfFiles} files";
            ChooseFileMenu[2] = DashesConstant;
            for (int i = 0; i < fileNames.Count; i++)
            {
                ChooseFileMenu[i + 3] = " - " + fileNames[i];
            }

            ChooseFileMenu[3 + fileNames.Count] = DashesConstant;
            ChooseFileMenu[4 + fileNames.Count] = "\n # Choose the number of the file";
            ChooseFileMenu[5 + fileNames.Count] = "\n # Choice: ";
        }
    }
}
