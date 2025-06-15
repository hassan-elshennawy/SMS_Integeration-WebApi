using SMS.Entities;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;
using System;
using System.Reflection;

namespace SMS.DataService
{
    public class SmsDeliverSmartDSL : ISMS
    {
        private const string API_URL = "http://smartsmsgateway.com/api/api_http.php";

        public string SendSMS(string mobileNumber, string message)
        {
            string response = "";
            try
            {
                string username = ApplicationSetting.UserName;
                string password = ApplicationSetting.Password;
                string senderID = ApplicationSetting.SMSSender;
                string languageType = "text";
                DateTime date = DateTime.Now;

                var url = $"{API_URL}?username={username}&password={password}&senderid={senderID}&" +
                          $"to={mobileNumber}&text={message}&type={languageType}&datetime={date}";

                response = Http_Helper.HttpGet(url);
                var result = response.IndexOf("ERROR", StringComparison.OrdinalIgnoreCase);
                if (result == -1)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Send Successfully to Number {mobileNumber} and Message {message} {Environment.NewLine} -- {response}");
                    return response;
                }
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Not Send to Number {mobileNumber} and Message {message} {Environment.NewLine} -- {response}");
                return response;
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Exception, MethodBase.GetCurrentMethod().Name, $"Message Not Send SuccessFully -> Exception {ex} {Environment.NewLine} ");
                return ex.ToString();
            }         
        }
    }
}
