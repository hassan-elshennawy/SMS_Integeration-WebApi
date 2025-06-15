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
    public class AptivadigiDSL : ISMS
    {
        private const string apiUrl = "https://smsapi.aptivadigi.com/api";
        private const string cmd = "sendSMS";

        /// <summary>
        /// there is no documentation for this provider according to amr aly requirments 17/2/2022 12:00 PM
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="message"></param>
        /// <returns></returns>


        public string SendSMS(string mobileNumber, string message)
        {
            string username = ApplicationSetting.UserName;
            string password = ApplicationSetting.Password;
            string sender = ApplicationSetting.SMSSender;
            string uniCode = "0";

            string response = Http_Helper.HttpGet<string>($"{apiUrl}?username={username}&password={password}&cmd={cmd}&to={mobileNumber}" +
                        $"&sender={sender}&message={message}&uniCode={uniCode}").Result;
            
            if(response != null)
            {
                bool isSuccess = response.ToLower().Contains("submitted");
                return isSuccess ? "true" : "false";
            }

            return false.ToString().ToLower();
        }
    }
}
