using GameOfLife.Entities.Models;
using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// The UserInterfaceFiller class fills the UI with necessary parameters.
    /// </summary>
    public interface IUIService
    {
        void CreateRuntimeView(GameModel game);

        /// <summary>
        /// Method to fill the runtime UI with relevant and current information about the Game Field.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that stores the list of Game Fields.</param>
        /// <param name="delay">Delay in miliseconds between generations.</param>
        void CreateSingleGameUI(MultipleGamesField multipleGames, int delay);

        /// <summary>
        /// Method to fill the runtime UI in the Multiple Games Mode with relevant information about the Game Fields.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that stores the list of Game Fields.</param>
        /// <param name="delay">Delay in miliseconds between generations.</param>
        void CreateMultipleGamesUI(MultipleGamesField multipleGames, int delay);

        /// <summary>
        /// Method to fill the Choose File Menu with the list of file names.
        /// </summary>
        /// <param name="numberOfFiles">The number of saved games files.</param>
        /// <param name="fileNames">The list of names of saved games files.</param>
        void CreateFileLoadingMenu(int numberOfFiles, List<string> fileNames);
    }
}
