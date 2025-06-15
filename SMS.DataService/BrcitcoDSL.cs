using SMS.Entities;
using SMS.Entities.Mora;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DataService
{
    public class BrcitcoDSL : ISMS
    {
        private const string API_URL = "https://www.brcitco-api.com/api/sendsms";
        public string SendSMS(string mobileNumber, string message)
        {
            string response = "";

            try
            {
                string username = ApplicationSetting.UserName;
                string password = ApplicationSetting.Password;
                string sender = ApplicationSetting.SMSSender;

                var url = $"{API_URL}?user={username}&pass={password}&sender={sender}&" +
              $"to={mobileNumber}&message={message}";

                response = Http_Helper.HttpGet(url);
               

                if (response.Contains('1') || response == "ok")
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Send Successfully to Number {mobileNumber} and Message {message} {Environment.NewLine} -- {response}");
                    return response;
                }

                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Not Send to Number {mobileNumber} and Message {message} {Environment.NewLine} -- {response}");

                return response;
            }catch( Exception ex )
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Exception, MethodBase.GetCurrentMethod().Name, $"Message Not Send SuccessFully -> Exception {ex} {Environment.NewLine} ");
                return ex.ToString();
            }




        }
    }
}
