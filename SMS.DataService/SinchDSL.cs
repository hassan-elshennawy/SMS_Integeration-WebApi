using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SMS.Entities;
using SMS.Entities.Sinch;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;

namespace SMS.DataService
{
    public class SinchDSL : ISMS
    {
        public string SendSMS(string mobileNumber, string message)
        {
            try
            {
                string apiUrl = $"https://sms.api.sinch.com/xms/v1/{ApplicationSetting.SinchServicePlanID}/batches";
                string sinchToken = ApplicationSetting.SinchToken;
                string from = ApplicationSetting.SinchFrom;

                MessageBodyDTO messageBodyDTO = new MessageBodyDTO()
                {
                    From = from,
                    Body = message,
                    To = new string[] { mobileNumber }
                };

                var response = Http_Helper.HttpPost<SinchResponseDTO>(apiUrl, messageBodyDTO, sinchToken);

                if (response.HttpStatusCode == HttpStatusCode.Created)
                {

                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $" Message Sent Successfuly....");
                    return true.ToString().ToLower();
                }

                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Message Failed to Send Info :Status Code  {response.HttpStatusCode}");
                return false.ToString().ToLower();
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Exception, MethodBase.GetCurrentMethod().Name, $"Error Message {ex.Message}\r\n{ex.StackTrace}");
                return false.ToString().ToLower();
            }
        }
    }
}
