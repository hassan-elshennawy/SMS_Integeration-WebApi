using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities.SmartMessageEtisalat
{
    public class SmartMessageEtisalatDTO
    {
        public string msgCategory { get; set; } = "4.5";
        public string contentType { get; set; } = "3.1";
        public string senderAddr { get; set; }
        public string dndCategory { get; set; } = "";
        public int priority { get; set; } = 1;
        public long clientTxnId { get; set; }
        public string recipient { get; set; }
        public string msg { get; set; }
        public string dr { get; set; } = "1";

    }
}
