using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace restJsonApi
{
    internal class Program
    {
        public string Post(string uirWebAPI, out string exceptionMessage)
        {
            exceptionMessage = string.Empty;
            string webResponse = string.Empty;
            try
            {
                Uri uri = new Uri(uirWebAPI);
                WebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    dynamic employee = new JObject();
                    employee.username = "aaa";
                    employee.password = "salut";
                    
                    /*JArray array = new JArray();
                    array.Add("Gogu");
                    array.Add("Marcel");

                    employee.usernames = array;*/
                    streamWriter.Write(employee.ToString());
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    webResponse = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                exceptionMessage = $"An error occurred. {ex.Message}";
            }
            return webResponse;
        }

        public string Get(string uri, out string exceptionMessage)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            exceptionMessage = string.Empty;

            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using(Stream stream = response.GetResponseStream())
            using(StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        
        public static void Main(string[] args)
        {
            Program p = new Program();
            Stopwatch s = new Stopwatch();
            s.Start();
            
            string exceptionMessage = string.Empty;
            var uirWebAPI = "http://34.65.236.120:5000/register";
            var webResponse = p.Post(uirWebAPI, out exceptionMessage);
          //  var webResponse = p.Get(uirWebAPI, out exceptionMessage);
            
            Console.WriteLine(s.ElapsedMilliseconds);
            if (string.IsNullOrEmpty(exceptionMessage))
            {
                Console.WriteLine(webResponse.ToString());
            }
            else
            {
                Console.WriteLine(exceptionMessage);
            }
            
            s.Stop();
        }
    }
}