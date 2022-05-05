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
        public int Length { get; set; }
        public int Width { get; set; }
        public int Generation { get; set; }
        public int NumberOfFieldsAlive { get; set; }
        public int TotalNumberOfGames { get; set; }
        public int TotalCellsAlive { get; set; }
        public List<GameFieldModel> ListOfGames { get; set; } = new();
        public List<int> GamesToBeDisplayed { get; set; } = new();
        public List<int> DeadFields { get; set; } = new();
        public int NumberOfGamesToBeDisplayed { get; set; }

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

            NumberOfFieldsAlive = ListOfGames.Count;
        }
    }
}
