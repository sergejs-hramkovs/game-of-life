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

        void InitialRender(IField field, string[,] inputField, bool loaded, bool gliderGunMode);

        Tuple<string[,], int, int, int> RuntimeRender(int delay, bool gliderGunMode, bool resetGeneration, bool readGeneration, int generationFromFile);

        void SeedFieldMenuRender();

        void LibraryMenuRender();

        void FieldSizeMenuRender(bool wrongIput);

        void GliderGunMenuRender();

        void PauseRender();

        void ExitRender();
    }
}
