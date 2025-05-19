namespace DomainModels
{
    public class ControllerLogs
    {
        public int Id { get; set; }
        public Guid PlayerID { get; set; }
        public Player Player { get; set; }
        public DateTime TimeStamp { get; set; }
        public string InputType { get; set; }
    }
}