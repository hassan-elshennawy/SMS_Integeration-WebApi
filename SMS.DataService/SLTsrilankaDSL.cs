using SMS.Entities;
using SMS.Helper;
using SMS.IDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SMS.DataService
{
    public class SLTsrilankaDSL : ISMS
    {
        private const string ApiUrl = "https://smsc.slt.lk:8093/api/sms";
        public string SendSMS(string mobileNumber, string message)
        {
            string userName = ApplicationSetting.UserName;
            string password = ApplicationSetting.Password;
            string senderID = ApplicationSetting.SMSSender;
            string encodedUrlMessage = HttpUtility.UrlEncode(message);

            string response = Http_Helper.HttpGet<string>($"{ApiUrl}?src={senderID}&dst={mobileNumber}&dr=0&type=0&user={userName}&password={password}&msg={encodedUrlMessage}").Result;

            if (response.Contains("success"))
                return true.ToString().ToLower();

            return response;

        }
    }
}
