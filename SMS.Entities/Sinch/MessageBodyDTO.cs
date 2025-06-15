using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities.Sinch
{
    public class MessageBodyDTO
    {
        public string From { get; set; }
        public  string [] To { get; set; }
        public string Body { get; set; }
    }
}
