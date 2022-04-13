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

        /// <summary>
        /// Method for initial rendering of the game field.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="inputField"></param>
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

        /// <summary>
        /// Method for rendering the game field between the generations.
        /// </summary>
        public static void RuntimeRender()
        {
            field.DrawField(gameField);
            iteration.CheckCells(gameField);
            gameField = iteration.FieldRefresh(gameField);
        }
    }
}
