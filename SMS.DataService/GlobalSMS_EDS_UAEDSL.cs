using System;
using Newtonsoft.Json;
using SMS.Entities;
using SMS.Helper;
using SMS.IDataService;

namespace SMS.DataService
{
    public class GlobalSMS_EDS_UAEDSL : ISMS
    {
        const string ApiUrl = "http://api.edsfze.com/http/sendsms.aspx";

        public string SendSMS(string mobileNumber, string message)
        {
            try
            {
                if (TryGetUrl(mobileNumber, message, out string url))
                {
                    var response = Http_Helper.HttpGet(url);

                    LoggerHelper.WriteToLogFile(Enums.ActionTypeEnum.Information, "SendSMS", "Response :  " + response + Environment.NewLine);

                    if (response != null && response.ToLower().Contains("success"))
                    {
                        return "\"description\":\"success\"";
                    }
                }

                return "Error";
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteToLogFile(Enums.ActionTypeEnum.Exception, "SendSMS", "Error Message : " + ex.Message + Environment.NewLine);
                LoggerHelper.WriteToLogFile(Enums.ActionTypeEnum.Exception, "SendSMS", "Stack Trace : " + ex.StackTrace + Environment.NewLine);
                return "Error";
            }
        }

        private bool TryGetUrl(string mobileNumber, string message , out string url)
        {
            string senderId = ApplicationSetting.SMSSender;
            string apikey = ApplicationSetting.UserName;
            url = $"{ApiUrl}?apikey= {apikey}&sid={senderId}&msg={message}&msgtype=0&mobiles={mobileNumber}&dlr=1";

            LoggerHelper.WriteToLogFile(Enums.ActionTypeEnum.Information, "SendSMS", "Complete URL  :  " + url + Environment.NewLine);
            
            if(String.IsNullOrWhiteSpace(ApiUrl) ||
               String.IsNullOrWhiteSpace(senderId) ||
               String.IsNullOrWhiteSpace(apikey) ||
               String.IsNullOrWhiteSpace(mobileNumber))
            {
                return false;
            }

            return true;
        }
    }
}
