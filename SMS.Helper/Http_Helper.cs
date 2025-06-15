using Newtonsoft.Json;
using SMS.Entities;
using SMS.Enums;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SMS.Helper
{
    public class Http_Helper
    {

        public static HttpResponse<T> HttpGet<T>(string url)
        {
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Start Sending To Api");

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(5);
                return AfterSentRequest<T>(client.GetAsync(url).Result);
            }
        }

        public static string HttpGet(string url)
        {
            string respnse = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                respnse = reader.ReadToEnd();
            }
            return respnse;
        }

        public static string HttpPost(string url, string body,string authorization=null)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            if (!string.IsNullOrEmpty(authorization))
            {
                httpWebRequest.Headers.Add("Authorization", authorization);
            }

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(body);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static HttpResponse<T> HttpPost<T>(string url, object body, string token = null)
        {
            LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Start Sending To Api");
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                HttpResponse<T> httpResponse = new HttpResponse<T>();
                string bodyStr = body.ToJSON();
                StringContent Content = new StringContent(bodyStr, Encoding.UTF8, "application/json");
                return AfterSentRequest<T>(client.PostAsync(url, Content).Result);
            }
        }

        private static HttpResponse<T> AfterSentRequest<T>(HttpResponseMessage response)
        {
            string responseString = string.Empty;
            HttpResponse<T> httpResponse = new HttpResponse<T>();

            if (response != null)
            {
                responseString = response.Content.ReadAsStringAsync().Result;
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Api status:{response.StatusCode} Reposnse From URL : " + responseString + Environment.NewLine);
            }
            httpResponse.HttpStatusCode = response.StatusCode;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                LoggerHelper.WriteToLogFile(ActionTypeEnum.Information, MethodBase.GetCurrentMethod().Name, $"Error In SendingApi Response is {responseString}");
            }
            else
            {
                if (!string.IsNullOrEmpty(responseString))
                {
                    if (response.Content.Headers.ContentType.MediaType.ToLower() == "application/json")
                    {
                        httpResponse.Result = JsonConvert.DeserializeObject<T>(responseString);
                    }
                    else if (response.Content.Headers.ContentType.MediaType.ToLower() == "text/xml")
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(T));
                        using (TextReader reader = new StringReader(responseString))
                        {

                            XmlTextReader xmlTextReader = new XmlTextReader(reader) { Namespaces = false };
                            httpResponse.Result = (T)ser.Deserialize(xmlTextReader);
                        }
                    }
                    else
                    {
                        httpResponse.Result = (T)Convert.ChangeType(responseString, typeof(T));
                    }
                }
                else
                {
                    httpResponse.Result = default(T);
                }
            }
            return httpResponse;
        }
    }
}