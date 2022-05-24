using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// The Library class deals with populating the Game Field with premade cell patterns.
    /// </summary>
    public interface ILibrary
    {
        /// <summary>
        /// Method to spawn a Glider pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a glider.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a glider.</param>
        void SpawnGlider(GameFieldModel gameField, int locationX, int locationY);

        /// <summary>
        /// Method to spawn a Light-Weight Spaceship pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a light-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a light-weight spaceship.</param>
        void SpawnLightWeight(GameFieldModel gameField, int locationX, int locationY);

        /// <summary>
        /// Method to spawn a Middle-Weight Spaceship pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a middle-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a middle-weight spaceship.</param>
        void SpawnMiddleWeight(GameFieldModel gameField, int locationX, int locationY);

        /// <summary>
        /// Method to spawn a Heavy-Weight Spaceship pattern.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of a heavy-weight spaceship.</param>
        /// <param name="locationY">Vertical location of the upper left corner of a heavy-weight spaceship.</param>
        void SpawnHeavyWeight(GameFieldModel gameField, int locationX, int locationY);

        /// <summary>
        /// Method to spawn Gosper's Glider Gun.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of the Gosper's glider gun.</param>
        /// <param name="locationY">Vertical location of the upper left corner of Gosper's glider gun.</param>
        void SpawnGosperGliderGun(GameFieldModel gameField, int locationX, int locationY);

        /// <summary>
        /// Method to spawn Simkin's Glider Gun.
        /// </summary>
        /// <param name="gameField">An instance of the GameFieldModel class that stores the game field and its properties.</param>
        /// <param name="locationX">Horizontal location of the upper left corner of the Simkin's glider gun.</param>
        /// <param name="locationY">Vertical location of the upper left corner of the Simkin's glider gun.</param>
        void SpawnSimkinGliderGun(GameFieldModel gameField, int locationX, int locationY);
    }
}
