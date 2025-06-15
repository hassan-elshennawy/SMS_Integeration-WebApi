using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities.SMSMisr
{
    public class SMSResponse
    {
        public string code { set; get; }
        public int SMSID { set; get; }
        public int etisalat { set; get; }
        public int orange { set; get; }
        public int vodafone { set; get; }
        public int we { set; get; }
        public string Language { set; get; }
        public int Vodafone_cost { set; get; }
        public int Etisalat_cost { set; get; }
        public int orange_cost { set; get; }
        public int we_cost { set; get; }
    }
}
