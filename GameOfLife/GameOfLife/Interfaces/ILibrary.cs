using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface ILibrary
    {
        GameFieldModel SpawnGlider(GameFieldModel gameField, int locationX, int locationY);

        GameFieldModel SpawnLightWeight(GameFieldModel gameField, int locationX, int locationY);

        GameFieldModel SpawnMiddleWeight(GameFieldModel gameField, int locationX, int locationY);

        GameFieldModel SpawnHeavyWeight(GameFieldModel gameField, int locationX, int locationY);

        GameFieldModel SpawnGosperGliderGun(GameFieldModel gameField, int locationX, int locationY);

        GameFieldModel SpawnSimkinGliderGun(GameFieldModel gameField, int locationX, int locationY);
    }
}
