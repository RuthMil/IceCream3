using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IceCream3.Models;
using RestSharp;
using Newtonsoft.Json;

namespace IceCream3
{
    public class WeatherDAL
    {
        public Temperature GetWeather(string City)
        {
            string apiKey = "79ad2eca59081f8100d6def78e86d99e";
            var client = new RestClient("http://api.openweathermap.org/data/2.5/weather?units=metric");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("q", City);
            request.AddParameter("appid", apiKey);
            //JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(query));
            IRestResponse response = client.Execute(request);
            var res = JsonConvert.DeserializeObject<dynamic>(response.Content);
            if (res["cod"] != 200)
            {
                return null;
            }
            float degree = res["main"]["temp"];
            float humidity = res["main"]["humidity"];
            Temperature final = new Temperature { Degree = degree, Humidity = humidity};
            return final;
        }
    }
}
