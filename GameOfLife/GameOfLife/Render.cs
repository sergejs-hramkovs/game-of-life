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
        static int aliveCellCount;
        static int deadCellCount;
        static int generation = 1;
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
        public static void RuntimeRender(int delay)
        {
            Console.WriteLine("Press ESC to stop");
            Console.WriteLine("Press Spacebar to pause");
            Console.WriteLine("Change the delay using left and right arrows");
            Console.WriteLine($"\nGeneration: {generation}");
            Console.WriteLine($"Alive cells: {CountAlive()}   ");
            Console.WriteLine($"Dead cells: {CountDead()}   ");
            Console.WriteLine($"Current delay between generations: {delay / 1000.0} seconds  ");
            
            field.DrawField(gameField);
            iteration.CheckCells(gameField);
            gameField = iteration.FieldRefresh(gameField);
            generation++;
        }

        /// <summary>
        /// Method to count the current number of alive cells on the field.
        /// </summary>
        /// <returns></returns>
        public static int CountAlive()
        {
            aliveCellCount = 0;

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(0); j++)
                {
                    if (gameField[i, j] == "X")
                    {
                        aliveCellCount++;
                    }                    
                }
            }
            return aliveCellCount;
        }

        /// <summary>
        /// Method to count the current number of dead cells on the field.
        /// </summary>
        /// <returns></returns>
        public static int CountDead()
        {
            deadCellCount = 0;

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(0); j++)
                {
                    if (gameField[i, j] == ".")
                    {
                        deadCellCount++;
                    }
                }
            }
            return deadCellCount;
        }
    }
}
