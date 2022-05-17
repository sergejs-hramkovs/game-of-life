using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IUserInterfaceFiller
    {
        void CreateSingleGameRuntimeUI(MultipleGamesModel multipleGames, int delay);

        void CreateMultiGameRuntimeUI(MultipleGamesModel multipleGames, int delay);

        void CreateFileChoosingMenu(int numberOfFiles, List<string> fileNames);
    }
}
