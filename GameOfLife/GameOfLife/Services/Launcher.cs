namespace GameOfLife
{
    public class Launcher
    {
        /// <summary>
        /// Method to launch one instance of the game.
        /// </summary>
        public void LaunchGame()
        {
            Engine engine = new();
            RulesApplier applier = new();
            Library library = new();
            Render render = new();
            FileIO file = new();
            InputController processor = new();
            FieldOperations field = new(library, render, processor);

            engine.Injection(render, file, field, library, applier, engine, processor);
            engine.StartGame();
            engine.RunGame();
        }
    }
}
