using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IInputProcessor
    {
        void CheckInputMainMenu(ConsoleKeyInfo keyPressed);

        void CheckInputGliderGunMenu(ConsoleKeyInfo keyPressed);

        void EnterFieldDimensions(bool wrongInput);
    }
}
