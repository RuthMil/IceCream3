using IceCream3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore;
using RestSharp;

namespace IceCream3.Controllers
{
    
    public class AddIceCreamImageController : Controller
    {

        private readonly ILogger<AddIceCreamImageController> _logger;

        public AddIceCreamImageController(ILogger<AddIceCreamImageController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string Image2Base64(string path){
            using (Image image = Image.FromFile("image.jpg"))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        static async Task<string> GetAPIAsync(string path)
        {
                HttpClient client = new HttpClient();
                string product = null;
                HttpResponseMessage response = await client.GetAsync("https://api.imagga.com/v2/tags?Basic=acc_9a28b094c77bd01:6b3dfbe6096a12230a5a86c8d7acb754&image_url=https://images.unsplash.com/photo-1497034825429-c343d7c6a68f?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxleHBsb3JlLWZlZWR8MXx8fGVufDB8fHx8&w=1000&q=80");
                if (response.IsSuccessStatusCode)
                    {
                        product = await response.Content.ReadAsAsync<string>();
                    }
                return product;
        }
        public string ImageIsIceCream(string image_path){
            //Add CloudinaryDotNet using Nuget Package Manager or if using Package Manager Console: Install-Package CloudinaryDotNet
            // Account account = new Account(
            // "doc1d2moa",
            // "227426427778973",
            // "Y0SO9Osy65sUWk8SMKdOw76ENCM");

            // Cloudinary cloudinary = new Cloudinary(account);
            // cloudinary.Api.Secure = true;
            
            // var uploadParams = new ImageUploadParams() 
            // {
            //     File = new FileDescription(@"/Users/ruthmiller/Documents/computer_science_studies/4th_year/cloud_computing/IceCream3/IceCream3/wwwroot/images/45min.jpg"),
            //     Categorization = "imagga_tagging"
            // };
            // var uploadResult = cloudinary.Upload(uploadParams);
            // Console.WriteLine(uploadResult);
            // string jsonString = JsonSerializer.Serialize(uploadResult);
            // Console.WriteLine(jsonString);
            // return jsonString;
            string Result = string.Empty;         
            string Basic = "acc_9a28b094c77bd01:6b3dfbe6096a12230a5a86c8d7acb754";
            string ImageUrl = "https://images.unsplash.com/photo-1497034825429-c343d7c6a68f?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxleHBsb3JlLWZlZWR8MXx8fGVufDB8fHx8&w=1000&q=80";
       
            var client = new RestClient("https://api.imagga.com/v2/tags");
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddParameter("Basic", Basic);
            request.AddParameter("ImageUrl", ImageUrl);
            //request.AddHeader("Authorization", String.Format("Basic {0}", basicAuthValue));

            IRestResponse response = client.Execute(request);
            Result = response.Content;
            // Console.Write(response.Content);
            Console.WriteLine(Result);
            return Result;
        }
    }
}
