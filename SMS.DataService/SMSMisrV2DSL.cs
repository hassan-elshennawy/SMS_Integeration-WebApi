using SMS.Entities.SMSMisr;
using SMS.Entities;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;
using System;
using System.Reflection;
using System.Text;
using System.Threading;

namespace SMS.DataService
{
    public class SMSMisrV2DSL : ISMS
    {
        private readonly static object lockObj = new object();
        private string Domain = "https://smsmisr.com/api/SMS/?";

        public SMSMisrV2DSL()
        {
            ConstructUrl();
        }

        private void ConstructUrl()
        {
            Domain = new StringBuilder(Domain).Append("environment=1&").Append($"username={ApplicationSetting.UserName}&").Append($"password={ApplicationSetting.Password}&")
                .Append("language=2&").Append($"sender={ApplicationSetting.SMSSender}&").Append("mobile={0}&").Append("message={1}&").Append("DelayUntil=202307190120").ToString();
        }

        public string SendSMS(string mobileNumber, string message)
        {
            try
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Sending To Api SMSMisrV2 With Number {mobileNumber} and Message {message} {Environment.NewLine}");
                if (string.IsNullOrEmpty(mobileNumber) || string.IsNullOrEmpty(message))
                {
                    return false.ToString().ToLower();
                }
                SMSResponse smsResponse = SendMessage(mobileNumber, message);
                if (smsResponse?.code.Trim().ToLower() == "1901")
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Sent Success to Number {mobileNumber} and Message {message} {Environment.NewLine}");
                    return true.ToString().ToLower();
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Exception, MethodBase.GetCurrentMethod().Name, $"Message Not Sent -> Exception {ex.Message} {Environment.NewLine}");
                return false.ToString().ToLower();
            }
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Not Sent to Number {mobileNumber} and Message {message} {Environment.NewLine}");
            return false.ToString().ToLower();
        }

        private SMSResponse SendMessage(string number, string message)
        {
            lock (lockObj)
            {
                Thread.Sleep(1000);
                var url = string.Format(Domain, number, message);
                HttpResponse<SMSResponse> result = Http_Helper.HttpPost<SMSResponse>(url,null);
                if (result?.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return result.Result;
                }
                return null;
            }
        }
    }
}

