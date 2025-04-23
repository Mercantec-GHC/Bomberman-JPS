namespace DomainModels
{
    public class Lobby
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User HostUserID { get; set; }
        public List<Player> Players { get; set; }
    }
}