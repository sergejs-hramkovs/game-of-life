using GameOfLife.Models;

namespace GameOfLife.Entities.Models
{
    public class GameModel
    {
        public SingleGameField? SingleGame { get; set; }

        public MultipleGamesField MultipleGamesField { get; set; }

        public GameDetails GameDetails { get; set; }

        public GameModel()
        {
            MultipleGamesField = new MultipleGamesField();
            GameDetails = new GameDetails();
        }
    }
}
