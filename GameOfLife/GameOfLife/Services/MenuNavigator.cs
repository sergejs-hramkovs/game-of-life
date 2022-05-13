using GameOfLife.Interfaces;
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

        public void Injection(IRenderer renderer, IInputController inputController, IMainEngine engine, IFieldOperations fieldOperations)
        {
            _renderer = renderer;
            _inputController = inputController;
            _engine = engine;
            _fieldOperations = fieldOperations;
        }

        public void MainMenuNavigator()
        {
            do
            {
                _renderer.MenuRenderer(MenuViews.MainMenuNew, wrongInput: _inputController.WrongInput);
                _inputController.MainMenuInputProcessor();
            } while (_inputController.WrongInput);
            Console.Clear();
        }

        public void SingleGameMenuNavigator()
        {
            do
            {
                _renderer.MenuRenderer(MenuViews.SingleGameMenu, wrongInput: _inputController.WrongInput);
                _inputController.SingleGameMenuInputProcessor();
            } while (_inputController.WrongInput);

            _engine.MultipleGames.InitializeSingleGameParameters();
            SeedingTypeMenuNavigator();
        }

        public void SeedingTypeMenuNavigator()
        {
            do
            {
                if (!_engine.MultipleGamesMode && !_engine.SavedGameLoaded && !_engine.GliderGunMode)
                {
                    _renderer.GridOfFieldsRenderer(_engine.MultipleGames, clearScreen: true);
                    _renderer.MenuRenderer(MenuViews.SeedingTypeMenu, clearScreen: false, wrongInput: _inputController.WrongInput);
                    _inputController.SeedingTypeMenuInputProcessor();
                }
            } while (_inputController.WrongInput);
        }

        public void MultipleGamesMenuNavigator()
        {
            MultipleGamesQuantityMenuNavigator();
            MultipleGamesFieldSizeMenuNavigator();
            _engine.MultipleGames.InitializeGames(_fieldOperations);
            MultipleGamesNumbersMenuNavigator();
        }

        private void MultipleGamesQuantityMenuNavigator()
        {
            _renderer.MenuRenderer(MenuViews.MultipleGamesModeGamesQuantityMenu, newLine: false);
            _inputController.EnterMultipleGamesQuantity();
        }

        public void GliderGunModeMenuNavigator()
        {
            do
            {
                _renderer.MenuRenderer(MenuViews.GliderGunModeMenu, wrongInput: _inputController.WrongInput);
                _inputController.GliderGunMenuInputProcessor();
            } while (_inputController.WrongInput);
        }

        private void MultipleGamesFieldSizeMenuNavigator()
        {
            do
            {
                _renderer.MenuRenderer(MenuViews.MultipleGamesModeFieldSizeChoiceMenu, wrongInput: _inputController.WrongInput);
                _inputController.CheckInputMultipleGamesMenuFieldSize();
            } while (_inputController.WrongInput);
        }

        private void MultipleGamesNumbersMenuNavigator()
        {
            do
            {
                _renderer.MenuRenderer(MenuViews.MultipleGamesModeMenu, wrongInput: _inputController.WrongInput);
                _inputController.ChooseMultipleGameNumbersMenuInputProcessor();
            } while (_inputController.WrongInput);
        }

        public void ExitMenuNavigator(ConsoleKey runTimeKeyPress)
        {
            _renderer.MenuRenderer(MenuViews.ExitMenu, clearScreen: false);
            do
            {
                runTimeKeyPress = Console.ReadKey(true).Key;
                _inputController.CheckInputExitMenu(runTimeKeyPress);
            } while (runTimeKeyPress != ConsoleKey.Escape || runTimeKeyPress != ConsoleKey.R);
        }

        public void LoadGameMenuNavigator()
        {
            do
            {
                _renderer.MenuRenderer(MenuViews.LoadGameMenu, clearScreen: true, wrongInput: _inputController.WrongInput);
                _inputController.LoadGameMenuInputProcessor();
            } while (_inputController.WrongInput);
        }
    }
}
