using GameOfLife.Interfaces;

namespace GameOfLife.Models
{
    /// <summary>
    /// The MultipleGamesModel class represents the list of GameFieldModel class objects - the list of Game Fields for the Multiple Games Mode.
    /// It stores the list of fields and different its various parameters.
    /// </summary>
    public class MultipleGamesModel
    {
        private IFieldOperations _fieldOperations;
        private int _length;
        public int Length
        {
            get => _length;
            set => _length = value;
        }
        private int _width;
        public int Width
        {
            get => _width;
            set => _width = value;
        }
        private int _generation;
        public int Generation
        {
            get => _generation;
            set => _generation = value;
        }
        private int _numberOfFieldsAlive;
        public int NumberOfFieldsAlive
        {
            get => _numberOfFieldsAlive;
            set => _numberOfFieldsAlive = value;
        }
        private int _totalNumberOfGames;
        public int TotalNumberOfGames
        {
            get => _totalNumberOfGames;
            set => _totalNumberOfGames = value;
        }
        private int _totalCellsAlive;
        public int TotalCellsAlive
        {
            get => _totalCellsAlive;
            set => _totalCellsAlive = value;
        }
        private List<GameFieldModel> _listOfGames = new();
        public List<GameFieldModel> ListOfGames
        {
            get => _listOfGames;
            set => _listOfGames = value;
        }
        private List<int> _gamesToBeDisplayed = new();
        public List<int> GamesToBeDisplayed
        {
            get => _gamesToBeDisplayed;
            set => _gamesToBeDisplayed = value;
        }
        private List<int> _deadFields = new();
        public List<int> DeadFields
        {
            get => _deadFields;
            set => _deadFields = value;
        }
        private int _numberOfGamesToBeDisplayed;
        public int NumberOfGamesToBeDisplayed
        {
            get => _numberOfGamesToBeDisplayed;
            set => _numberOfGamesToBeDisplayed = value;
        }

        /// <summary>
        /// Method to create a list of Game Fields.
        /// </summary>
        /// <param name="fieldOperations">An object of the FieldOperations class.</param>
        public void InitializeGames(IFieldOperations fieldOperations)
        {
            _fieldOperations = fieldOperations;

            for (int i = 0; i < TotalNumberOfGames; i++)
            {
                ListOfGames.Add(new(Length, Width));
                ListOfGames[i] = _fieldOperations.RandomSeeding(ListOfGames[i]);
            }
            _numberOfFieldsAlive = ListOfGames.Count;
        }
    }
}
