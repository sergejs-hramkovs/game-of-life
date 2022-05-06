using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IRenderer
    {
        void MenuRenderer(string[] menuLines, bool wrongInput = false, bool fileNotFound = false,
            bool noSavedGames = false, bool clearScreen = true, bool multipleGames = false, bool newLine = true);

        void RenderField(GameFieldModel gameField, bool dead = false);

        void GameOverRender(int generation);

        void ChooseFileToLoadMenuRender(int numberOfFiles, string filePath, bool wrongInput);

        int RenderMultipleHorizontalFields(MultipleGamesModel multipleGames, int rowNumber);
    }
}
