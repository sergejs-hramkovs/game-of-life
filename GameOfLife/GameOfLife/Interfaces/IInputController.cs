using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IInputController
    {
        bool WrongInput { get; set; }

        bool CorrectKeyPressed { get; set; }

        GameFieldModel GameField { get; set; }

        void Injection(IEngine engine, IFileIO file, IRender render, IFieldOperations operations, ILibrary library);

        GameFieldModel CheckInputMainMenu(ConsoleKeyInfo keyPressed);

        GameFieldModel CheckInputGliderGunMenu(ConsoleKeyInfo keyPressed);

        GameFieldModel EnterFieldDimensions(bool wrongInput);

        bool EnterCoordinates();

        bool CheckInputPopulateFieldMenu(ConsoleKeyInfo keyPressed);

        bool CheckInputLibraryMenu(ConsoleKeyInfo keyPressed);

        int CheckInputSavedGameMenu(int numberOfFiles);

        void ChangeDelay(ConsoleKeyInfo keyPressed);
    }
}
