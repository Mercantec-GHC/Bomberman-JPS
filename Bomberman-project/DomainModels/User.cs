using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Key]
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }

    }

    public class UpdateUserInfoDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class UpdateUserPasswordDTO
    {
        public string Password { get; set; }
    }
}