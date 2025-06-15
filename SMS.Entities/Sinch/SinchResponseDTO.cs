using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities.Sinch
{
    public class SinchResponseDTO
    {
        public string Body { get; set; }
        public string Canceled { get; set; }
        public string Created_At { get; set; }
        public string Delivery_Report { get; set; }
        public string Expired_At { get; set; }
        public string Flash_Messagee { get; set; }
        public string From { get; set; }
        public string Id { get; set; }
        public string Modified_At { get; set; }
        public string To { get; set; }

    }
}
