using System.Reflection;
using SMS.Entities;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;

namespace SMS.DataService
{
    public class ElitbuzzDSL : ISMS
    {
        private const string apiUrl = "https://www.elitbuzz-me.com/sms/smsapi";
        private string apikey = ApplicationSetting.UserName;
        private string sender = ApplicationSetting.SMSSender;

        public string SendSMS(string mobileNumber, string message)
        {
            string response = Http_Helper.HttpGet<string>($"{apiUrl}?api_key={apikey}&type=text&contacts={mobileNumber}&senderid={sender}&msg={message}").Result;

            if (response != null)
            {
                switch (response.ToLower().Substring(0, 4))
                {
                    case "sms ":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , $"Message Delivered to Client Successfully whose number is: {mobileNumber}");
                        return true.ToString().ToLower();
                    case "1002":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Sender Id/Masking Not Found");
                        return false.ToString().ToLower();
                    case "1003":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "API Key Not Found.");
                        return false.ToString().ToLower();
                    case "1004":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "SPAM Detected");
                        return false.ToString().ToLower();
                    case "1005":
                    case "1006":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Internal Error.");
                        return false.ToString().ToLower();
                    case "1007":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Balance Insufficient.");
                        return false.ToString().ToLower();
                    case "1008":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Message is empty.");
                        return false.ToString().ToLower();
                    case "1009":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Message Type Not Set (text/unicode).");
                        return false.ToString().ToLower();
                    case "1010":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Invalid User & Password.");
                        return false.ToString().ToLower();
                    case "1011":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Invalid User Id.");
                        return false.ToString().ToLower();
                    case "2001":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Invalid Mobile Number.");
                        return false.ToString().ToLower();
                }
            }

            return false.ToString().ToLower();
        }
    }
}
