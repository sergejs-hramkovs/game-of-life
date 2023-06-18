using GameOfLife.Entities.Models;
using GameOfLife.Interfaces;
using GameOfLife.Models;

namespace GameOfLife.Views
{
    /// <summary>
    /// The UserInterfaceFiller class fills the UI with necessary parameters.
    /// </summary>
    [Serializable]
    public class UIService : IUIService
    {
        public UIService()
        {

        }

        /// <summary>
        /// Method that is responsible for creation and displaying of the runtime UI and the Game Field(s).
        /// </summary>
        public void CreateRuntimeView(GameModel game)
        {
            Console.SetCursorPosition(0, 0);
            CountTotalAliveCells(game.MultipleGamesField);
            if (!game.GameDetails.IsMultipleGamesMode)
            {
                _userInterfaceService.CreateSingleGameRuntimeUI(game.MultipleGamesField, game.GameDetails.Delay);
                _renderer.RenderMenu(MenuViews.SingleGameUI, clearScreen: false);
            }
            else
            {
                _userInterfaceService.CreateMultiGameRuntimeUI(game.MultipleGamesField, game.GameDetails.Delay);
                _renderer.RenderMenu(MenuViews.MultiGameUI, clearScreen: false);
            }

            _renderer.RenderGridOfFields(game.MultipleGamesField);
            RemoveDeadFieldsFromRendering(game.MultipleGamesField, game.MultipleGamesField.AliveFields);
        }

        /// <summary>
        /// Method to fill the runtime UI with relevant and current information about the Game Field.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that stores the list of Game Fields.</param>
        /// <param name="delay">Delay in miliseconds between generations.</param>
        public void CreateSingleGameUI(MultipleGamesField multipleGames, int delay)
        {
            SingleGameField gameField = multipleGames.ListOfGames[0];
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
        /// <param name="multipleGames">A MultipleGamesModel object that stores the list of Game Fields.</param>
        /// <param name="delay">Delay in miliseconds between generations.</param>
        public void CreateMultipleGamesUI(MultipleGamesField multipleGames, int delay)
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
        public void CreateFileLoadingMenu(int numberOfFiles, List<string> fileNames)
        {
            MenuViews.ChooseFileMenu = new string[7 + fileNames.Count];
            MenuViews.ChooseFileMenu[0] = StringConstants.WrongInputPhrase;
            MenuViews.ChooseFileMenu[1] = " ### Choose which saved game to load ###";
            MenuViews.ChooseFileMenu[2] = $"\n # There are currently {numberOfFiles} files";
            MenuViews.ChooseFileMenu[3] = StringConstants.DashesConstant;
            for (int i = 0; i < fileNames.Count; i++)
            {
                MenuViews.ChooseFileMenu[i + 4] = " - " + fileNames[i];
            }

            MenuViews.ChooseFileMenu[4 + fileNames.Count] = StringConstants.DashesConstant;
            MenuViews.ChooseFileMenu[5 + fileNames.Count] = "\n # Choose the number of the file";
            MenuViews.ChooseFileMenu[6 + fileNames.Count] = "\n # Choice: ";
        }
    }
}