using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SMS.Entities;
using SMS.Entities.SmartMessageEtisalat;
using SMS.Helper;
using SMS.IDataService;

namespace SMS.DataService
{
    public class SmartMessagingEtisalatDSL : ISMS
    {
        private static string GetTokenApiURL = "https://smartmessaging.etisalat.ae:5676/login/user/";
        private static string SendMessageRequestURL = "https://smartmessaging.etisalat.ae:5676/campaigns/submissions/sms/nb";
        public SmartMessagingEtisalatDSL()
        {

        }
        public string SendSMS(string mobileNumber, string message)
        {
            try
            {
                var requestBody = new { username = ApplicationSetting.UserName, password = ApplicationSetting.Password };
                string getTokenJsonObj = JsonConvert.SerializeObject(requestBody);
                string tokenApiResponse = Http_Helper.HttpPost(GetTokenApiURL, getTokenJsonObj);
                var responseObj = JsonConvert.DeserializeObject<TokenApiResponseDTO>(tokenApiResponse);

                string sendSmsJsonObj = JsonConvert.SerializeObject(new SmartMessageEtisalatDTO()
                {
                    senderAddr = ApplicationSetting.SMSSender,
                    clientTxnId = ApplicationSetting.ClientTxnId,
                    recipient = mobileNumber.ToString(),
                    msg = message,
                });

                var sendMessageResponse = Http_Helper.HttpPost(SendMessageRequestURL, sendSmsJsonObj, "Bearer " + responseObj.Token);

                LoggerHelper.WriteToLogFile(Enums.ActionTypeEnum.Information, "SendSMS", $"API Response :  {sendMessageResponse}");

                if (sendMessageResponse.Contains("jobCost"))
                {
                    LoggerHelper.WriteToLogFile(Enums.ActionTypeEnum.Information, "SendSMS", $"Message Sent Successfuly TO Number {mobileNumber}");
                    return "true";
                }


                return sendMessageResponse;
            }
            catch(Exception ex)
            {
                LoggerHelper.WriteToLogFile(Enums.ActionTypeEnum.Exception, "SendSMS", $"Exception Details :  {ex}");
                return ex.Message;
            }
        }
    }
}
