using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IRender
    {
        void Injection(IFileIO file);

        void RenderField(GameFieldModel gameField, bool dead = false);

        void RuntimeUIRender(GameFieldModel gameField, int delay);

        void SeedFieldMenuRender();

        void LibraryMenuRender(bool wrongInput);

        void MainMenuRender(bool wrongIput, bool fileReadingError, bool noSavedGames);

        void GliderGunModeRender(bool wrongInput);

        void PauseMenuRender(bool multipleGamesMode = false);

        void ExitMenuRender();

        void MultipleGamesMenuRender();

        void PrintRules();

        void GameOverRender(int generation);

        void BlankUIRender();

        void MultipleGamesModeUIRender(int delay, int generation, int numberOfFieldsAlive, int totalCellsAlive);

        void ChooseFileToLoadMenuRender(int numberOfFiles, string filePath, bool wrongInput);

        void MultipleGamesModeGameTitleRender(int gameNumber, int cellsAliveNumber);
    }
}
