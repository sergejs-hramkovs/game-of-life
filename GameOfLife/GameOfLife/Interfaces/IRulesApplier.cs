using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IRulesApplier
    {
        void DetermineCellsDestiny(GameFieldModel gameField, bool disableWrappingAroundField);

        void FieldRefresh(GameFieldModel gameField);
    }
}
