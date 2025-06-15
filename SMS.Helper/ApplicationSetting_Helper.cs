using NT.Integration.Security;
using SMS.Entities;
using SMS.Enums;
using System;
using System.Configuration;

namespace SMS.Helper
{
    public class ApplicationSetting_Helper
    {
        private const string EncryptDecryptKey = "Xp2s5u8x/A?D(G+KbPeShVmYq3t6w9y$B&E)H@McQfTjWnZr4u7x!A%C*F-JaNdR";
        private static readonly EncryptDecryptText encryptDecryptText = new EncryptDecryptText(EncryptDecryptKey);

        public static void Init()
        {
            ApplicationSetting.UserName = SetEncryptConfigration<string>("UserName");
            ApplicationSetting.Password = SetEncryptConfigration<string>("PassWord");
            ApplicationSetting.SMSSender = SetEncryptConfigration<string>("SMSSender");
            ApplicationSetting.SMSProvider = (SMSProvider)SetEncryptConfigration<int>("SMSProvider");
            ApplicationSetting.CustomerId = SetEncryptConfigration<string>("CustomerID");
            ApplicationSetting.Originator = SetEncryptConfigration<string>("Originator");
            ApplicationSetting.MessageType = SetEncryptConfigration<string>("MessageType");
            ApplicationSetting.DefDate = SetEncryptConfigration<string>("DefDate");
            ApplicationSetting.Blink = SetEncryptConfigration<bool>("Blink");
            ApplicationSetting.Flash = SetEncryptConfigration<bool>("Flash");
            ApplicationSetting.Private = SetEncryptConfigration<bool>("Private");
            ApplicationSetting.AccountId = SetEncryptConfigration<string>("AccountID");
            ApplicationSetting.SecretKey = SetEncryptConfigration<string>("SecretKey");
            ApplicationSetting.SinchServicePlanID = SetEncryptConfigration<string>("SinchServicePlanID");
            ApplicationSetting.SinchToken = SetEncryptConfigration<string>("SinchToken");
            ApplicationSetting.SinchFrom = SetEncryptConfigration<string>("SinchFrom");
            ApplicationSetting.ClientTxnId = SetEncryptConfigration<long>("CleintTxnId");
            ApplicationSetting.From = SetEncryptConfigration<string>("From");
            ApplicationSetting.SecretKey = SetEncryptConfigration<string>("SecretKey");
        }

        private static T SetEncryptConfigration<T>(string appSettingKey)
        {
            string key = ConfigurationManager.AppSettings[appSettingKey];

            if (!string.IsNullOrEmpty(key))
            {
                return (T)Convert.ChangeType(encryptDecryptText.DecryptString(key), typeof(T));
            }
            return default(T);
        }
    }
}
