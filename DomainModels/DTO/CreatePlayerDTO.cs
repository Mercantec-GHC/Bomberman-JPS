using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
