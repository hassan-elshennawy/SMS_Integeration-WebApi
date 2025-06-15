using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities.Adafsa
{
    public class SMSRequest
    {
        public string InterfaceID { get;set; }
        public string PhoneNos { get;set; }
        public string MSG { get;set; }
        public string lang { get;set; }
    }
}
