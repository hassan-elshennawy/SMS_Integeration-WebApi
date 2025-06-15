using System;
using System.Reflection;
using SMS.Entities;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;

namespace SMS.DataService
{
    public class NdmSolutionsDSL : ISMS
    {

        private const string ApiUrl = "https://ndm-solutions.com/sms/api";
        /// <summary>
        /// Provider Doc:
        /// https://products.groupdocs.app/viewer/view?file=77a155cf-5cdb-4118-843a-673f407c2982/SMS%20API%20-%20Web%20%20.docx
        /// Status Codes : it always return 200 OK so you have to check the message.
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendSMS(string mobileNumber, string message)
        {
            string From = ApplicationSetting.From;
            string ApiKey = ApplicationSetting.SecretKey;

            NdmSolutionResponseDTO response = Http_Helper.HttpGet<NdmSolutionResponseDTO>($"{ApiUrl}?action=send-sms" +
                $"&api_key={ApiKey}&to={mobileNumber}&from={From}&&sms={message}&response=json&type=unicode").Result;

            if(response.Message.ToLower().Contains("successfully"))
            {
                return true.ToString().ToLower();
            }
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, 
                                    MethodBase.GetCurrentMethod().Name, 
                                    $"Message Not Send Success to Number {mobileNumber} and Message {message} {Environment.NewLine}");

            return response.Message;
        }
    }
}
