using Newtonsoft.Json;
using SMS.Entities;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DataService
{
    public class CommunityAdsDSL : ISMS
    {

        private const string API_URL = "https://app.community-ads.com/SendSMSAPI/api/SMSSender/SendSMS";
        public string SendSMS(string mobileNumber, string message)
        {
            string response = "";

            try
            {
                string username = ApplicationSetting.UserName;
                string password = ApplicationSetting.Password;
                string sender = ApplicationSetting.SMSSender;
                var requestBody = JsonConvert.SerializeObject(new
                {
                    UserName = username,
                    Password = password,
                    SMSText = message,
                    SMSLang = "a",
                    SMSSender = sender,
                    SMSReceiver = mobileNumber,
                    SMSID = FormatGuid(Guid.NewGuid()),
                });

                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $" url : {API_URL} " +
                    $"body : {JsonConvert.SerializeObject(requestBody)}  {Environment.NewLine} -- {response}");
                response = Http_Helper.HttpPost(API_URL, requestBody);

                if (response.Contains('0') || response == "ok")
                {
                    response = "True";
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Send Successfully to Number {mobileNumber} and Message {message} {Environment.NewLine} -- {response}");
                    return response;
                }
                response = "False";
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Not Send to Number {mobileNumber} and Message {message} {Environment.NewLine} -- {response}");
                return response;
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Exception, MethodBase.GetCurrentMethod().Name, $"Message Not Send SuccessFully -> Exception {ex} {Environment.NewLine} ");
                return ex.ToString();
            }

            string FormatGuid(Guid guid)
            {
                string guidString = guid.ToString().ToLower();

                string[] parts = guidString.Split('-');
                parts[3] = "1f93";
                return string.Join("-", parts);
            }


        }
    }
}