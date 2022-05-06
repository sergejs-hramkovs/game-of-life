﻿using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IFileIO
    {
        bool FileReadingError { get; set; }

        bool FileLoaded { get; set; }

        bool NoSavedGames { get; set; }

        string FilePath { get; }

        string MultipleGamesModeFilePath { get; }

        int NumberOfFiles { get; }

        void Injection(IRenderer render, IInputController inputController, IEngine engine);

        void SaveGameFieldToFile(GameFieldModel gameField);

        GameFieldModel LoadGameFieldFromFile(int fileNumber);

        void CountFiles();

        void InitiateLoadingFromFile();

        void Serializer(MultipleGamesModel multipleGames);

       MultipleGamesModel Deserializer();
    }
}
