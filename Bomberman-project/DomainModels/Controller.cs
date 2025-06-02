namespace DomainModels
{
    public class Controller
    {
        public int Id { get; set; }
        public string playerColor { get; set; }
        public float ledBrightness { get; set; }
        public Guid playerId { get; set; }
        public int gyroScopeId { get; set; }
        public int buttonsId { get; set; }
        public Player? Player { get; set; }
        public Gyroscope? Gyroscope { get; set; }
        public Buttons? Buttons { get; set; }
    }
}