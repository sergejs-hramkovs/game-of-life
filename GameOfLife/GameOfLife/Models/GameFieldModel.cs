using static GameOfLife.StringConstantsModel;

namespace GameOfLife.Models
{
    /// <summary>
    /// The GameFieldModel class represents the Game Field. It stores the field itself, with dead and alive cells, the number of dead and alive cells
    /// field dimensions and the number of generation.
    /// </summary>
    public class GameFieldModel
    {
        public string[,] GameField { get; set; }
        public int Length { get; }
        public int Width { get; }
        public int Area { get; }
        public int Generation { get; set; }
        public int AliveCellsNumber { get; set; }
        public int DeadCellsNumber
        {
            get => Area - AliveCellsNumber;
        }

        /// <summary>
        /// The GameFieldModel constructor is used to initialize an instance of the class, while also initializing field's dimensions, generation
        /// and seeding the field with dead cells.
        /// </summary>
        /// <param name="length">The horizontal dimension of the field.</param>
        /// <param name="width">THe vertical dimension of the field.</param>
        public GameFieldModel(int length, int width)
        {
            GameField = new string[length, width];
            Length = GameField.GetLength(0);
            Width = GameField.GetLength(1);
            Area = Length * Width;
            Generation = 1;

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    GameField[i, j] = DeadCellSymbol;
                }
            }
        }
    }
}
