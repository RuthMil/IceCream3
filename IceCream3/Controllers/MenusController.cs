using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IceCream3.Data;
using IceCream3.Models;
using System.Net;
using Firebase.Storage;
using Firebase.Auth;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Specialized;
using System.Globalization;

namespace IceCream3.Controllers
{
    public class MenusController : Controller
    {
        private readonly IceCream3Context _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public static readonly List<string> ImageExtensions = new() { ".JPG", ".JPEG", ".BMP", ".PNG", "TIFF", "TIF" };
        public enum Seasons
        {
            /// Spring begins on the 80th day, on a leap year it begins one the 81
            Spring,
            /// Summer begins on the 172nd day, on a leap year it begins one the 173
            Summer,
            /// Autumn the 266th, on a leap year it begins one the 267
            Autumn,
            /// Winter the 355th, on a leap year it begins one the 356
            Winter
        }

        public MenusController(IceCream3Context context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menu.ToListAsync());
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Flavor,Description,ImageFile,ImageUrl,Price,DateAdded")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (ImageExtensions.Contains(Path.GetExtension(menu.ImageFile.FileName).ToUpperInvariant()))
                {
                    string fireBaseUrl = await UploadToFireBase(menu.ImageFile, menu.Flavor);
                    if (ImageIsIceCream(fireBaseUrl))
                    {
                        _context.Add(menu);
                        menu.DateAdded = DateTime.Now;
                        menu.ImageUrl = fireBaseUrl;
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(Menu.ImageFile), "Image does not contain ice-cream! Please provide an ice-cream image");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(Menu.ImageFile), "The file must be an image from type: jpeg, jpg, png, bmp, tiff, tif");
                }

            }
            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Flavor,Description,ImageFile,ImageUrl,Price,DateAdded")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menu.FindAsync(id);
            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.Id == id);
        }
        public bool ImageIsIceCream(string imageUrl)
        {
            List<string> Data = new List<string>();
            ImaggaDAL ImageAdapter = new ImaggaDAL();
            List<string> Result = ImageAdapter.CheckImage(imageUrl);

            List<string> IceCreamTags = new List<string>() { "ice", "cream", "ice cream" };

            foreach (string Tag in IceCreamTags)
            {
                if (Result.Contains(Tag))
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<string> UploadToFireBase(IFormFile image, string name)
        {
            //var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCoBgC6vDNDQfbYmSaH7C_mMyvvqRofiCM"));
            //var a = await auth.SignInWithEmailAndPasswordAsync("", "password");
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string extension = Path.GetExtension(image.FileName);
            string path = Path.Combine(wwwRootPath + "/images/", name + extension);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var task = new FirebaseStorage("icecream-66e2f.appspot.com").Child(name + ".jpg").PutAsync(stream);
                string url = await task;
                return url;
            }
            //using (FileStream stream = System.IO.File.Open(path, FileMode.Open))
            //{
            //    var task = new FirebaseStorage("icecream-66e2f.appspot.com").Child(name + ".jpg").PutAsync(stream);
            //    //task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
            //    string url = await task;
            //    return url;
            //}
        }

        public static Seasons GetSeason(DateTime date)
        {
            /* Astronomically Spring begins on March 21st, the 80th day of the year. 
             * Summer begins on the 172nd day, Autumn, the 266th and Winter the 355th.
             * Of course, on a leap year add one day to each, 81, 173, 267 and 356.*/

            int doy = date.DayOfYear - Convert.ToInt32((DateTime.IsLeapYear(date.Year)) && date.DayOfYear > 59);

            if (doy < 80 || doy >= 355) return Seasons.Winter;

            if (doy >= 80 && doy < 172) return Seasons.Spring;

            if (doy >= 172 && doy < 266) return Seasons.Summer;

            return Seasons.Autumn;
        }

        public string ExportOrders2CSV()
        {
            var orders = this._context.Order;
            string currentTimeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string outputPath = $"OrdersDataForModel\\orders-{currentTimeStamp}.csv";
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine("City,DayOfWeek,Season,Degree,Humidity,Flavor");
                foreach (Order o in orders)
                {
                    string degree = "0";
                    string humidity = "0";

                    Temperature temp = this._context.Temperature.FirstOrDefault(m => m.Id == o.TemperatureId);
                    if (temp != null)
                    {
                        degree = temp.Degree.ToString();
                        humidity = temp.Humidity.ToString();
                    }
                    writer.WriteLine(o.City + "," + o.TimeOrdered.DayOfWeek.ToString() + "," 
                        + GetSeason(o.TimeOrdered).ToString() + "," + degree + "," + humidity
                        + "," + o.Flavor);
                }
            }
            return outputPath;
        }

        [HttpGet]
        public IActionResult SellPrediction()
        {
            return View();
        }

        [HttpPost, ActionName("SellPrediction")]
        public async Task<IActionResult> SellPrediction(string args = "")
        {
            string city = Request.Form["City"];
            string dayOfWeek = Request.Form["DayOfWeek"];
            string season = Request.Form["Season"];
            string degree = Request.Form["Degree"];
            string humidity = Request.Form["Humidity"];
            BigMLModel myModel = new BigMLModel();
            string dataFilePath = ExportOrders2CSV();
            bool res = await myModel.CreateModel(dataFilePath);
            string prediction = myModel.GetPrediction(city, dayOfWeek, season, degree, humidity);
            Console.WriteLine("*********Predicted flavor: ");
            Console.WriteLine(prediction);
            return RedirectToAction("PresentPrediction", "Menus", new { prediction=prediction});
        }

        public async Task<IActionResult> PresentPrediction(string prediction)
        {
            ViewData["Prediction"] = prediction;
            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Flavor == prediction);
            return View(menu);
        }

        [HttpGet]
        public async Task<IActionResult> Analytics()
        {
            return View(await _context.Menu.ToListAsync());
        }

        [HttpPost, ActionName("Analytics")]
        public IActionResult Analytics(string args = "")
        {
            string flavor = Request.Form["Flavor"].ToString();
            DateTime startDate = DateTime.ParseExact(Request.Form["StartDate"].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(Request.Form["EndDate"].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return RedirectToAction("Graph", "Orders", new { flavor = flavor, startDate=startDate, endDate= endDate });
        }

    }
}
