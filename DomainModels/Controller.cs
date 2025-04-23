namespace DomainModels
{
    public class Controller
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public string playerColor { get; set; }
        public float ledBrightness { get; set; }
        public Gyroscope Gyroscope { get; set; }
        public Buttons Buttons { get; set; }
    }
}