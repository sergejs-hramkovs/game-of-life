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
        private int _fieldWidth { get; set; }

        private string[,] fieldArray;

        public Field(int height, int width)
        {
            _fieldHeight = height;
            _fieldWidth = width;
        }

        /// <summary>
        /// Initial creation of an empty gaming field.
        /// </summary>
        /// <returns></returns>
        public string[,] CreateField()
        {
            fieldArray = new string[_fieldWidth, _fieldHeight];

            for (int j = 0; j < _fieldHeight; j++)
            {
                for (int i = 0; i < _fieldWidth; i++)
                {
                    // Creating padding for now to avoid wrapping issues.
                    if (j == 0 || i == 0 || j == _fieldHeight - 1 || i == _fieldWidth - 1)
                    {
                        fieldArray[i, j] = "#";
                    }
                    // ----------

                    else
                    {
                        fieldArray[i, j] = "-";
                    }
                }
            }

            return fieldArray;
        }

        /// <summary>
        /// Function that draws the field.
        /// </summary>
        /// <param name="field"></param>
        public void DrawField(string [,] field)
        {
            Console.WriteLine();

            for (int j = 0; j < field.GetLength(0); j++)
            {
                for (int i = 0; i < field.GetLength(1); i++)
                {
                    Console.Write(" " + field[i, j]);
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Seeding the field with user input cells.
        /// </summary>
        /// <returns></returns>
        public string[,] SeedField()
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
                    return fieldArray;
                }

                cellX = Convert.ToInt32(input);

                Console.Write("Enter Y coordinate of the cell: ");
                input = Console.ReadLine();

                if (input == "stop")
                {
                    Console.WriteLine();
                    Console.WriteLine("The seeding has been stopped!");
                    return fieldArray;
                }

                cellY = Convert.ToInt32(input);      

                fieldArray[cellX, cellY] = "X";
                DrawField(fieldArray);
            }
            
            return fieldArray;
        }
    }
}