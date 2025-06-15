using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SMS.Entities;
using SMS.Helper;
using SMS.IDataService;

namespace SMS.DataService
{
    public class MshastraDSL:ISMS
    {
        private const string ApiUrl = "https://mshastra.com/sendurl.aspx";
        public string SendSMS(string mobileNumber, string message)
        {
            string userName = ApplicationSetting.UserName;
            string password = ApplicationSetting.Password;
            string senderID = ApplicationSetting.SMSSender;

            string encodedUrlMessage = HttpUtility.UrlEncode(message);
            string response = Http_Helper.HttpGet<string>($"{ApiUrl}?user={userName}&pwd={password}&senderid={senderID}&mobileno={mobileNumber}&msgtext={encodedUrlMessage}&priority=High&CountryCode=ALL").Result;

            if (response.Contains("Successful"))
            {
                return true.ToString().ToLower();
            }
            return response;
        }
    }
}
