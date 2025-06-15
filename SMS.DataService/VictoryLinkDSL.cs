
using SMS.Entities;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;
using System;
using System.Reflection;
using System.Text;
using System.Web;

namespace SMS.DataService
{
    public class VictoryLinkDSL:ISMS
    {
        public string SendSMS(string mobileNumber, string message)
        {
            string smsLang = "A";
            string encodedUrlMessage = HttpUtility.UrlEncode(message);
            string url =
                string.Format(@"https://smsvas.vlserv.com/KannelSending/service.asmx/SendSMS?username={0}&password={1}&SMSText={2}&SMSLang={3}&SMSSender={4}&SMSReceiver={5}",
                ApplicationSetting.UserName,ApplicationSetting.Password, encodedUrlMessage, smsLang, ApplicationSetting.SMSSender, mobileNumber);
            
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Sending To Api VictoryLink With Number {mobileNumber} and Message {message} {Environment.NewLine}");
            HttpResponse<int> result = Helper.Http_Helper.HttpGet<int>(url);
            if (result != null && result.HttpStatusCode ==System.Net.HttpStatusCode.OK)
            {
                if (result.Result == 0)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"The SMS is sent successfully {Environment.NewLine}");
                    return true.ToString().ToLower();
                }
                else if (result.Result == -1)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"User is not subscribed {Environment.NewLine}");
                }
                else if (result.Result == -5)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"out of credit {Environment.NewLine}");
                }
                else if (result.Result == -10)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Queued Message, no need to send it again { Environment.NewLine}");
                }
                else if (result.Result == -11)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Invalid language { Environment.NewLine}");
                }
                else if (result.Result == -12)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"SMS is empty { Environment.NewLine}");
                }
                else if (result.Result == -13)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Invalid fake sender exceeded 12 chars or empty{ Environment.NewLine}");
                }
                else if (result.Result == -25)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Sending rate greater than receiving rate(only for send / receive accounts { Environment.NewLine}");
                }
                else if (result.Result == -100)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"other error { Environment.NewLine}");
                }
            }
            return false.ToString().ToLower();
        }
    }
}
