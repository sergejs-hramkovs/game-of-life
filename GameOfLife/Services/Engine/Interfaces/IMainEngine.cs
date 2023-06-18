namespace GameOfLife.Interfaces
{
    /// <summary>
    /// The Engine class starts, runs and restarts the game.
    /// </summary>
    public interface IMainEngine
    {
        void StartGame(bool firstLaunch = true);

        void RunGame();

        //void RestartGame();
    }
}
