using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities.SMSMisr
{
    public class SMSMessage
    {
        public string Username { set; get; }
        public string password { set; get; }
        public int language { set; get; }
        public string sender { set; get; }
        public long Mobile { set; get; }
        public string Message { set; get; }
        public int Environment { get; set; }
    }
}
