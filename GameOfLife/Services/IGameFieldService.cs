using GameOfLife.Entities.Models;
using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// The RulesApplier class deals with the apllication of the rules of the game to cells.
    /// </summary>
    public interface IGameFieldService
    {
        /// <summary>
        /// Method to go through all the field cells and call the corresponding method if the cell is alive or dead.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="disableWrappingAroundField">Parameter that shows if field's wrapping around is disabled.</param>
        void IterateThroughGameFieldCells(SingleGameField gameField, bool disableWrappingAroundField = false);

        /// <summary>
        /// Removes or creates new cells according to the rules.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        void RefreshField(SingleGameField gameField);

        void PerformRuntimeCalculations(GameModel game);

        void CountTotalAliveCells(MultipleGamesField multipleGamesField);

        void RemoveDeadFieldsFromRendering(MultipleGamesField multipleGames, List<int> aliveFields);
    }
}
