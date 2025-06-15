using System.Reflection;
using SMS.Entities;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;

namespace SMS.DataService
{
    public class MIMDSL: ISMS
    {
        private const string apiUrl = "https://myinboxmedia.in/api/mim/SendSMS";
        private string user_id = ApplicationSetting.UserName;
        private string password = ApplicationSetting.Password;
        private string sender_id = ApplicationSetting.SMSSender;

        public string SendSMS(string mobileNumber, string message)
        {
            string response = Http_Helper.HttpGet<string>($"{apiUrl}?userid={user_id}&pwd={password}&mobile={mobileNumber}&sender={sender_id}&msg={message}&msgtype=16").Result;

            if (response.ToLower().Contains("successfully"))
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , response);
                return true.ToString().ToLower();
            }
            else
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Exception, MethodBase.GetCurrentMethod().Name
                            , response);
                return false.ToString().ToLower();
            }
        }
    }
}
