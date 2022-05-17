using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IAuxiliaryEngine
    {
        void Inject(IMainEngine engine, IRulesApplier rulesApplier, IRenderer renderer, IUserInterfaceFiller userInterfaceFiller);

        void PerformRuntimeCalculations();

        void CreateRuntimeView();

        void CountAliveCells(GameFieldModel gameField);

        void CountTotalAliveCells(MultipleGamesModel multipleGames);

        void RemoveDeadFieldsFromRendering(MultipleGamesModel multipleGames);
    }
}
