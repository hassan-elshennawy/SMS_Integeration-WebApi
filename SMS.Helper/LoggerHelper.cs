using SMS.Entities;
using SMS.Enums;
using System;
using System.IO;

namespace SMS.Helper
{
    public class LoggerHelper
    {
        public static void WriteToLogFile(string methodName, Exception exception, string exceptionDetail)
        {
            WriteToLogFile(ActionTypeEnum.Exception, methodName, exceptionDetail + Environment.NewLine + exception.ToString());
        }


        static object lockObj = new object();
        public static void WriteToLogFile(ActionTypeEnum logAction, string methodName, string message)
        {
            lock (lockObj)
            {
                try
                {
                    string directoryPath = Path.Combine(ApplicationSetting.LogFilePath, DateTime.Now.ToString("yyyy-MM-dd"));
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    using (StreamWriter streamWriter = new StreamWriter(Path.Combine(directoryPath, DateTime.Now.ToString("ddMM") + ".txt"), true))
                    {
                        string[] str = new string[7];
                        str[0] = DateTime.Now.ToString("HH:mm:ss.fff");
                        str[1] = " || ";
                        str[2] = methodName.PadRight(33);
                        str[3] = " || ";
                        str[4] = logAction.ToString().PadRight(11);
                        str[5] = " || ";
                        str[6] = message;
                        streamWriter.WriteLine(string.Concat(str));
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
