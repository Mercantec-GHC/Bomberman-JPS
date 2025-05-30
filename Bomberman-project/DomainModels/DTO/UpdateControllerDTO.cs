using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.DTO
{
    public class UpdateControllerDTO
    {
        public int Id { get; set; }
        public string playerColor { get; set; }
        public float ledBrightness { get; set; }
        public Guid playerId { get; set; }
    }
}
