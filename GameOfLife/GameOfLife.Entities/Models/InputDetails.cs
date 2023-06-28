namespace GameOfLife.Entities.Models
{
    public class InputDetails
    {
        public bool WrongInput { get; set; }

        public bool IsCorrectKeyPressed { get; set; }

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }

        public bool StopDataInput { get; set; }
    }
}
