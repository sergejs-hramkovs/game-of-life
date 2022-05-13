using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IAuxiliaryEngine
    {
        void Injection(IMainEngine engine, IRulesApplier rulesApplier, IRenderer renderer, IUserInterfaceFiller userInterfaceFiller);

        void RuntimeCalculations();

        void RuntimeViewCreator();

        void CountAliveCells(GameFieldModel gameField);

        void CountTotalAliveCells(MultipleGamesModel multipleGames);

        void RemoveDeadFieldsFromRendering(MultipleGamesModel multipleGames);
    }
}
