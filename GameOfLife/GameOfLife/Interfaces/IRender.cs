using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IRender
    {
        void RenderField(GameFieldModel gameField, bool dead = false);

        void RuntimeUIRender(GameFieldModel gameField, int delay);

        void SeedFieldMenuRender();

        void LibraryMenuRender(bool wrongInput);

        void MainMenuRender(bool wrongIput, bool fileReadingError);

        void GliderGunModeRender(bool wrongInput);

        void PauseMenuRender();

        void ExitMenuRender();

        void PrintRules();

        void GameOverRender(int generation);

        void BlankUIRender();

        void ChooseFileToLoadMenuRender(int numberOfFiles, string filePath, bool wrongInput);
    }
}
