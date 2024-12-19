using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PediatriNobetYonetimSistemi.Data;
using PediatriNobetYonetimSistemi.Models;

namespace PediatriNobetYonetimSistemi.Controllers
{
    public class DepartmanController : Controller
    {
        private readonly DatabaseContext _context;

        public DepartmanController(DatabaseContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departman.ToListAsync());
        }

        
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var departman = await _context.Departman.FirstOrDefaultAsync(m => m.DepartmanId == id);
            if (departman == null) return NotFound();

            return View(departman);
        }

       
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Yeni Departman Ekle";
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("DepartmanAdi, BolumTanimi, YatakSayisi")] Departman departman)
        {
            if (ModelState.IsValid)
            {
                _context.Departman.Add(departman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departman);
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var departman = await _context.Departman.FindAsync(id);
            if (departman == null) return NotFound();

            return View(departman);
        }

        

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var departman = _context.Departman.Find(id);
            if (departman == null) return NotFound();
            return View(departman);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Departman departman)
        {
            if (id != departman.DepartmanId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var departmanToUpdate = await _context.Departman.FindAsync(id);
                    if (departmanToUpdate == null)
                    {
                        return NotFound();
                    }

                    departmanToUpdate.DepartmanAdi = departman.DepartmanAdi;
                    departmanToUpdate.BolumTanimi = departman.BolumTanimi;
                    departmanToUpdate.YatakSayisi = departman.YatakSayisi;

                    _context.Departman.Update(departmanToUpdate);
                    await _context.SaveChangesAsync(); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Departman.Any(e => e.DepartmanId == departman.DepartmanId))
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
            return View(departman); 
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var departman = await _context.Departman.FirstOrDefaultAsync(m => m.DepartmanId == id);
            if (departman == null) return NotFound();

            return View(departman);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departman = await _context.Departman.FindAsync(id);
            if (departman != null)
            {
                _context.Departman.Remove(departman);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> KullaniciDepartmanlari()
        {
            var departmanlar = await _context.Departman.ToListAsync();
            return View(departmanlar);
        }

    }
}
