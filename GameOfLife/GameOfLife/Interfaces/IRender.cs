using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IRender
    {
        void RenderField(GameFieldModel gameField, bool dead = false);

        void RuntimeUIRender(int aliveCells, int deadCells, int generation, int delay);

        void SeedFieldMenuRender();

        void LibraryMenuRender();

        void MainMenuRender(bool wrongIput, bool fileReadingError);

        void GliderGunModeRender();

        void PauseMenuRender();

        void ExitMenuRender();

        void PrintRules();

        void GameOverRender(int generation);

        void BlankUIRender();

        void ChooseFileToLoadMenuRender(int numberOfFiles, string filePath, bool wrongInput);
    }
}
