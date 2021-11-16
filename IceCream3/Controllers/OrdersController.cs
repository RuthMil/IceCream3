using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IceCream3.Data;
using IceCream3.Models;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace IceCream3.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IceCream3Context _context;

        public static object Enumerations { get; private set; }

        public OrdersController(IceCream3Context context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Orders/Create/{iceCreamId:int}")]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,TimeOrdered,Flavor,Quantity,City,Street,HouseNum,TemperatureId")] Order order, int iceCreamId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                WeatherDAL weather = new WeatherDAL();
                string address = order.Street;
                var addressParts = address.Split(",");
                string street = addressParts[0].Trim();
                string city = "Jerusalem";
                if (addressParts.Length >= 2)
                {
                    city = addressParts[1].Trim();
                }
                if (city == "Tel Aviv-Yafo")
                    city = "Tel Aviv";
                order.Street = street;
                order.City = city;
                string houseNum = "";
                try
                {
                    var matches = Regex.Matches(street, @"\d+");
                    foreach (var match in matches)
                    {
                        houseNum += match;
                    }
                    order.HouseNum = int.Parse(houseNum);
                }
                catch
                {
                    order.HouseNum = 1;
                }
                Temperature temp = weather.GetWeather(city);
                if (temp != null)
                {
                    this._context.Temperature.Add(temp);
                    this._context.SaveChanges();
                    order.TemperatureId = temp.Id;
                }
                else
                {
                    order.TemperatureId = 0;
                }
                this._context.SaveChanges();
                order.TimeOrdered = DateTime.Now;
                var menuItem = _context.Menu.Find(iceCreamId);
                order.Flavor = menuItem.Flavor;
                await _context.SaveChangesAsync();
                return RedirectToAction("OrderSuccess", "Orders");
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,TimeOrdered,Flavor,Quantity,City,Street,HouseNum")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }


        public async Task<IActionResult> ItemsPage2()
        {
            return View(await _context.Menu.ToListAsync());
        }

        public string GetWeather()
        {
            WeatherDAL ws = new WeatherDAL();
            Temperature temp = ws.GetWeather("Jerusalem");
            return temp.Degree.ToString() + "   " + temp.Humidity.ToString();
        }
        
        public IActionResult OrderSuccess()
        {
            return View();
        }

        public async Task<IActionResult> Graph(string flavor)
        {
            ViewData["desiredFlavor"] = flavor;
            return View(await _context.Order.ToListAsync());
        }

        [HttpGet]
        public IActionResult Analytics()
        {
            return View();
        }

        [HttpPost, ActionName("Analytics")]
        public IActionResult Analytics(string args="")
        {
            string flavor = Request.Form["Flavor"].ToString();
            return RedirectToAction("Graph", "Orders", new { flavor=flavor});
        }
    }
}
