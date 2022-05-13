namespace GameOfLife.Interfaces
{
    public interface IMenuNavigator
    {
        void Injection(IRenderer renderer, IInputController inputController, IMainEngine engine, IFieldOperations fieldOperations);

        void MainMenuNavigator();

        void SeedingTypeMenuNavigator();

        void SingleGameMenuNavigator();

        void MultipleGamesMenuNavigator();

        void LoadGameMenuNavigator();

        void GliderGunModeMenuNavigator();

        void ExitMenuNavigator(ConsoleKey runTimeKeyPress);
    }
}
