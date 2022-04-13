using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public static class Render
    {
        static string[,] gameField;
        static Field field;
        static Iteration iteration;

        public static void InitialRender(int height, int width, string[,] inputField)
        {
            gameField = inputField;
            field = new Field(height, width);
            iteration = new Iteration();
            gameField = field.CreateField();
            field.DrawField(gameField);
            gameField = field.SeedField();
            Console.Clear();
            Console.CursorVisible = false;
        }
        public static void RuntimeRender()
        {
            field.DrawField(gameField);
            iteration.CheckCells(gameField);
            gameField = iteration.FieldRefresh(gameField);
        }
    }
}
