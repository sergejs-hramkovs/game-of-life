using GameOfLife.Entities.Models;
using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IAuxiliaryEngine
    {
        /// <summary>
        /// Method to perform the required runtime actions, like applying game rules and counting alive cells of each field.
        /// </summary>
        void PerformRuntimeCalculations();

        /// <summary>
        /// Method that is responsible for creation and displaying of the runtime UI and the Game Field(s).
        /// </summary>
        void CreateRuntimeView(GameModel game);

        /// <summary>
        /// Method to count the number of alive cells on one field.
        /// </summary>
        /// <param name="gameField">A GameFieldModel object that contains the Game Field.</param>
        void CountAliveCells(SingleGameField gameField);

        /// <summary>
        /// Method to count total alive cells number on all the fields in the Multiple Games Mode.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        void CountTotalAliveCells(MultipleGamesField multipleGames);

        /// <summary>
        /// Method to replace rendered dead fields with alive ones in the list of fields to be displayed.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        void RemoveDeadFieldsFromRendering(MultipleGamesField multipleGames);
    }
}
