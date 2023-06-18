using GameOfLife.Entities.Models;
using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Views;

namespace GameOfLife.Services
{
    /// <summary>
    /// The AuxiliaryEngine class helps to relieve the MainEngine class from some methods that can be considered to be auxiliary.
    /// </summary>
    public class AuxiliaryEngine : IAuxiliaryEngine
    {
        private readonly IMainEngine _engine;
        private readonly IGameFieldService _gameFieldService;
        private readonly IRenderingService _renderer;
        private readonly IUIService _userInterfaceService;

        public AuxiliaryEngine(
            IMainEngine engine,
            IGameFieldService gameFieldService,
            IRenderingService renderer,
            IUIService userInterfaceService)
        {
            _engine = engine;
            _gameFieldService = gameFieldService;
            _renderer = renderer;
            _userInterfaceService = userInterfaceService;
        }

        /// <summary>
        /// Method to count the number of alive cells on one field.
        /// </summary>
        /// <param name="gameField">A GameFieldModel object that contains the Game Field.</param>
        public void CountAliveCells(SingleGameField gameField)
        {
            gameField.AliveCellsNumber = 0;
            for (int xCoordinate = 0; xCoordinate < gameField.Length; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < gameField.Width; yCoordinate++)
                {
                    if (gameField.GameField[xCoordinate, yCoordinate] == StringConstants.AliveCellSymbol)
                    {
                        gameField.AliveCellsNumber++;
                    }
                }
            }
        }

        /// <summary>
        /// Method to count total alive cells number on all the fields in the Multiple Games Mode.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        public void CountTotalAliveCells(MultipleGamesField multipleGames)
        {
            multipleGames.TotalCellsAlive = 0;
            foreach (var field in multipleGames.ListOfGames)
            {
                CountAliveCells(field);
                multipleGames.TotalCellsAlive += field.AliveCellsNumber;
            }
        }

        /// <summary>
        /// Method to replace rendered dead fields with alive ones in the list of fields to be displayed.
        /// </summary>
        /// <param name="multipleGames">A MultipleGamesModel object that contains the list of Game Fields.</param>
        public void RemoveDeadFieldsFromRendering(MultipleGamesField multipleGames)
        {
            for (int rowNumber = 0; rowNumber < multipleGames.NumberOfRows; rowNumber++)
            {
                for (int i = rowNumber * multipleGames.NumberOfHorizontalFields; i < multipleGames.NumberOfHorizontalFields + rowNumber * multipleGames.NumberOfHorizontalFields; i++)
                {
                    if ((multipleGames.ListOfGames[multipleGames.GamesToBeDisplayed[i]].AliveCellsNumber == 0) && (multipleGames.NumberOfFieldsAlive >= multipleGames.NumberOfGamesToBeDisplayed))
                    {
                        foreach (int aliveFieldNumber in _engine.MultipleGames.AliveFields)
                        {
                            if (!multipleGames.GamesToBeDisplayed.Contains(aliveFieldNumber))
                            {
                                multipleGames.GamesToBeDisplayed[i] = aliveFieldNumber;
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to perform the required runtime actions, like applying game rules and counting alive cells of each field.
        /// </summary>
        public void PerformRuntimeCalculations()
        {
            for (int gameNumber = 0; gameNumber < _engine.MultipleGames.TotalNumberOfGames; gameNumber++)
            {
                _gameFieldService.IterateThroughGameFieldCells(_engine.MultipleGames.ListOfGames[gameNumber], _engine.GliderGunMode);
                _gameFieldService.RefreshField(_engine.MultipleGames.ListOfGames[gameNumber]);
                CountAliveCells(_engine.MultipleGames.ListOfGames[gameNumber]);

                if (_engine.MultipleGames.ListOfGames[gameNumber].AliveCellsNumber == 0 && !_engine.MultipleGames.DeadFields.Contains(gameNumber))
                {
                    _engine.MultipleGames.NumberOfFieldsAlive--;
                    _engine.MultipleGames.DeadFields.Add(gameNumber);
                    if (_engine.MultipleGames.AliveFields.Contains(gameNumber))
                    {
                        _engine.MultipleGames.AliveFields.Remove(gameNumber);
                    }
                }
                else if (_engine.MultipleGames.ListOfGames[gameNumber].AliveCellsNumber > 0 && !_engine.MultipleGames.AliveFields.Contains(gameNumber))
                {
                    _engine.MultipleGames.AliveFields.Add(gameNumber);
                }
            }

            _engine.MultipleGames.Generation++;
        }

        /// <summary>
        /// Method that is responsible for creation and displaying of the runtime UI and the Game Field(s).
        /// </summary>
        public void CreateRuntimeView(GameModel game)
        {
            Console.SetCursorPosition(0, 0);
            CountTotalAliveCells(game.MultipleGamesField);
            if (!game.GameDetails.IsMultipleGamesMode)
            {
                _userInterfaceService.CreateSingleGameRuntimeUI(game.MultipleGamesField, game.GameDetails.Delay);
                _renderer.RenderMenu(MenuViews.SingleGameUI, clearScreen: false);
            }
            else
            {
                _userInterfaceService.CreateMultiGameRuntimeUI(game.MultipleGamesField, game.GameDetails.Delay);
                _renderer.RenderMenu(MenuViews.MultiGameUI, clearScreen: false);
            }

            _renderer.RenderGridOfFields(game.MultipleGamesField);
            RemoveDeadFieldsFromRendering(game.MultipleGamesField);
        }
    }
}
