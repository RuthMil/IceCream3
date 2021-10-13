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

        public string Image2Base64(string path)
        {
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
        public bool ImageIsIceCream(string imageUrl)
        {
            List<string> Data = new List<string>();
            ImaggaDAL ImageAdapter = new ImaggaDAL();
            List<string> Result = ImageAdapter.CheckImage("https://images.unsplash.com/photo-1497034825429-c343d7c6a68f?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxleHBsb3JlLWZlZWR8MXx8fGVufDB8fHx8&w=1000&q=80");

            List<string> IceCreamTags = new List<string>(){ "ice", "cream", "ice cream"};
            
            foreach (string res in Result)
            {
                Console.WriteLine(res);
            }
            
            foreach (string Tag in IceCreamTags)
            {
                if (Result.Contains(Tag)){
                    return true;
                }
            }
            return false;
        }
    }
}