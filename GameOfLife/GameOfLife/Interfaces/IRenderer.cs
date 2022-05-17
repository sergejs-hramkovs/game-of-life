using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IRenderer
    {
        void Inject(IMainEngine engine);

        void RenderMenu(string[] menuLines, bool wrongInput = false, bool noSavedGames = false, 
            bool clearScreen = true, bool multipleGames = false, bool newLine = true, bool gameOver = false);

        void RenderGridOfFields(MultipleGamesModel multipleGames, bool clearScreen = false);

        public void ChangeColorWrite(string textToWrite);
    }
}
