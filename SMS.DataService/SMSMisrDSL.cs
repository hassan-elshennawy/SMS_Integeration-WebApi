using SMS.Entities;
using SMS.Entities.SMSMisr;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMS.DataService
{
    public class SMSMisrDSL : ISMS
    {
        const string Domain = "https://smsmisr.com/api/SMS/?";

        public string SendSMS(string mobileNumber, string message)
        {
            try
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Sending To Api SMSMisr With Number {mobileNumber} and Message {message} {Environment.NewLine}");
                if (string.IsNullOrEmpty(mobileNumber) || string.IsNullOrEmpty(message))
                {
                    return false.ToString().ToLower();
                }
                SMSResponse smsResponse = SendMessage(mobileNumber, message);
                //Thread.Sleep(1500);  // in Document You must delay at least 1Sec
                if (smsResponse != null && smsResponse.code.Trim().ToLower() == "1901")
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Send Success to Number {mobileNumber} and Message {message} {Environment.NewLine}");
                    return true.ToString().ToLower();
                }
               
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Exception, MethodBase.GetCurrentMethod().Name, $"Message Not Send SuccessFully -> Exception {ex.Message} {Environment.NewLine}");
                return false.ToString().ToLower();
            }
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Not Send Success to Number {mobileNumber} and Message {message} {Environment.NewLine}");
            return false.ToString().ToLower();
        }
        private static object lockObj = new object();
        private SMSResponse SendMessage(string number, string message)
        {
            lock (lockObj)
            {
                SMSMessage smsMessage = new SMSMessage()
                {
                    Username = ApplicationSetting.UserName,
                    password = ApplicationSetting.Password,
                    language = 2, //1 For English, 2 For Arabic, 3 For Unicode  --2 works fine with both arabic and english
                    sender = ApplicationSetting.SMSSender,
                    Mobile = Convert.ToInt64(number),
                    Message = message,
                    Environment = 1

                };
                Thread.Sleep(1000);
                HttpResponse<SMSResponse> result = Helper.Http_Helper.HttpPost<SMSResponse>(Domain, smsMessage);
                if (result != null && result.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return result.Result;
                }
                return null;
            }
        }
    }
}
