using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IRenderer
    {
        void MenuRenderer(string[] menuLines, bool wrongInput = false, bool fileNotFound = false,
            bool noSavedGames = false, bool clearScreen = true, bool multipleGames = false, bool newLine = true);

        void RenderField(GameFieldModel gameField, bool dead = false);

        void RuntimeUIRender(GameFieldModel gameField, int delay);

        void GameOverRender(int generation);

        void MultipleGamesModeUIRender(int delay, int generation, int numberOfFieldsAlive, int totalCellsAlive);

        void ChooseFileToLoadMenuRender(int numberOfFiles, string filePath, bool wrongInput);

        int RenderMultipleHorizontalFields(MultipleGamesModel multipleGames, int rowNumber);
    }
}
