using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Field
    {
        private int _fieldHeight { get; set; }
        public int _fieldWidth { get; set; }

        string[,] fieldArray;

        public Field(int height, int width)
        {
            _fieldHeight = height;
            _fieldWidth = width;
        }

        public void CreateField()
        {
            fieldArray = new string[_fieldWidth, _fieldHeight];

            for (int j = 0; j < _fieldHeight; j++)
            {
                for (int i = 0; i < _fieldWidth; i++)
                {
                    fieldArray[i, j] = "-";
                }
            }
        }

        public void DrawField()
        {
            Console.WriteLine();

            for (int j = 0; j < _fieldHeight; j++)
            {
                for (int i = 0; i < _fieldWidth; i++)
                {
                    Console.Write(" " + fieldArray[i, j]);
                }

                Console.WriteLine();
            }
        }

        public void SeedField()
        {
            int cellX;
            int cellY;
            string input;
            
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("To stop seeding enter 'stop'");
                Console.Write("Enter X coordinate of the cell: ");
                input = Console.ReadLine();

                if (input == "stop")
                {
                    Console.WriteLine();
                    Console.WriteLine("The seeding has been stopped!");
                    break;
                }

                cellX = Convert.ToInt32(input);

                Console.Write("Enter Y coordinate of the cell: ");
                input = Console.ReadLine();

                if (input == "stop")
                {
                    Console.WriteLine();
                    Console.WriteLine("The seeding has been stopped!");
                    break;
                }

                cellY = Convert.ToInt32(input);      

                fieldArray[cellX - 1, cellY - 1] = "X";
                DrawField();
            }            
        }
    }
}