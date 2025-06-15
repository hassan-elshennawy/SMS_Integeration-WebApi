using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities.GlobalSms
{
    public class GlobalSmsResponseDTO
    {
        public int ErrorCode { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string OriginatingAddress { get; set; }
        public string DestinationAddress { get; set; }
        public int MessageCount { get; set; }

 
    }
}
