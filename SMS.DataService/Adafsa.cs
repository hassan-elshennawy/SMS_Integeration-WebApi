using SMS.Entities;
using SMS.Entities.Adafsa;
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
    public class Adafsa : ISMS
    {
        const string SmsUrl = "http://AEADCMS04-AFS.ADFCA.AE:7004/ADAFSA_SMSService/SendSMS";
        public string SendSMS(string mobileNumber, string message)
        {
            try
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Sending To Api Adafsa With Number {mobileNumber} and Message {message} ");
                if (string.IsNullOrEmpty(mobileNumber) || string.IsNullOrEmpty(message))
                {
                    return false.ToString().ToLower();
                }
                SMSResponse smsResponse = SendMessage(mobileNumber, message);
                if (smsResponse != null)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Send Success to Number {mobileNumber} and Message {message} ");
                    return true.ToString().ToLower();
                }

            }
            catch (Exception ex)
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Exception, MethodBase.GetCurrentMethod().Name, $"Message Not Send SuccessFully -> Exception {ex.Message}");
                return false.ToString().ToLower();
            }
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Not Send Success to Number {mobileNumber} and Message {message} ");
            return false.ToString().ToLower();
        }
        private SMSResponse SendMessage(string number, string message)
        {
            SMSRequest smsRequest = new SMSRequest()
            {
                InterfaceID = ApplicationSetting.InterfaceId, // where to get the interface ID
                PhoneNos = number,
                MSG = message,
                lang = "EN",
            };
            HttpResponse<SMSResponse> response = Helper.Http_Helper.HttpPost<SMSResponse>(SmsUrl,smsRequest);
            if (response != null && response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Result;
            }
            return null;
        }
    }
}
