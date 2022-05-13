using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface ILibrary
    {
        void SpawnGlider(GameFieldModel gameField, int locationX, int locationY);

        void SpawnLightWeight(GameFieldModel gameField, int locationX, int locationY);

        void SpawnMiddleWeight(GameFieldModel gameField, int locationX, int locationY);

        void SpawnHeavyWeight(GameFieldModel gameField, int locationX, int locationY);

        void SpawnGosperGliderGun(GameFieldModel gameField, int locationX, int locationY);

        void SpawnSimkinGliderGun(GameFieldModel gameField, int locationX, int locationY);
    }
}
