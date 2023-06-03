using GameOfLife.Services;
using GameOfLife.Views;

namespace GameOfLife
{
    /// <summary>
    /// The Launcher class deals with all the required class instantiations and calls the StartGame method.
    /// </summary>
    [Serializable]
    public class Launcher
    {
        /// <summary>
        /// Method to start the game.
        /// </summary>
        public void LaunchGame()
        {
            MainEngine mainEngine = new MainEngine();
            AuxiliaryEngine auxiliaryEngine = new AuxiliaryEngine();
            RulesApplier applier = new RulesApplier();
            Library library = new Library();
            Renderer renderer = new Renderer();
            FileIO file = new FileIO();
            InputController inputController = new InputController();
            FieldOperations fieldOperations = new FieldOperations(renderer, inputController);
            UserInterfaceFiller userInterfaceFiller = new UserInterfaceFiller();
            MenuNavigator menuNavigator = new MenuNavigator();
            mainEngine.Inject(renderer, file, fieldOperations, library, applier, inputController, userInterfaceFiller, auxiliaryEngine, menuNavigator);
            mainEngine.StartGame();
        }
    }
}
