using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities.Adafsa
{
    public class SMSResponse
    {
        public SMS SMS { get; set; }
    }

    public class SMS
    {
        public Responses Responses { get; set; }
        public string bStatus { get; set; }
        public string bError { get; set; }
    }

    public class Responses
    {
        public MessageResponse MessageResponse { get; set; }
    }

    public class MessageResponse
    {
        public string MobileNumber { get; set; }
        public string SMSREF { get; set; }
        public string Status { get; set; }
    }
}
