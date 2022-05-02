﻿using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IEngine
    {
        bool ReadGeneration { get; set; }

        bool GliderGunMode { get; set; }

        bool MultipleGamesMode { get; set; }

        int GliderGunType { get; set; }

        int Delay { get; set; }

        void Injection(IRender render, IFileIO file, IFieldOperations field, ILibrary library, IRulesApplier rulesApplier,
            IEngine engine, IInputController inputProcessor);

        void StartGame(bool firstLaunch = true);

        void RunGame(int indentationSize = 1);

        void RestartGame();

        int CountAliveCells(GameFieldModel gameField);
    }
}
