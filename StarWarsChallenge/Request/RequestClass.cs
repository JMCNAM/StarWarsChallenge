using Newtonsoft.Json;
using StartWarsChallenge.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace StartWarsChallenge.Requests
{
    class RequestClass
    {
        // ------------------------- //
        // Request Handlers
        // ------------------------- //

        private string apiUrl = "http://swapi.co/api";

        public RequestResults<Starship> GetByUrl<Starship>(string url, Dictionary<string, string> parameters)
        {
            RequestResults<Starship> swapiResponse;
            string serializedParameters = "?" + SerializeDictionary(parameters);
            string json = HttpRequest(apiUrl + url + serializedParameters, HttpMethod.GET);

            if (json == "")
            {
                swapiResponse = new RequestResults<Starship>() { next = null };
            }
            else
                swapiResponse = JsonConvert.DeserializeObject<RequestResults<Starship>>(json);

            return swapiResponse;
        }

        private string SerializeDictionary(Dictionary<string, string> dictionary)
        {
            StringBuilder parameters = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                parameters.Append(keyValuePair.Key + "=" + keyValuePair.Value);
            }

            var v = parameters.Remove(parameters.Length - 1, 0).ToString();
            return parameters.Remove(parameters.Length - 1, 0).ToString();
        }

        // ------------------------- //
        // HTTPRequest
        // ------------------------- //


        private string HttpRequest(string url, HttpMethod httpMethod)
        {
            string result = string.Empty;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = httpMethod.ToString();
            httpWebRequest.Timeout = 10000;

            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream());
                result = reader.ReadToEnd();
                reader.Dispose();
            }

            catch (WebException e)
            {
                Console.WriteLine("Error making HTTP request : " + e.Message);
            }
            return result;
        }

        private enum HttpMethod
        {
            GET
        }

    }
}
