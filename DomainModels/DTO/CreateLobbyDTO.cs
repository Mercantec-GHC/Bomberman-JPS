namespace DomainModels.DTO
{
    public class CreateLobbyDTO
    {
        public string Name { get; set; }
        public Guid HostUserID { get; set; }
    }
}
