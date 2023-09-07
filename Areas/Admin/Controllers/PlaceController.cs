using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YESHome.Data.Models;
using YesHome.Data;
using Microsoft.AspNetCore.Authorization;
using YESHome.Areas.Admin.Models.PlaceVM;

namespace YESHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PlaceController : Controller
    {
        private readonly YESHomeDb _context;

        public PlaceController(YESHomeDb context)
        {
            _context = context;
        }

        // GET: Admin/Places
        public async Task<IActionResult> Index()
        {
            //var places = await _context.Places
            //    .Include(x => x.Reports)
            //    .ThenInclude(x => x.User)
            //    .Where(x => x.Reports.FirstOrDefault(x => x.WorkStart.Date == DateTime.Today) != null)
            //    .ToListAsync();

            //List<InfoVM> list = new();
            //foreach (var place in places)
            //{
            //    InfoVM tmp = new()
            //    {
            //        Id = place.Id,
            //        Name = place.Name,
            //        EmployeeNames = place.Reports.Select(x => x.User.UserName).ToList() 
            //    };

            //    list.Add(tmp);
            //}

            var places = await _context.Places.ToListAsync();

            List<InfoVM> list = new();
            foreach (var place in places)
            {
                InfoVM tmp = new()
                {
                    Id = place.Id,
                    Name = place.Name
                };

                list.Add(tmp);
            }

            return View(list);
        }

        // GET: Admin/Places/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Places == null)
            {
                return NotFound();
            }

            var place = await _context.Places
                .FirstOrDefaultAsync(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // GET: Admin/Places/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Places/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlaceCreateVM model)
        {
            if (ModelState.IsValid)
            {
                _context.Places.Add(new Place() 
                {
                    Name=model.Name,
                    Latitude= Convert.ToDouble(model.Latitude.Replace(".",",")),
                    Longitude= Convert.ToDouble(model.Longitude.Replace(".", ","))
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Admin/Places/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Places == null)
            {
                return NotFound();
            }

            var place = await _context.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            return View(new PlaceEditVM() { 
                Id= place.Id,
                Name=place.Name,
                Latitude=""+place.Latitude,
                Longitude="" + place.Longitude
                });
        }

        // POST: Admin/Places/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlaceEditVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var oldPlace = _context.Places.FirstOrDefault(x=>x.Id==model.Id);
                    oldPlace.Name = model.Name;
                    oldPlace.Latitude = Convert.ToDouble(model.Latitude.Replace(".", ","));
                    oldPlace.Longitude = Convert.ToDouble(model.Longitude.Replace(".", ","));

                    _context.Update(oldPlace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(model.Id))
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
            return View(model);
        }

        // GET: Admin/Places/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Places == null)
            {
                return NotFound();
            }

            var place = await _context.Places
                .FirstOrDefaultAsync(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // POST: Admin/Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Places == null)
            {
                return Problem("Entity set 'YESHomeDb.Places'  is null.");
            }
            var place = await _context.Places.FindAsync(id);
            if (place != null)
            {
                _context.Places.Remove(place);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceExists(string id)
        {
            return (_context.Places?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
