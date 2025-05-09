namespace DomainModels.DTO
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}