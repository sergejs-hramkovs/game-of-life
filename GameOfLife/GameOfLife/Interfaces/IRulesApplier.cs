using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IRulesApplier
    {
        void IterateThroughGameFieldCells(GameFieldModel gameField, bool disableWrappingAroundField = false);

        void RefreshField(GameFieldModel gameField);
    }
}
