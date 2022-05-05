using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IRulesApplier
    {
        void IterateThroughGameFieldCells(GameFieldModel gameField, bool disableWrappingAroundField);

        void FieldRefresh(GameFieldModel gameField);
    }
}
