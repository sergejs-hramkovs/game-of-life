﻿using GameOfLife.Interfaces;
using static GameOfLife.StringConstants;

namespace GameOfLife
{
    public class Field : IField
    {
        private string[,] _fieldArray;
        private string _inputCoordinate;
        private int _coordinateX;
        private int _coordinateY;
        private bool _wrongInput = false;
        private bool _stop = false;
        private ConsoleKeyInfo _seedingChoice;
        private IRender _render;
        private ILibrary _library;
        private IEngine _engine;
        private IRulesApplier _rulesApplier;

        /// <summary>
        /// Initial creation of an empty gaming field.
        /// </summary>
        /// <returns>Returns an array of a gamefield seeded with dead cells(.) .</returns>
        public string[,] CreateField(ILibrary library, int fieldLength, int fieldWidth)
        {
            _library = library;
            _fieldArray = new string[fieldLength, fieldWidth];
            _render = new Render(_engine = new Engine(), _rulesApplier = new RulesApplier(), library);

            for (int i = 0; i < fieldLength; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    _fieldArray[i, j] = DeadCellSymbol;
                }
            }

            return _fieldArray;
        }

        /// <summary>
        /// Method to choose how to seed the field - manually or automatically.
        /// </summary>
        /// <param name="gliderGunMode">Parameter to show whether the glider gun mode is on.</param>
        /// <returns>Returns an array of a seeded gamefield.</returns>
        public string[,] PopulateField(bool gliderGunMode)
        {
            while (true)
            {
                if (_wrongInput)
                {
                    Console.Clear();
                    _render.RenderField(_fieldArray);
                    Console.WriteLine("\n" + WrongInputPhrase);
                    _wrongInput = false;
                }
                if (gliderGunMode)
                {
                    Console.Clear();
                    LibrarySeeding(gliderGunMode);
                    return _fieldArray;
                }
                else
                {
                    _render.SeedFieldMenuRender();
                    _seedingChoice = Console.ReadKey(true);
                }

                switch (_seedingChoice.Key)
                {
                    case ConsoleKey.D1:
                        ManualSeeding();
                        return _fieldArray;

                    case ConsoleKey.D2:
                        RandomSeeding(_fieldArray.GetLength(0), _fieldArray.GetLength(1));
                        return _fieldArray;

                    case ConsoleKey.D3:
                        Console.Clear();
                        LibrarySeeding(gliderGunMode);
                        return _fieldArray;

                    default:
                        _wrongInput = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Cell seeding coordinates are entered manually by the user.
        /// </summary>
        /// <returns>Returns an array of manually seeded gamefield.</returns>
        private string[,] ManualSeeding()
        {
            while (true)
            {
                Console.Clear();
                if (!_wrongInput)
                {
                    _render.RenderField(_fieldArray);
                }
                else if (_wrongInput)
                {
                    _render.RenderField(_fieldArray);
                    Console.WriteLine("\n" + WrongInputPhrase);
                    _wrongInput = false;
                }

                if (!EnterCoordinates())
                {
                    _wrongInput = true;
                    continue;
                }
                else
                {
                    _wrongInput = false;
                }

                // Without this "if" there is a problem with the displaying of the last cell.
                if (!_stop)
                {
                    if (_fieldArray[_coordinateX, _coordinateY] == DeadCellSymbol)
                    {
                        _fieldArray[_coordinateX, _coordinateY] = AliveCellSymbol;
                    }
                    else
                    {
                        _fieldArray[_coordinateX, _coordinateY] = DeadCellSymbol;
                    }
                }
                else
                {
                    _stop = false;
                    break;
                }
            }
            return _fieldArray;
        }

        /// <summary>
        /// Cell amount and coordinates are generated automatically and randomly.
        /// </summary>
        /// <returns>Returns an array of randomly seeded gamefield.</returns>
        private string[,] RandomSeeding(int fieldLength, int fieldWidth)
        {
            Random random = new Random();
            int aliveCellCount = random.Next(1, fieldWidth * fieldLength);
            int randomX, randomY;

            for (int i = 1; i <= aliveCellCount; i++)
            {
                randomX = random.Next(0, _fieldArray.GetLength(0) - 1);
                randomY = random.Next(0, _fieldArray.GetLength(1) - 1);

                if (_fieldArray[randomX, randomY] != AliveCellSymbol)
                {
                    _fieldArray[randomX, randomY] = AliveCellSymbol;
                }
            }
            return _fieldArray;
        }

        /// <summary>
        /// Method to choose a cell pattern from the premade library.
        /// </summary
        /// <param name="gliderGunMode">Parameter to show whether the glider gun mode is on.</param>
        /// <returns>Returns an array of a gamefield seeded with objects from the library.</returns>
        private string[,] LibrarySeeding(bool gliderGunMode)
        {
            ConsoleKeyInfo libraryChoice;

            while (true)
            {
                if (gliderGunMode)
                {
                    _library.SpawnGliderGun(_fieldArray, 1, 1);
                    Console.Clear();
                    return _fieldArray;
                }

                if (!_wrongInput)
                {
                    _render.RenderField(_fieldArray);
                }
                if (_wrongInput)
                {
                    Console.Clear();
                    _render.RenderField(_fieldArray);
                    Console.WriteLine("\n" + WrongInputPhrase);
                    _wrongInput = false;
                }
                _render.LibraryMenuRender();
                libraryChoice = Console.ReadKey(true);

                switch (libraryChoice.Key)
                {
                    case ConsoleKey.Escape:
                        return _fieldArray;

                    case ConsoleKey.D1:
                        if (!EnterCoordinates())
                        {
                            _wrongInput = true;
                            continue;
                        }
                        _library.SpawnGlider(_fieldArray, _coordinateX, _coordinateY);
                        Console.Clear();
                        break;

                    case ConsoleKey.D2:
                        if (!EnterCoordinates())
                        {
                            _wrongInput = true;
                            continue;
                        }
                        _library.SpawnLightWeight(_fieldArray, _coordinateX, _coordinateY);
                        Console.Clear();
                        break;

                    case ConsoleKey.D3:
                        if (!EnterCoordinates())
                        {
                            _wrongInput = true;
                            continue;
                        }
                        _library.SpawnMiddleWeight(_fieldArray, _coordinateX, _coordinateY);
                        Console.Clear();
                        break;

                    case ConsoleKey.D4:
                        if (!EnterCoordinates())
                        {
                            _wrongInput = true;
                            continue;
                        }
                        _library.SpawnHeavyWeight(_fieldArray, _coordinateX, _coordinateY);
                        Console.Clear();
                        break;

                    default:
                        _wrongInput = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Method to process user input coordinates.
        /// </summary>
        /// <returns>Returns "stop = true" if the process of entering coordinates was stopped. Returns false if there is wrong input.</returns>
        private bool EnterCoordinates()
        {
            Console.WriteLine(StopSeedingPhrase);
            Console.Write(EnterXPhrase);
            _inputCoordinate = Console.ReadLine();
            if (_inputCoordinate == StopWord)
            {
                return _stop = true;
            }
            else if (int.TryParse(_inputCoordinate, out var resultX) && resultX >= 0 && resultX < _fieldArray.GetLength(0))
            {
                _coordinateX = resultX;
                Console.Write(EnterYPhrase);
                _inputCoordinate = Console.ReadLine();
                if (_inputCoordinate == StopWord)
                {
                    return _stop = true;
                }
                else if (int.TryParse(_inputCoordinate, out var resultY) && resultY >= 0 && resultY < _fieldArray.GetLength(1))
                {
                    _coordinateY = resultY;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}