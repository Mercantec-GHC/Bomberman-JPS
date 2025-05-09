namespace DomainModels
{
    public class ControllerLogs
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public DateTime TimeStamp { get; set; }
        InputType InputType { get; set; }
    }
}