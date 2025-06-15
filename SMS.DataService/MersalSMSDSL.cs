using Newtonsoft.Json;
using SMS.Entities.Mora;
using SMS.Entities;
using SMS.Helper;
using System;
using SMS.Entities.MersalSMS;
using SMS.Enums;
using System.Reflection;
using SMS.IDataService;
using static System.Net.WebRequestMethods;

namespace SMS.DataService
{
    public class MersalSMSDSL : ISMS
    {
        private const string apiUrl = "https://portal.mersalsms.com/httpget/";
        private string apiKey = ApplicationSetting.SecretKey;
        private string username = ApplicationSetting.UserName;
        private string sender = ApplicationSetting.SMSSender;

        public string SendSMS(string mobileNumber, string message)
        {
            MersalSmsDto smsResponse = new MersalSmsDto();

            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name,
                      $"Initiating SMS send request to {mobileNumber} with message: {message}");

            // Make the HTTP GET request
            var response = Http_Helper.HttpGet<string>(
                $"{apiUrl}?username={username}&apikey={apiKey}&sender={sender}&scheduled=&to=2{mobileNumber}&text={message}&submit=").Result;

            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name,
             $"Received response: {JsonConvert.SerializeObject(response)}");
            if (response.Contains("ORDERID"))
            {
                return JsonConvert.SerializeObject(new
                {
                    response,
                    Status = "Success"
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    response,
                    Status = "Fail"
                });

            }

        }
    }
}
