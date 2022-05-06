using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IInputController
    {
        bool WrongInput { get; set; }

        bool CorrectKeyPressed { get; set; }

        GameFieldModel GameField { get; set; }

        MultipleGamesModel MultipleGames { get; set; }

        void Injection(IEngine engine, IFileIO file, IRenderer render, IFieldOperations operations, ILibrary library);

        GameFieldModel CheckInputMainMenu(ConsoleKey keyPressed);

        GameFieldModel CheckInputGliderGunMenu(ConsoleKey keyPressed);

        GameFieldModel EnterFieldDimensions(bool wrongInput);

        bool EnterCoordinates();

        void EnterGameNumber();

        bool CheckInputPopulateFieldMenu(ConsoleKey keyPressed);

        bool CheckInputLibraryMenu(ConsoleKey keyPressed);

        void CheckInputPauseMenu(ConsoleKey keyPressed, bool multipleGamesMode = false);

        bool CheckInputMultipleGamesMenu(ConsoleKey keyPressed);

        void PauseGame(ConsoleKey keyPressed, bool multipleGamesMode = false);

        int CheckInputSavedGameMenu(int numberOfFiles);

        void CheckInputExitMenu(ConsoleKey keyPressed);

        void ChangeDelay(ConsoleKey keyPressed);

        MultipleGamesModel EnterNumberOfMultipleGames(MultipleGamesModel multipleGames);

        MultipleGamesModel CheckInputMultipleGamesMenuFieldSize(MultipleGamesModel multipleGames, ConsoleKey keyPressed);
    }
}
