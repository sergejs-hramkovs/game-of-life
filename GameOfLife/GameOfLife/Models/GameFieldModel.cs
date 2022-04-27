using static GameOfLife.StringConstants;

namespace GameOfLife.Models
{
    public class GameFieldModel
    {

        private string[,] _gameField;
        private int _length;
        private int _width;
        private int _area;
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

        public GameFieldModel(int length, int width)
        {
            _gameField = new string[length, width];
            _length = _gameField.GetLength(0);
            _width = _gameField.GetLength(1);
            _area = Length * Width;

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
