using System;
using System.Reflection;
using SMS.Entities;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;

namespace SMS.DataService
{
    public class RouteMobileDSL : ISMS
    {
        private const string apiUrl = "https://api.rmlconnect.net:8443/bulksms/bulksms";
        private string type = "5";
        private string dlr = "0";
        private string username = ApplicationSetting.UserName;
        private string password = ApplicationSetting.Password;
        private string sender = ApplicationSetting.SMSSender;

        public string SendSMS(string mobileNumber, string message)
        {
            string response = Http_Helper.HttpGet<string>($"{apiUrl}?username={username}&password={password}&type={type}&dlr={dlr}" +
                        $"&destination={mobileNumber}&source={sender}&message={message}").Result;

            if (response != null)
            {
                switch (response.ToLower().Substring(0, 4))
                {
                    case "1701":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Message Delivered to Client Successfully");
                        return true.ToString().ToLower();
                    case "1702":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Invalid URL.");
                        return false.ToString().ToLower();
                    case "1703":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Invalid value in username or password field.");
                        return false.ToString().ToLower();
                    case "1704":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Invalid value in type field.");
                        return false.ToString().ToLower();
                    case "1705":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "nvalid message.");
                        return false.ToString().ToLower();
                    case "1706":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Invalid destination.");
                        return false.ToString().ToLower();
                    case "1707":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Invalid source (Sender).");
                        return false.ToString().ToLower();
                    case "1708":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Invalid value for dlr field");
                        return false.ToString().ToLower();
                    case "1709":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "User validation failed.");
                        return false.ToString().ToLower();
                    case "1710":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Internal error.");
                        return false.ToString().ToLower();
                    case "1025":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Insufficient credit.");
                        return false.ToString().ToLower();
                    case "1715 ":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Response timeout.");
                        return false.ToString().ToLower();
                    case "1032 ":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "DND reject.");
                        return false.ToString().ToLower();
                    case "1028":
                        LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name
                            , "Spam message");
                        return false.ToString().ToLower();
                    default:
                        return false.ToString().ToLower();
                }
            }

            return false.ToString().ToLower();
        }
    }
}
