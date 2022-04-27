using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IEngine
    {
        bool CorrectKeyPressed { get; set; }

        bool WrongInput { get; set; }

        bool Loaded { get; set; }

        bool ReadGeneration { get; set; }

        bool GliderGunMode { get; set; }

        int GliderGunType { get; set; }

        void StartGame(IRender render, IFileIO file, IFieldOperations field, ILibrary library, IRulesApplier rulesApplier,
            IEngine engine, IInputProcessor inputProcessor);

        void RunGame();

        int CountAliveCells();
    }
}
