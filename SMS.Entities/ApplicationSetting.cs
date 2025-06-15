using SMS.Enums;
using System;
using System.Configuration;

namespace SMS.Entities
{
    public class ApplicationSetting
    {
        public static string UserName { get; set; }
        public static string From { get; set; }
        public static string Password { get; set; }
        public static string SMSSender { get; set; }
        public static SMSProvider SMSProvider { get; set; }
        public static string CustomerId { get; set; }
        public static string Originator { get; set; }
        public static string MessageType { get; set; }
        public static string DefDate { get; set; }
        public static bool Blink { get; set; }
        public static bool Flash { get; set; }
        public static bool Private { get; set; }
        public static string SecretKey { get; set; }
        public static string AccountId { get; set; }
        public static string SinchServicePlanID { get; set; }
        public static string SinchToken { get; set; }
        public static string SinchFrom { get; set; }
        public static string InterfaceId { get; set; }


        public static string LogFilePath
        {
            get
            {
                return string.IsNullOrEmpty(ConfigurationManager.AppSettings["LogFilePath"]) 
                    ? System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs") 
                    : ConfigurationManager.AppSettings["LogFilePath"];
            }
        }
        public static long ClientTxnId { get; set; }


    }
}
