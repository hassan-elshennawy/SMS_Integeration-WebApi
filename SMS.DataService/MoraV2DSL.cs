using Newtonsoft.Json;
using SMS.Entities;
using SMS.Helper;
using SMS.IDataService;

namespace SMS.DataService
{
    public class MoraV2DSL : ISMS
    {
        private const string apiUrl = "http://mora-sa.com/api/v1/sendsms";
        private string apiKey = ApplicationSetting.SecretKey;
        private string username = ApplicationSetting.UserName;
        private string sender = ApplicationSetting.SMSSender;

        public string SendSMS(string mobileNumber, string message)
        {
            int response = Http_Helper.HttpGet<int>(
                $"{apiUrl}?api_key={apiKey}&username={username}&message={message}&sender={sender}&numbers={mobileNumber}&response=text").Result;

            if (response == 100)
            {
                return JsonConvert.SerializeObject(new
                {
                    response,
                    Status = "Success"
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    response,
                    Status = "Fail"
                });

            }

        }

    }
}

