using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IUserInterfaceViews
    {
        string[] SingleGameUI { get; set; }

        string[] MultiGameUI { get; set; }

        void SingleGameRuntimeUIParameterInitialization(GameFieldModel gameField, int delay);

        void MultiGameRuntimeUIParameterInitialization(int delay, MultipleGamesModel multipleGames);
    }
}
