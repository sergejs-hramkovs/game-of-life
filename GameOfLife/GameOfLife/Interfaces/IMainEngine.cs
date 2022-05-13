using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IMainEngine
    {
        bool ReadGeneration { get; set; }

        bool GliderGunMode { get; set; }

        bool MultipleGamesMode { get; set; }

        bool SavedGameLoaded { get; set; }

        bool InitializationFinished { get; set; }

        int GliderGunType { get; set; }

        int Delay { get; set; }

        MultipleGamesModel MultipleGames { get; set; }

        void Injection(IRenderer render, IFileIO file, IFieldOperations field, ILibrary library, IRulesApplier rulesApplier, IInputController inputProcessor,
            IUserInterfaceFiller userInterfaceViews, IAuxiliaryEngine auxiliaryEngine, IMenuNavigator menuNavigator);

        void StartGame(bool firstLaunch = true);

        void RunGame();

        void RestartGame();
    }
}
