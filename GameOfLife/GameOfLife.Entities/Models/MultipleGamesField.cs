using GameOfLife.Interfaces;

namespace GameOfLife.Models
{
    [Serializable]
    public class MultipleGamesField
    {
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
                if (TotalNumberOfGames == 1)
                {
                    return 1;
                }

                return ListOfGames[0].Length switch
                {
                    25 or 20 => 2,
                    15 => 3,
                    10 => 4,
                    _ => 2,
                };
            }
        }
        public int NumberOfHorizontalFields
        {
            get
            {
                if (TotalNumberOfGames == 1)
                {
                    return 1;
                }

                return ListOfGames[0].Length switch
                {
                    25 or 20 => 3,
                    15 => 4,
                    10 => 6,
                    _ => 3,
                };
            }
        }
        public List<SingleGameField> ListOfGames { get; set; } = new List<SingleGameField>();
        public List<int> GamesToBeDisplayed { get; set; } = new List<int>();
        public List<int> DeadFields { get; set; } = new List<int>();
        public List<int> AliveFields { get; set; } = new List<int>();

        /// <summary>
        /// Method to create a list of Game Fields.
        /// </summary>
        /// <param name="fieldOperations">An object of the FieldOperations class.</param>
        public void InitializeGames(IFieldOperations fieldOperations)
        {
            for (int gameNumber = 0; gameNumber < TotalNumberOfGames; gameNumber++)
            {
                ListOfGames.Add(new(Length, Width));
                fieldOperations.PopulateFieldRandomly(ListOfGames[gameNumber]);
            }

            NumberOfFieldsAlive = ListOfGames.Count;
        }

        /// <summary>
        /// Method to initialize necessary parameters in the single game mode.
        /// </summary>
        public void InitializeSingleGameParameters()
        {
            Length = ListOfGames[0].Length;
            Width = ListOfGames[0].Width;
            TotalNumberOfGames = 1;
            NumberOfGamesToBeDisplayed = 1;
            GamesToBeDisplayed.Add(0);
        }
    }
}
