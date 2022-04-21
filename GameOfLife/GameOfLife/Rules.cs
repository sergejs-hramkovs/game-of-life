using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public static class Rules
    {
        public static void PrintRules()
        {
            Console.Clear();
            Console.WriteLine("\n### Game of Life ###");
            Console.WriteLine("\nThe Game of Life, also known simply as Life, is a cellular automaton devised by the British mathematician John Horton Conway in 1970." +
                "\nIt is a zero - player game, meaning that its evolution is determined by its initial state, requiring no further input. " +
                "\nOne interacts with the Game of Life by creating an initial configuration and observing how it evolves." +
                "\nIt is Turing complete and can simulate a universal constructor or any other Turing machine.");
            Console.WriteLine("\n## Rules ##");
            Console.WriteLine("\nThe universe of the Game of Life is an infinite, two - dimensional orthogonal grid of square cells, " +
                "\neach of which is in one of two possible states, live or dead(or populated and unpopulated, respectively)." +
                "\nEvery cell interacts with its eight neighbours, which are the cells that are horizontally, vertically, or diagonally adjacent. " +
                "\nAt each step in time, the following transitions occur:");
            Console.WriteLine("\n# Any live cell with fewer than two live neighbours dies, as if by underpopulation.");
            Console.WriteLine("# Any live cell with two or three live neighbours lives on to the next generation.");
            Console.WriteLine("# Any live cell with more than three live neighbours dies, as if by overpopulation.");
            Console.WriteLine("# Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.");
            Console.WriteLine("\nThese rules, which compare the behavior of the automaton to real life, can be condensed into the following:");
            Console.WriteLine("\nAny live cell with two or three live neighbours survives.");
            Console.WriteLine("# Any dead cell with three live neighbours becomes a live cell.");
            Console.WriteLine("# All other live cells die in the next generation.Similarly, all other dead cells stay dead.");
            Console.WriteLine("\nThe initial pattern constitutes the seed of the system." +
                "\nThe first generation is created by applying the above rules simultaneously to every cell in the seed, live or dead; " +
                "\nbirths and deaths occur simultaneously, and the discrete moment at which this happens is sometimes called a tick. " +
                "\nEach generation is a pure function of the preceding one.The rules continue to be applied repeatedly to create further generations.");
            Console.WriteLine("\n# Press any key to go back");
            Console.ReadKey();
        }
    }
}
