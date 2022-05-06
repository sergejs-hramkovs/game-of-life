using GameOfLife.Interfaces;
using GameOfLife.Models;

namespace GameOfLife.Views
{
    /// <summary>
    /// The UserInterfaceViews class stores arrays of UI lines which are displayed during the runtime of the game.
    /// </summary>
    public class UserInterfaceViews : IUserInterfaceViews
    {
        private GameFieldModel _gameField;
        private int _delay;

        public string[] SingleGameUI { get; set; }
        public string[] MultiGameUI { get; set; }

        /// <summary>
        /// Method to fill the runtime UI with relevant and current information about the Game Field.
        /// </summary>
        /// <param name="gameField">A Game Field object filled with cells.</param>
        /// <param name="delay">Delay in miliseconds between generations.</param>
        public void SingleGameRuntimeUIParameterInitialization(GameFieldModel gameField, int delay)
        {
            _gameField = gameField;
            _delay = delay;
            SingleGameUI = new string[]
            {
                " # Press ESC to stop",
                " # Press Spacebar to pause",
                " # Change the delay using left and right arrows",
                $"\n Generation: {_gameField.Generation}",
                $" Alive cells: {_gameField.AliveCellsNumber}({(int)Math.Round(_gameField.AliveCellsNumber / (double)_gameField.Area * 100.0)}%)   ",
                $" Dead cells: {_gameField.DeadCellsNumber}   ",
                $" Current delay between generations: {_delay / 1000.0} seconds  ",
                $" Number of generations per second: {Math.Round(1 / (_delay / 1000.0), 2)}   "
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="multipleGames">An object with a list of Game Fields.</param>
        public void MultiGameRuntimeUIParameterInitialization(int delay, MultipleGamesModel multipleGames)
        {
            MultiGameUI = new string[]
            {
                $"Delay: {delay}  ",
                $"Generation: {multipleGames.Generation}   ",
                $"Fields alive: {multipleGames.NumberOfFieldsAlive}   ",
                $"Total alive cells: {multipleGames.TotalCellsAlive}   "
            };
        }
    }
}
