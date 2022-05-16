using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IMenuNavigator
    {
        void Inject(IRenderer renderer, IInputController inputController, IMainEngine engine, IFieldOperations fieldOperations);

        public void NavigateMenu(string[] menu, Action HandleInput, bool clearScr = true, Action<MultipleGamesModel, bool>? Render = null);

        void NavigateMultipleGamesMenu();

        void NavigateExitMenu(bool gameIsOver);
    }
}
