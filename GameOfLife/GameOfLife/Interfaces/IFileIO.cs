namespace GameOfLife.Interfaces
{
    public interface IFileIO
    {
        int Generation { get; set; }

        bool FileReadingError { get; set; }

        void SaveGameFieldToFile(string[,] currentGameState, int aliveCount, int deadCount, int generation);

        string[,] LoadGameFieldFromFile();
    }
}
