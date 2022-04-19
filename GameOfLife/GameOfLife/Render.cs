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
        static int generation = 1;
        static Field field;
        static Iteration iteration;
        static Engine engine;

        /// <summary>
        /// Method for initial rendering of the game field.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="inputField"></param>
        public static void InitialRender(int length, int width, string[,] inputField)
        {
            gameField = inputField;
            field = new Field(length, width);
            iteration = new Iteration();
            engine = new Engine();
            gameField = field.CreateField();
            Console.Clear();
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
            Console.WriteLine($"Alive cells: " +
                $"{engine.CountAlive(gameField)}({(int)Math.Round((engine.CountAlive(gameField) / (double)engine.CountDead(gameField)) * 100)}%)   ");
            Console.WriteLine($"Dead cells: {engine.CountDead(gameField)}   ");
            Console.WriteLine($"Current delay between generations: {delay / 1000.0} seconds  ");
            Console.WriteLine($"Number of generations per second: {Math.Round(1/ (delay / 1000.0), 2)}   ");
            
            field.DrawField(gameField);
            iteration.CheckCells(gameField);
            gameField = iteration.FieldRefresh(gameField);
            generation++;
        }
    }
}
