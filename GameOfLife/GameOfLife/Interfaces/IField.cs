using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IField
    {
        string[,] CreateField(ILibrary library, IEngine engine, IRulesApplier rulesApplier, IRender render, int fieldLength, int fieldWidth);

        string[,] PopulateField(bool gliderGunMode);
    }
}
