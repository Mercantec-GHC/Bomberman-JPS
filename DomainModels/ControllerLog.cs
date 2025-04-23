

namespace DomainModels
{
    public class ControllerLog
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public DateTime timeStamp { get; set; }
        public Inputtype InputType { get; set; }
    }
}
