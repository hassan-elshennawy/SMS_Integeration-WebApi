using SMS.Entities;
using SMS.IDataService;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Ecs.Model.V20140526;
using Aliyun.Acs.Core.Http;
using Newtonsoft.Json;
using SMS.Helper;
using System.Reflection;
using SMS.Enums;
using System;

namespace SMS.DataService
{
    public class AlibabaDSL : ISMS
    {
        public string SendSMS(string mobileNumber, string message)
        {
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name,
                $"Sending To Api SMSMisr With Number {mobileNumber} and Message {message} {Environment.NewLine}");
            try
            {
                string userName = ApplicationSetting.UserName;
                string password = ApplicationSetting.Password;
                string sender = ApplicationSetting.SMSSender;

                IClientProfile profile = DefaultProfile.GetProfile("ap-southeast-1", userName, password);
                DefaultAcsClient client = new DefaultAcsClient(profile);
                CommonRequest request = new CommonRequest();

                request.Domain = "dysmsapi.ap-southeast-1.aliyuncs.com";
                request.Method = MethodType.POST;
                request.Protocol = ProtocolType.HTTPS;
                request.Version = "2018-05-01";
                request.Action = "SendMessageToGlobe";

                // QueryParameters
                request.QueryParameters.Add("RegionId", "ap-southeast-1");
                request.QueryParameters.Add("To", mobileNumber);
                request.QueryParameters.Add("From", sender);
                request.QueryParameters.Add("Message", message);

                CommonResponse response = client.GetCommonResponse(request);

                LogResponseStatus(response.HttpStatus, mobileNumber, message);

                return System.Text.Encoding.Default.GetString(response.HttpResponse.Content);
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Exception, MethodBase.GetCurrentMethod().Name,
                    $"Message Not Send SuccessFully -> Exception {ex.Message} {Environment.NewLine}");

                return "Error";
            }
        }

        private void LogResponseStatus(int statusCode, string mobileNumber, string message)
        {
            if(statusCode == 200)
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name,
                    $"Message Send Success to Number {mobileNumber} and Message {message} {Environment.NewLine}");
            
            else
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name,
                    $"Message Not Send Success to Number {mobileNumber} and Message {message} {Environment.NewLine}");
            
        }
    }
}
