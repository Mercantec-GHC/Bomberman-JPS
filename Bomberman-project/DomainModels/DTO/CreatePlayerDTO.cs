using System.ComponentModel.DataAnnotations;

namespace DomainModels.DTO
{
    public class CreatePlayerDTO
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }

        [Required]
        public string email { get; set; }

        public Session sessionId { get; set; }

    }
}