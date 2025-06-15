using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.IDataService
{
    public interface ISMS
    {
        string SendSMS(string mobileNumber, string message);
    }
}
