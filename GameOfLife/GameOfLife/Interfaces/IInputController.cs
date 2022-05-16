using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IInputController
    {
        bool WrongInput { get; set; }

        bool CorrectKeyPressed { get; set; }

        GameFieldModel GameField { get; set; }

        void Injection(IMainEngine engine, IUserInterfaceFiller userInterfaceViews, IFileIO file, IRenderer render, IFieldOperations operations, ILibrary library,
            IMenuNavigator? menuNavigator = null);

        void HandleInputMainMenu();

        void HandleInputSeedingTypeMenu();

        void HandleInputGliderGunMenu();

        void EnterFieldDimensions(bool wrongInput);

        bool EnterCoordinates();

        void EnterGameNumbersToBeDisplayed();

        bool LibraryMenuInputProcessor();

        void CheckInputPauseMenu(ConsoleKey keyPressed, bool multipleGamesMode = false);

        void ChooseMultipleGameNumbersMenuInputProcessor();

        void PauseGame(ConsoleKey keyPressed, bool multipleGamesMode = false);

        int CheckInputSavedGameMenu(int numberOfFiles);

        void CheckInputExitMenu(ConsoleKey keyPressed);

        void ChangeDelay(ConsoleKey keyPressed);

        void EnterMultipleGamesQuantity();

        void HandleInputMultipleGamesMenuFieldSize();

        ConsoleKey RuntimeKeyReader(bool multipleGamesMode = false);

        void HandleInputSingleGameMenu();

        void LoadGameMenuInputProcessor();
    }
}
