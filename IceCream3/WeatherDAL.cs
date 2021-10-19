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
            string query = String.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric", City, apiKey);
            var client = new RestClient("http://api.openweathermap.org/data/2.5/weather?units=metric");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("q", City);
            request.AddParameter("appid", apiKey);
            //JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(query));
            IRestResponse response = client.Execute(request);
            // Console.Write(response.Content);
            var res = JsonConvert.DeserializeObject<dynamic>(response.Content);
            float degree = res["main"]["temp"];
            float humidity = res["main"]["humidity"];
            Temperature final = new Temperature { Degree = degree, Humidity = humidity};
            return final;

            //if (response.SelectToken("cod").ToString().Equals("200"))
            //{
            //    string res = response.SelectToken("weather[0].main").ToString();
            //    float degree = float.Parse(response.SelectToken("weather[0].main.degree").ToString());
            //    float humidity = float.Parse(response.SelectToken("weather[0].main.humidity").ToString());
            //    float pollution = float.Parse(response.SelectToken("weather[0].main.temp").ToString());
            //    Temperature final = new Temperature { Degree = degree, Humidity = humidity, AirPollution = pollution };
            //    return final;
            //}
            //else
            //{
            //    return null;
            //}
        }
    }
}
