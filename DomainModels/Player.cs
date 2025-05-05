namespace DomainModels
{
    public class Player : User
    {
        public uint score { get; set; }
        public uint lives { get; set; }
        public PowerUp? powerUp { get; set; }
        public bool inLobby { get; set; }
        public int wins { get; set; }
        public Session sessionId { get; set; }

        public string characterColor { get; set; }

        public Lobby? lobby { get; set; }

        public Bomb bomb { get; set; }
    }
}
