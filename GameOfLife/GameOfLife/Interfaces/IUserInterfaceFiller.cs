﻿using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IUserInterfaceFiller
    {
        void SingleGameRuntimeUICreator(MultipleGamesModel multipleGames, int delay);

        void MultiGameRuntimeUICreator(MultipleGamesModel multipleGames, int delay);

        void GameOverUICreation(int generation);

        void ChooseFileMenuCreation(int numberOfFiles, List<string> fileNames);
    }
}
