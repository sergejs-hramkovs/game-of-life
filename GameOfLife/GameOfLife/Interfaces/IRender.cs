using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IRender
    {
        void RenderField(string[,] field);

        void RuntimeUIRender(int aliveCells, int deadCells, int generation, int delay);

        void SeedFieldMenuRender();

        void LibraryMenuRender();

        void FieldSizeMenuRender(bool wrongIput, bool fileReadingError);

        void GliderGunMenuRender();

        void PauseMenuRender();

        void ExitMenuRender();

        void PrintRules();
    }
}
