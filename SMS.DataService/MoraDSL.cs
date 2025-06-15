using Newtonsoft.Json;
using SMS.Entities;
using SMS.Entities.Mora;
using SMS.Helper;
using SMS.IDataService;

namespace SMS.DataService
{
    public class MoraDSL : ISMS
    {
        private const string apiUrl = "http://mora-sa.com/api/v1/sendsms";
        private string apiKey = ApplicationSetting.SecretKey;
        private string username = ApplicationSetting.UserName;
        private string sender = ApplicationSetting.SMSSender;

        public string SendSMS(string mobileNumber, string message)
        {
            MoraResponseDTO response = Http_Helper.HttpGet<MoraResponseDTO>(
                $"{apiUrl}?api_key={apiKey}&username={username}&message={message}&sender={sender}&numbers={mobileNumber}&unicode=e&return=json").Result;

            if (response.Data.Code == 100)
            {
                return JsonConvert.SerializeObject(new
                {
                    response.Data.Message,
                    Status = "Success"
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    response.Data.Message,
                    Status = "Fail"
                });

            }
       
        }

    }
}

