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
            Engine engine = new();
            RulesApplier applier = new();
            Library library = new();
            Renderer render = new();
            FileIO file = new();
            InputController processor = new();
            FieldOperations field = new(library, render, processor);
            engine.Injection(render, file, field, library, applier, processor);
            engine.StartGame();
        }
    }
}
