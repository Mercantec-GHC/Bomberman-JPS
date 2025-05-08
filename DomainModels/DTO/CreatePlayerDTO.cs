using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.DTO
{
    public class CreatePlayerDTO
    {
        public string userName { get; set; }
        public string password { get; set; }

        public string email { get; set; }

        public Session sessionId { get; set; }

    }
}
