﻿using System;
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

namespace IceCream3.Controllers
{
    public class MenusController : Controller
    {
        private readonly IceCream3Context _context;

        public MenusController(IceCream3Context context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("Id,Flavor,Description,ImageUrl,Price,DateAdded")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (ImageIsIceCream(menu.ImageUrl))
                {
                    UploadToFireBase(menu.ImageUrl, menu.Flavor);
                    _context.Add(menu);
                    menu.DateAdded = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl,Price,DateAdded")] Menu menu)
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
        public async Task UploadToFireBase(string imageUrl,string name)
        {
            WebClient client = new WebClient();
            string path = @"D:\" + name + ".jpg";
            client.DownloadFile(imageUrl, path);//Download img to computer
            var stream = System.IO.File.Open(path, System.IO.FileMode.Open);
            var task = new FirebaseStorage("icecream3-53315.appspot.com").Child(name +".jpg").PutAsync(stream);
            var url = await task;
        }
    }
}
