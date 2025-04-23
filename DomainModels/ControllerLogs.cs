namespace DomainModels
{
    public class ControllerLogs
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public DateTime TimeStamp { get; set; }
        Inputtype InputType { get; set; }
    }
}