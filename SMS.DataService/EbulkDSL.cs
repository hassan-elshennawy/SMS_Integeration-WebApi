using SMS.Entities;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;
using System;
using System.Reflection;

namespace SMS.DataService
{
    public class EbulkDSL : ISMS
    {
        private const string ApiUrl = "https://api.ebulksms.com:4433/sendsms";
        public string SendSMS(string mobileNumber, string message)
        {

            string url = $"{ApiUrl}?username={ApplicationSetting.UserName}&apikey={ApplicationSetting.Password}&sender={ApplicationSetting.SMSSender}&messagetext={message}&flash=0&recipients={mobileNumber}";
            string response = Http_Helper.HttpGet(url);

            if (response.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, response);
                return true.ToString().ToLower();
            }
            else
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Exception, MethodBase.GetCurrentMethod().Name, response);
                return response;
            }
        }

    }



}












