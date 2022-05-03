using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IEngine
    {
        bool ReadGeneration { get; set; }

        bool GliderGunMode { get; set; }

        bool MultipleGamesMode { get; set; }

        int GliderGunType { get; set; }

        int Delay { get; set; }

        int NumberOfGamesToBeCreated { get; set; }

        int NumberOfGamestoBeDisplayed { get; set; }

        int Length { get; set; }

        int Width { get; set; }

        List<int> GamesToBeDisplayed { get; set; }

        List<GameFieldModel> ListOfGames { get; set; }

        void Injection(IRender render, IFileIO file, IFieldOperations field, ILibrary library, IRulesApplier rulesApplier,
            IEngine engine, IInputController inputProcessor);

        void StartGame(bool firstLaunch = true);

        void RunGame();

        void RestartGame();

        int CountAliveCells(GameFieldModel gameField);
    }
}
