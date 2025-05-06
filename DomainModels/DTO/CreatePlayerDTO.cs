using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.DTO
{
    public class CreatePlayerDTO
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }
        public string Username { get; set; }
        public uint Score { get; set; } = 0;
        public uint Lives { get; set; } = 3;

        public Session sessionId { get; set; }

        public string CharacterColor { get; set; }

        public int Wins { get; set; }


    }
}
