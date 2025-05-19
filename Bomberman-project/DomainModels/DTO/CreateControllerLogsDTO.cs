using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.DTO
{
    public class CreateControllerLogsDTO
    {
        public Guid PlayerID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string InputType { get; set; }
    }
}
