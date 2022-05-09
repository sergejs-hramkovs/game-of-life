using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IUserInterfaceFiller
    {
        void SingleGameRuntimeUICreation(GameFieldModel gameField, int delay);

        void MultiGameRuntimeUICreation(int delay, MultipleGamesModel multipleGames);

        void GameOverUICreation(int generation);

        void ChooseFileMenuCreation(int numberOfFiles, List<string> fileNames);
    }
}
