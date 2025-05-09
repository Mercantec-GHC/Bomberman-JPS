using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.DTO
{
    public class CreateControllerLogsDTO
    {
        public Player Player { get; set; }
        public DateTime TimeStamp { get; set; }
        Inputtype InputType { get; set; }
    }
}
