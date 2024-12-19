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

        // GET: Randevu
        [AllowAnonymous] // Randevu listesini herkes görebilir
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

        // GET: Randevu/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            // Hocaların ve Asistanların departmanla birlikte listelenmesi
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

        // POST: Randevu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("Tarih, HocaId, AsistanId")] Randevu randevu)
        {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Randevu/Edit/5
        [AllowAnonymous]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var randevu = await _context.Randevu.Include(r => r.Hoca).Include(r => r.Asistan).FirstOrDefaultAsync(r => r.Id == id);
            if (randevu == null) return NotFound();

            // Hoca ve Asistan için, Departman adıyla birlikte adları birleştirerek ViewData'ya atıyoruz
            ViewData["HocaId"] = new SelectList(_context.Hoca
                .Include(h => h.Departman)  // Departman bilgisini almak için Include kullanıyoruz
                .Select(h => new { h.Id, Name = $"{h.Departman.DepartmanAdi} - {h.Ad} {h.Soyad}" }), "Id", "Name", randevu.HocaId);

            ViewData["AsistanId"] = new SelectList(_context.Asistan
                .Include(a => a.Departman)  // Departman bilgisini almak için Include kullanıyoruz
                .Select(a => new { a.Id, Name = $"{a.Departman.DepartmanAdi} - {a.Ad} {a.Soyad}" }), "Id", "Name", randevu.AsistanId);

            return View(randevu);
        }

        // POST: Randevu/Edit/5
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

        // GET: Randevu/Delete/5
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var randevu = await _context.Randevu.Include(r => r.Hoca).Include(r => r.Asistan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null) return NotFound();

            return View(randevu);
        }

        // POST: Randevu/Delete/5
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
