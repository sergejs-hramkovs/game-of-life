using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IInputController
    {
        bool WrongInput { get; set; }

        bool CorrectKeyPressed { get; set; }

        GameFieldModel GameField { get; set; }

        void Inject(IMainEngine engine, IUserInterfaceFiller userInterfaceViews, IFileIO file, IRenderer render,
            IFieldOperations operations, ILibrary library, IMenuNavigator? menuNavigator = null);

        void HandleInputMainMenu();

        void HandleInputSeedingTypeMenu();

        void HandleInputGliderGunMenu();

        void EnterFieldDimensions(bool wrongInput);

        bool EnterCoordinates();

        void EnterGameNumbersToBeDisplayed();

        bool HandleInputLibraryMenu();

        void HandleInputPauseMenu(ConsoleKey keyPressed, bool multipleGamesMode = false);

        void HandleInputMultipleGameNumbersMenu();

        void PauseGame(ConsoleKey keyPressed, bool multipleGamesMode = false);

        int HandleInputSavedGameMenu(int numberOfFiles);

        void HandleInputExitMenu(ConsoleKey keyPressed);

        void ChangeDelay(ConsoleKey keyPressed);

        void EnterMultipleGamesQuantity();

        void HandleInputMultipleGamesMenuFieldSize();

        ConsoleKey ReadKeyRuntime(bool multipleGamesMode = false);

        void HandleInputSingleGameMenu();

        void HandleInputLoadGameMenu();
    }
}
