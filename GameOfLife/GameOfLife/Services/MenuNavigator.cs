using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Views;

namespace GameOfLife.Services
{
    /// <summary>
    /// The MenuNavigator class deals with transitions between UI menus.
    /// </summary>
    public class MenuNavigator : IMenuNavigator
    {
        private IRenderer _renderer;
        private IInputController _inputController;
        private IMainEngine _engine;
        private IFieldOperations _fieldOperations;

        /// <summary>
        /// Method to inject the required objects into the MenuNavigator class.
        /// </summary>
        /// <param name="renderer">An object of the Renderer class.</param>
        /// <param name="inputController">An object of the InputController class.</param>
        /// <param name="engine">An object of the Engine class.</param>
        /// <param name="fieldOperations">An object of the FieldOperations class.</param>
        public void Inject(IRenderer renderer, IInputController inputController, IMainEngine engine, IFieldOperations fieldOperations)
        {
            _renderer = renderer;
            _inputController = inputController;
            _engine = engine;
            _fieldOperations = fieldOperations;
        }

        /// <summary>
        /// General method to navigate through the menus.
        /// </summary>
        /// <param name="menu">An instance of a menu to be displayed.</param>
        /// <param name="HandleInput">Method to handle the user's input in the menu.</param>
        public void NavigateMenu(string[] menu, Action HandleInput, bool clearScr = true, Action<MultipleGamesModel, bool>? Render = null)
        {
            do
            {
                Render?.Invoke(_engine.MultipleGames, !clearScr);
                _renderer.MenuRenderer(menu, wrongInput: _inputController.WrongInput, clearScreen: clearScr);
                HandleInput();
            } while (_inputController.WrongInput);
        }

        /// <summary>
        /// Method to navigate through all the Multiple Games Mode menus.
        /// </summary>
        public void NavigateMultipleGamesMenu()
        {
            NavigateMultipleGamesQuantityMenu();
            NavigateMenu(MenuViews.MultipleGamesModeFieldSizeChoiceMenu, _inputController.HandleInputMultipleGamesMenuFieldSize);
            _engine.MultipleGames.InitializeGames(_fieldOperations);
            NavigateMenu(MenuViews.MultipleGamesModeMenu, _inputController.ChooseMultipleGameNumbersMenuInputProcessor);
        }

        /// <summary>
        /// Method to navigate through the 'Enter the quantity of games' Menu.
        /// </summary>
        private void NavigateMultipleGamesQuantityMenu()
        {
            _renderer.MenuRenderer(MenuViews.MultipleGamesModeGamesQuantityMenu, newLine: false);
            _inputController.EnterMultipleGamesQuantity();
        }

        /// <summary>
        /// Method to navigate through the Exit Menu.
        /// </summary>
        /// <param name="runTimeKeyPress">Parameter that stores the user's input in the Exit Menu.</param>
        public void NavigateExitMenu(ConsoleKey runTimeKeyPress)
        {
            _renderer.MenuRenderer(MenuViews.ExitMenu, clearScreen: false);
            do
            {
                runTimeKeyPress = Console.ReadKey(true).Key;
                _inputController.CheckInputExitMenu(runTimeKeyPress);
            } while (runTimeKeyPress != ConsoleKey.Escape || runTimeKeyPress != ConsoleKey.R);
        }
    }
}
