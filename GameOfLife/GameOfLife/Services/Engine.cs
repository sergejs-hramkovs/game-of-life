using GameOfLife.Interfaces;
using GameOfLife.Models;
using static GameOfLife.StringConstantsModel;

namespace GameOfLife
{
    public class Engine : IEngine
    {
        private int _delay = 1000;
        private int _gliderGunType = 0;
        private bool _readGeneration = false;
        private bool _gliderGunMode = false;
        private bool _gameOver = false;
        private GameFieldModel _gameField;
        private IFileIO _file;
        private IRender _render;
        private IFieldOperations _fieldOperations;
        private ILibrary _library;
        private IRulesApplier _rulesApplier;
        private IEngine _engine;
        private IInputController _inputController;
        public bool ReadGeneration
        {
            get => _readGeneration;
            set => _readGeneration = value;
        }
        public bool GliderGunMode
        {
            get => _gliderGunMode;
            set => _gliderGunMode = value;
        }
        public int GliderGunType
        {
            get => _gliderGunType;
            set => _gliderGunType = value;
        }
        public int Delay
        {
            get => _delay;
            set => _delay = value;
        }

        /// <summary>
        /// Initiate field size choice.
        /// </summary>
        public void StartGame(IRender render, IFileIO file, IFieldOperations operations, ILibrary library, IRulesApplier rulesApplier,
            IEngine engine, IInputController inputController)
        {
            ConsoleKeyInfo fieldSizeChoice;
            _render = render;
            _file = file;
            _fieldOperations = operations;
            _library = library;
            _rulesApplier = rulesApplier;
            _engine = engine;
            _inputController = inputController;
            _inputController.Injection(_engine, _file, _render, _fieldOperations, _library);
            _file.Injection(_render, _inputController, this);

            Console.CursorVisible = false;
            Console.SetWindowSize(170, 55);

            while (true)
            {
                if (!GliderGunMode)
                {
                    _render.MainMenuRender(_inputController.WrongInput, _file.FileReadingError);
                    _inputController.WrongInput = false;
                    _file.FileReadingError = false;
                    fieldSizeChoice = Console.ReadKey(true);
                    _gameField = _inputController.CheckInputMainMenu(fieldSizeChoice);

                    if (_inputController.CorrectKeyPressed)
                    {
                        _inputController.CorrectKeyPressed = false;
                        break;
                    }
                    else if (fieldSizeChoice.Key != ConsoleKey.G && fieldSizeChoice.Key != ConsoleKey.F1)
                    {
                        _inputController.WrongInput = true;
                    }
                }
                else
                {
                    _render.GliderGunModeRender(_inputController.WrongInput);
                    fieldSizeChoice = Console.ReadKey(true);
                    _gameField = _inputController.CheckInputGliderGunMenu(fieldSizeChoice);
                    if (fieldSizeChoice.Key == ConsoleKey.D1 || fieldSizeChoice.Key == ConsoleKey.D2)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Main process of the game.
        /// </summary>
        public void RunGame()
        {
            ConsoleKeyInfo runTimeKeyPress;

            if (!_file.FileLoaded)
            {
                FirstRenderCalculations();
            }
            else
            {
                Console.Clear();
            }
            // To reset the fact of previous loading to avoid disruption of the game after restart.
            _file.FileLoaded = false;

            do
            {
                while (Console.KeyAvailable == false)
                {
                    Console.SetCursorPosition(0, 0);
                    RuntimeCalculations();
                    if (_gameOver)
                    {
                        break;
                    }
                    ReadGeneration = false;
                    Thread.Sleep(Delay);
                }
                if (!_gameOver)
                {
                    runTimeKeyPress = Console.ReadKey(true);
                    _inputController.PauseGame(runTimeKeyPress);
                    _inputController.ChangeDelay(runTimeKeyPress);
                }
                else
                {
                    _gameOver = false;
                    break;
                }
            } while (runTimeKeyPress.Key != ConsoleKey.Escape);

            _render.ExitMenuRender();
            do
            {
                runTimeKeyPress = Console.ReadKey(true);
                _inputController.CheckInputExitMenu(runTimeKeyPress);
            } while (runTimeKeyPress.Key != ConsoleKey.Escape || runTimeKeyPress.Key != ConsoleKey.R);
        }

        /// <summary>
        /// Method for calling necessary methods for the first rendering.
        /// </summary>
        private void FirstRenderCalculations()
        {
            Console.Clear();
            _render.RenderField(_gameField);
            _fieldOperations.PopulateField(_gameField, _gliderGunMode, _gliderGunType);
            Console.Clear();
            _render.BlankUIRender();
            _render.RenderField(_gameField);
            Thread.Sleep(2000);
            Console.Clear();
        }

        /// <summary>
        /// Method for calling methods that take care of all the necessary calculations during the runtime.
        /// </summary>
        private void RuntimeCalculations()
        {
            // Parameter for proper loading from file.
            int generationsAfterLoading = 1;

            // ReadGeneration ensures loading of a proper generation number when loading from the file.
            if (ReadGeneration)
            {
                generationsAfterLoading = 0;
                ReadGeneration = false;
            }

            // generationsAfterLoading ensure proper game field rendering.
            // Helps to avoid loading of the previous or next game state.
            if (generationsAfterLoading >= 1)
            {
                _rulesApplier.DetermineCellsDestiny(_gameField, GliderGunMode);
                _rulesApplier.FieldRefresh(_gameField);
                CountAliveCells();
            }
            if (generationsAfterLoading == 0)
            {
                generationsAfterLoading++;
                CountAliveCells();
            }

            if (_gameField.AliveCellsNumber == 0)
            {
                _render.GameOverRender(_gameField.Generation);
                _gameOver = true;
            }
            else
            {
                _gameField.Generation++;
                _render.RuntimeUIRender(_gameField, Delay);
            }

            if (_gameOver)
            {
                _render.RenderField(_gameField, true);
            }
            else
            {
                _render.RenderField(_gameField);
            }
        }



        /// <summary>
        /// Method to restart the game without rerunning the application.
        /// </summary>
        public void RestartGame()
        {
            GliderGunMode = false;
            Delay = 1000;
            Console.Clear();
            StartGame(_render, _file, _fieldOperations, _library, _rulesApplier, _engine, _inputController);
            RunGame();
        }

        /// <summary>
        /// Method to count the current number of alive cells on the field.
        /// </summary>
        public void CountAliveCells()
        {
            _gameField.AliveCellsNumber = 0;

            for (int i = 0; i < _gameField.Length; i++)
            {
                for (int j = 0; j < _gameField.Width; j++)
                {
                    if (_gameField.GameField[i, j] == AliveCellSymbol)
                    {
                        _gameField.AliveCellsNumber++;
                    }
                }
            }
        }
    }
}
