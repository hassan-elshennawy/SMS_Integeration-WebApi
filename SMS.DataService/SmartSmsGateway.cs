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
    public class SmartSmsGateway : ISMS
    {
        private const string ApiUrl = "https://smartsmsgateway.com/api/api_http.php";
        public string SendSMS(string mobileNumber, string message)
        {
            string userName = ApplicationSetting.UserName;
            string password = ApplicationSetting.Password;
            string senderID = ApplicationSetting.SMSSender;

            string encodedUrlMessage = HttpUtility.UrlEncode(message);
            string response = Http_Helper.HttpGet<string>($"{ApiUrl}?username={userName}&password={password}&senderid={senderID}&to={mobileNumber}&text={encodedUrlMessage}").Result;

            if (response.Contains("OK"))
            {
                return true.ToString().ToLower();
            }
            return response;
        }
    }
}
