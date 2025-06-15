using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities.MersalSMS
{
    public class MersalSmsDto
    {
        public string OrderId { get; set; }
        public string CreditUsed { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
