using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IRenderer
    {
        void Injection(IMainEngine engine);

        void MenuRenderer(string[] menuLines, bool wrongInput = false, bool fileNotFound = false,
            bool noSavedGames = false, bool clearScreen = true, bool multipleGames = false, bool newLine = true, bool gameOver = false);

        void GridOfFieldsRenderer(MultipleGamesModel multipleGames, bool clearScreen = false);
    }
}
