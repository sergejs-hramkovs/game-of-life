using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IRender
    {
        void RenderField(string[,] field, bool dead = false);

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
    }
}
