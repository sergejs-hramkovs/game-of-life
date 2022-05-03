using static GameOfLife.StringConstantsModel;

namespace GameOfLife.Models
{
    /// <summary>
    /// The GameFieldModel class represents the Game Field. It stores the field itself, with dead and alive cells, the number of dead and alive cells
    /// field dimensions and the number of generation.
    /// </summary>
    public class GameFieldModel
    {
        private string[,] _gameField;
        private int _length;
        private int _width;
        private int _area;
        private int _generation;
        private int _aliveCellsNumber;
        public string[,] GameField
        {
            get => _gameField;
            set => _gameField = value;
        }
        public int Length
        {
            get => _length;
        }
        public int Width
        {
            get => _width;
        }
        public int Area
        {
            get => _area;
        }
        public int Generation
        {
            get => _generation;
            set => _generation = value;
        }
        public int AliveCellsNumber
        {
            get => _aliveCellsNumber;
            set => _aliveCellsNumber = value;
        }
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
            _gameField = new string[length, width];
            _length = _gameField.GetLength(0);
            _width = _gameField.GetLength(1);
            _area = Length * Width;
            _generation = 1;

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
