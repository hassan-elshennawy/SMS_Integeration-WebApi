using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using SMS.IDataService;
using SMS.Entities;
using SMS.Entities.AsiaCell;
using SMS.Helper;
using System;
using SMS.Enums;
using System.Reflection;

namespace SMS.DataService
{
    public class AsiaCellDSL : ISMS
    {
        readonly string Domain = "https://messaging.asiacell.com//bms/Soap/Messenger.asmx";

        public AsiaCellDSL()
        {

        }

        public string SendSMS(string mobileNumber, string message)
        {
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Sending To Api AsiaCel With Number {mobileNumber} and Message {message} {Environment.NewLine}");

            // Auth The User Setting in json File
            AuthResultDTO authResultDTO = Auth();

            if (authResultDTO != null && authResultDTO.Result.Trim().ToLower() == "ok")
            {
                // valid credentials 
                // start to send sms
                SendResultDTO sendResultDTO = SendMessage(mobileNumber, message);
                if (sendResultDTO != null  && sendResultDTO.Result.Trim().ToLower() == "ok")
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Send Success to Number {mobileNumber} and Message {message} {Environment.NewLine}");
                    return true.ToString().ToLower();
                }
            }
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Not Send Success to Number {mobileNumber} and Message {message} {Environment.NewLine}");
            return false.ToString().ToLower();
        }

        public AuthResultDTO Auth()
        {
            string url = $"{Domain}/HTTP_Authenticate?customerID={ApplicationSetting.CustomerId}&userName={ApplicationSetting.UserName}&userPassword={ApplicationSetting.Password}";
            HttpResponse<AuthResultDTO> result = Helper.Http_Helper.HttpGet<AuthResultDTO>(url);
            if (result != null && result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return result.Result;
            }
            return null;
        }

        private SendResultDTO SendMessage(string number, string message)
        {
            string url = $"{Domain}/HTTP_SendSms?";
            url += SetParamters(number, message);
            HttpResponse<SendResultDTO> result = Helper.Http_Helper.HttpGet<SendResultDTO>(url);
            if (result != null && result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return result.Result;
            }
            return null;
        }
        private string SetParamters(string phoneNumber, string message)
        {
            return $"customerID={ApplicationSetting.CustomerId}&originator={ApplicationSetting.Originator}&userName={ApplicationSetting.UserName}" +
                   $"&userPassword={ApplicationSetting.Password}&smsText={message}" +
                   $"&recipientPhone={phoneNumber}" +
                   $"&messageType={ApplicationSetting.MessageType}&defDate={ApplicationSetting.DefDate}" +
                   $"&blink={ApplicationSetting.Blink}&flash={ApplicationSetting.Flash}&Private={ApplicationSetting.Private}";
        }
    }
}