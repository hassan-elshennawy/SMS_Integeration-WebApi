using SMS.Entities;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DataService
{
    public class VictoryLinkV2DSL : ISMS
    {
        private const string apiUrl = "https://smsvas.vlserv.com/VLSMSPlatformResellerAPI/NewSendingAPI/api/SMSSender/SendSMS";
        private readonly string username = ApplicationSetting.UserName;
        private readonly string password = ApplicationSetting.Password;
        private readonly string sender = ApplicationSetting.SMSSender;
        private readonly string smsLang = "E";


        public string SendSMS(string mobileNumber, string message)
        {
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Sending To Api VictoryLink With Number {mobileNumber} and Message {message} {Environment.NewLine}");
            HttpResponse<int> result = Http_Helper.HttpPost<int>(
            $"{apiUrl}", new
            {
                username = username,
                password = password,
                smstext = message,
                smsLang = smsLang,
                smssender = sender,
                smsreceiver = mobileNumber,
                smsid = Guid.NewGuid()
            });

;            if (result != null && result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                if (result.Result == 0)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"The SMS is sent successfully {Environment.NewLine}");
                    return true.ToString().ToLower();
                }
                if (result.Result == -1)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Invalid Username or Password {Environment.NewLine}");
                }
                if (result.Result == -2)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"SMS has been sent by an account through not white listed IPs of the sender’s account {Environment.NewLine}");
                }
                if (result.Result == -3)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"SMS Sent to black listed dial of the sender's account {Environment.NewLine}");
                }
                if (result.Result == -5)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Account's quota has ended {Environment.NewLine}");
                }
                if (result.Result == -6)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"SMS platform database down {Environment.NewLine}");
                }
                if (result.Result == -7)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"The sending account isn’t active {Environment.NewLine}");
                }
                if (result.Result == -11)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"The account exceeded the expire date {Environment.NewLine}");
                }
                if (result.Result == -12)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"There is no text {Environment.NewLine}");
                }
                if (result.Result == -13)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"There is a problem with the connection {Environment.NewLine}");
                }
                if (result.Result == -14)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"While sending SMS an error occurs and SMS not sent {Environment.NewLine}");
                }
                if (result.Result == -16)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"This account doesn’t have the send with DLR option. {Environment.NewLine}");
                }
                if (result.Result == -18)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"ANI is wrong {Environment.NewLine}");
                }
                if (result.Result == -19)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Repeated SMS Id, and SMS ID must be unique {Environment.NewLine}");
                }
                if (result.Result == -21)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"The sending account is not exist in the system {Environment.NewLine}");
                }
                if (result.Result == -22)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"SMS not validate yet because the database is down, it will be validated later and sent to kannel {Environment.NewLine}");
                }
                if (result.Result == -23)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"The connection is wrong {Environment.NewLine}");
                }
                if (result.Result == -26)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"SMS ID not a GUID {Environment.NewLine}");
                }
                if (result.Result == -29)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"User name or password fields has no data {Environment.NewLine}");
                }
                if (result.Result == -30)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Invalid SMS fake sender {Environment.NewLine}");
                }
                if (result.Result == -31)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"The start time is invalid {Environment.NewLine}");
                }
                if (result.Result == -32)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"In case of the client enters a key not included in the 'items' parameter {Environment.NewLine}");
                }
                if (result.Result == -100)
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"The account exceeded the expire date {Environment.NewLine}");
                }
            }
            return false.ToString().ToLower();
        }
    }
}