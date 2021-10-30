using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using System.Collections.Specialized;
using Microsoft.Graph;

namespace IceCream3
{
    public class ImaggaDAL
    {
        public List<string> CheckImage(string ImageUrl)
        {
            List<string> Result = null;
            string apiKey = "acc_9a28b094c77bd01";
            string apiSecret = "6b3dfbe6096a12230a5a86c8d7acb754";
            // string imageUrl = "https://d1ymz67w5raq8g.cloudfront.net/Pictures/780xany/3/4/2/513342_gettyimages1053776630_690692.jpg";

            string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));

            var client = new RestClient("https://api.imagga.com/v2/tags");
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddParameter("image_url", ImageUrl);
            request.AddHeader("Authorization", String.Format("Basic {0}", basicAuthValue));

            IRestResponse response = client.Execute(request);
            Console.Write(response.Content);
            if (response == null)
            {
                return null;
            }
            Result = ConvertToList(response.Content);
            return Result;
        }

        public List<string> ConvertToList(string response)
        {
            List<string> Result = new List<string>();

            if(response == "")
            {
                return Result;
            }

            Root TheTags = JsonConvert.DeserializeObject<Root>(response);
            
            if (TheTags.result == null)
            {
                return Result;
            }

            foreach (Tag item in TheTags.result.tags)
            {
                Result.Add(item.tag.en);
            }

            return Result;
        }
    }
}
