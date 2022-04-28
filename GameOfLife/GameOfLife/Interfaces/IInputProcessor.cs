using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IInputProcessor
    {
        bool WrongInput { get; set; }

        bool CorrectKeyPressed { get; set; }

        void Injection(IEngine engine, IFileIO file, IRender render, IFieldOperations operations, ILibrary library);

        GameFieldModel CheckInputMainMenu(ConsoleKeyInfo keyPressed);

        GameFieldModel CheckInputGliderGunMenu(ConsoleKeyInfo keyPressed);

        GameFieldModel EnterFieldDimensions(bool wrongInput);

        bool EnterCoordinates();

        bool CheckInputPopulateFieldMenu(ConsoleKeyInfo keyPressed);

        bool CheckInputLibraryMenu(ConsoleKeyInfo keyPressed);
    }
}
