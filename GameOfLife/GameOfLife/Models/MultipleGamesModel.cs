using GameOfLife.Interfaces;

namespace GameOfLife.Models
{
    /// <summary>
    /// The MultipleGamesModel class represents the list of GameFieldModel class objects - the list of Game Fields for the Multiple Games Mode.
    /// It stores the list of fields and different its various parameters.
    /// </summary>
    [Serializable]
    public class MultipleGamesModel
    {
        private IFieldOperations _fieldOperations;
        public int Length { get; set; }
        public int Width { get; set; }
        public int Generation { get; set; }
        public int NumberOfFieldsAlive { get; set; }
        public int TotalNumberOfGames { get; set; } = 1000;
        public int TotalCellsAlive { get; set; }
        public int NumberOfGamesToBeDisplayed { get; set; }
        public int NumberOfRows
        {
            get
            {
                switch (ListOfGames[0].Length)
                {
                    case 25:

                    case 20:
                        return 2;

                    case 15:
                        return 3;

                    case 10:
                        return 4;

                    default:
                        return 2;
                }
            }
        }
        public int NumberOfHorizontalFields
        {
            get
            {
                switch (ListOfGames[0].Length)
                {
                    case 25:

                    case 20:
                        return 3;

                    case 15:
                        return 4;

                    case 10:
                        return 6;

                    default:
                        return 3;
                }
            }
        }
        public List<GameFieldModel> ListOfGames { get; set; } = new();
        public List<int> GamesToBeDisplayed { get; set; } = new();
        public List<int> DeadFields { get; set; } = new();

        /// <summary>
        /// Method to create a list of Game Fields.
        /// </summary>
        /// <param name="fieldOperations">An object of the FieldOperations class.</param>
        public void InitializeGames(IFieldOperations fieldOperations)
        {
            _fieldOperations = fieldOperations;
            for (int gameNumber = 0; gameNumber < TotalNumberOfGames; gameNumber++)
            {
                ListOfGames.Add(new(Length, Width));
                ListOfGames[gameNumber] = _fieldOperations.RandomSeeding(ListOfGames[gameNumber]);
            }

            NumberOfFieldsAlive = ListOfGames.Count;
        }
    }
}
