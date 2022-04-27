using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IField
    {
        string[,] FieldArray { get; set; }

        int CoordinateX { get; set; }

        int CoordinateY { get; set; }

        bool Stop { get; set; }

        string[,] CreateField(ILibrary library, IEngine engine, IRulesApplier rulesApplier, IRender render, IInputProcessor processor, int fieldLength, int fieldWidth);

        string[,] PopulateField(bool gliderGunMode, int gliderGunType);

        string[,] ManualSeeding();

        string[,] RandomSeeding(int fieldLength, int fieldWidth);

        string[,] LibrarySeeding(bool gliderGunMode, int gliderGunType);

        void CallSpawningMethod(Func<string[,], int, int, string[,]> SpawnLibraryObject);
    }
}
