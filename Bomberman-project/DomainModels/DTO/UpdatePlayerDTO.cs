namespace DomainModels.DTO
{
    public class UpdatePlayerDTO
    {
        public string Username { get; set; }
        public uint Score { get; set; }
        public uint Lives { get; set; }
        public string CharacterColor { get; set; }
        public int Wins { get; set; }
        public bool InLobby { get; set; } 

        public Session sessionId { get; set; }
        public PowerUp PowerUp { get; set; }
    }
}