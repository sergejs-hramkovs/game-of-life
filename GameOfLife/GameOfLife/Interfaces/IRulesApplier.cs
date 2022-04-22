using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IRulesApplier
    {
        void CheckCells(string[,] field);

        void CheckCellsDeadBorder(string[,] field);

        string[,] FieldRefresh(string[,] field);
    }
}
