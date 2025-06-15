using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SMS.Entities;
using SMS.Entities.GlobalSms;
using SMS.Helper;
using SMS.IDataService;

namespace SMS.DataService
{
    public class GlobalSmsDSL : ISMS
    {
        //https://globalsms.wisoftsolutions.com:1a111/API/SendSMS?username=skghlabr&apiId=3uviVAzH&json=True&destination=971501125833&source=SKGH%20UAQ&text=test
        const string ApiUrl = "https://globalsms.wisoftsolutions.com:1111/API/SendSMS";

        public string SendSMS(string mobileNumber, string message)
        {
            try
            {
                string userName = ApplicationSetting.UserName;
                string password = ApplicationSetting.Password;
                string senderID = ApplicationSetting.SMSSender;

                var url = $"{ApiUrl}?username={userName}&apiId={password}&json=True&destination={mobileNumber}&source={senderID}";

                LoggerHelper.WriteToLogFile(Enums.ActionTypeEnum.Information, "SendSMS", "Complete URL  :  " + url + Environment.NewLine);

                var requestBody = JsonConvert.SerializeObject(new { text = message });
                var response = Http_Helper.HttpPost(url, requestBody);


                LoggerHelper.WriteToLogFile(Enums.ActionTypeEnum.Information, "SendSMS", "Response :  " + response + Environment.NewLine);

                if (response != null && response.ToLower().Contains("success"))
                {
                    return "\"description\":\"success\"";
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
    }
}
