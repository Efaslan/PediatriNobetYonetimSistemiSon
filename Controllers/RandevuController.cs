using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PediatriNobetYonetimSistemi.Data;
using PediatriNobetYonetimSistemi.Models;

namespace PediatriNobetYonetimSistemi.Controllers
{
    public class RandevuController : Controller
    {
        private readonly DatabaseContext _context;

        public RandevuController(DatabaseContext context)
        {
            _context = context;
        }

        [AllowAnonymous] 
        public IActionResult Index()
        {
            var randevular = _context.Randevu
                .Include(r => r.Hoca)
                .ThenInclude(h => h.Departman)
                .Include(r => r.Asistan)
                .ThenInclude(a => a.Departman)
                .ToList();

            return View(randevular);
        }

        
        [AllowAnonymous]
        public IActionResult Create()
        {
            ViewData["HocaId"] = new SelectList(_context.Hoca
                .Select(h => new
                {
                    HocaId = h.Id,
                    Text = $"{h.Departman.DepartmanAdi} - {h.Ad} {h.Soyad}"
                }),
                "HocaId", "Text");

            ViewData["AsistanId"] = new SelectList(_context.Asistan
                .Select(a => new
                {
                    AsistanId = a.Id,
                    Text = $"{a.Departman.DepartmanAdi} - {a.Ad} {a.Soyad}"
                }),
                "AsistanId", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("Tarih, HocaId, AsistanId")] Randevu randevu)
        {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var randevu = await _context.Randevu.Include(r => r.Hoca).Include(r => r.Asistan).FirstOrDefaultAsync(r => r.Id == id);
            if (randevu == null) return NotFound();

            ViewData["HocaId"] = new SelectList(_context.Hoca
                .Include(h => h.Departman)  // Departman bilgisini almak için Include kullanıyoruz
                .Select(h => new { h.Id, Name = $"{h.Departman.DepartmanAdi} - {h.Ad} {h.Soyad}" }), "Id", "Name", randevu.HocaId);

            ViewData["AsistanId"] = new SelectList(_context.Asistan
                .Include(a => a.Departman)  
                .Select(a => new { a.Id, Name = $"{a.Departman.DepartmanAdi} - {a.Ad} {a.Soyad}" }), "Id", "Name", randevu.AsistanId);

            return View(randevu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Tarih, HocaId, AsistanId")] Randevu randevu)
        {
            if (id != randevu.Id) return NotFound();

                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
           
        }

        [AllowAnonymous]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var randevu = await _context.Randevu.Include(r => r.Hoca).Include(r => r.Asistan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null) return NotFound();

            return View(randevu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var randevu = await _context.Randevu.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevu.Remove(randevu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
