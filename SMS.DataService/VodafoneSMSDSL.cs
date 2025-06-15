using SMS.Entities;
using SMS.Enums;
using SMS.Helper;
using SMS.IDataService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SMS.DataService
{
    public class VodafoneSMSDSL : ISMS
    {
        private string GenerateHash(string AccountId, string Password, List<string> ReceiverList, string SMSText)
        {
            byte[] KeyBytes = Encoding.ASCII.GetBytes(ApplicationSetting.SecretKey);
            HMACSHA256 myhmacsha256 = new HMACSHA256(KeyBytes);

            string SMSList = "";
            if (ReceiverList != null & ReceiverList.Count > 0)
            {
                foreach (var receiver in ReceiverList)
                {
                    SMSList += $"&SenderName={ApplicationSetting.SMSSender}&ReceiverMSISDN={receiver}&SMSText={SMSText}";
                }
            }

            string textToHash = string.Format("AccountId={0}&Password={1}{2}", AccountId, Password, SMSList);
            byte[] bytes = Encoding.UTF8.GetBytes(textToHash);
            string hashKey = HashEncode(myhmacsha256.ComputeHash(bytes));
            return hashKey.ToUpper();
        }
        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
        private string SendRequest(List<string> RecieverList, string Message, string hashkey)
        {
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"API URL: https://e3len.vodafone.com.eg/web2sms/sms/submit/" + " {Environment.NewLine}");

            HttpWebRequest request = HttpWebRequest.Create("https://e3len.vodafone.com.eg/web2sms/sms/submit/") as HttpWebRequest;
            request.Method = "POST";
            request.Headers.Add("Access-Control-Allow-Headers", "*");
            request.ContentType = "application/xml";

            Encoding e = Encoding.GetEncoding("UTF-8");
            string SMSList = "";

            foreach (var reciever in RecieverList)
            {
                SMSList += "<SMSList><SenderName>" + ApplicationSetting.SMSSender + "</SenderName><ReceiverMSISDN>" + reciever + "</ReceiverMSISDN><SMSText>" + Message + "</SMSText></SMSList>";
            }
            string reqString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><SubmitSMSRequest xmlns=\"http://www.edafa.com/web2sms/sms/model/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.edafa.com/web2sms/sms/model/SMSAPI.xsd\" xsi:type =\"SubmitSMSRequest\"><AccountId>" + ApplicationSetting.AccountId + "</AccountId><Password>" + ApplicationSetting.Password + "</Password><SecureHash>" + hashkey + "</SecureHash>" + SMSList + "</SubmitSMSRequest>";
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"request String = {reqString}" + Environment.NewLine);
            try
            {
                string requestText = string.Format(reqString, HttpUtility.UrlEncode(reqString, e));
                Stream requestStream = request.GetRequestStream();
                StreamWriter requestWriter = new StreamWriter(requestStream, e);
                requestWriter.Write(requestText);
                requestWriter.Close();
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, "Sent Request" + Environment.NewLine);
                var response = (HttpWebResponse)request.GetResponse();
                string responseText = string.Empty;
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), e))
                {
                    responseText = reader.ReadToEnd();
                }
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, "Responce IS: " + responseText + " " + response.StatusDescription + Environment.NewLine);
                if (response.StatusCode == HttpStatusCode.OK && responseText.ToLower().Contains("success"))
                {
                    return "true";
                }
                else
                {

                    return responseText;
                }

            }
            catch (Exception ex)
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Exception, MethodBase.GetCurrentMethod().Name, ex.Message + Environment.NewLine);

                return ex.Message;
            }


        }
        public string SendSMS(string Receivers, string message)
        {

            try
            {
                List<string> ReceiverList = new List<string>();
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Sending To Api Vodafone With Number {Receivers} and Message {message} {Environment.NewLine}");


                if (!string.IsNullOrEmpty(Receivers) && !string.IsNullOrEmpty(message))
                {
                    ReceiverList = Receivers.Split(',').ToList();

                    string HashKey = GenerateHash(ApplicationSetting.AccountId, ApplicationSetting.Password, ReceiverList, message);
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Generate Hash IS : {HashKey}" + Environment.NewLine);

                    string result = SendRequest(ReceiverList, message, HashKey);
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, "Result from API  >>" + result + Environment.NewLine);
                    return result.ToLower();
                }
                else
                {
                    LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, "Result from API  >> Error : There Are Missing Data ." + Environment.NewLine);
                    return false.ToString().ToLower();
                }

            }
            catch (Exception ex)
            {
                LoggerHelper.WriteToLogFile(MethodBase.GetCurrentMethod().Name, ex, ex.InnerException.ToString());

                return false.ToString().ToLower();
            }
        }
    }
}
