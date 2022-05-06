using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IEngine
    {
        bool ReadGeneration { get; set; }

        bool GliderGunMode { get; set; }

        bool MultipleGamesMode { get; set; }

        bool MultipleGamesLoaded { get; set; }

        int GliderGunType { get; set; }

        int Delay { get; set; }

        MultipleGamesModel MultipleGames { get; set; }

        void Injection(IRenderer render, IFileIO file, IFieldOperations field, ILibrary library, IRulesApplier rulesApplier, IInputController inputProcessor,
            IUserInterfaceViews userInterfaceViews);

        void StartGame(bool firstLaunch = true);

        void RunGame();

        void RestartGame();

        int CountAliveCells(GameFieldModel gameField);
    }
}
