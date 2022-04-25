using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IRulesApplier
    {
        void DetermineCellsDestiny(string[,] field, bool disableWrappingAroundField);

        string[,] FieldRefresh(string[,] field);
    }
}
