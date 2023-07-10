namespace GameOfLife.Entities.Models
{
    public class FileDetails
    {
        public bool FileReadingError { get; set; }
        public bool FileLoaded { get; set; }
        public bool NoSavedGames { get; set; }
        public int NumberOfFiles { get; set; }
        public int FileNumber { get; set; }
    }
}
