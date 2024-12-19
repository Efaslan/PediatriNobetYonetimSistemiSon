using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PediatriNobetYonetimSistemi.Data;
using PediatriNobetYonetimSistemi.Models;

namespace PediatriNobetYonetimSistemi.Controllers
{
    public class AsistanController : Controller
    {
        private readonly DatabaseContext _context;

        public AsistanController(DatabaseContext context)
        {
            _context = context;
        }

        [AllowAnonymous] // Hoca listesini herkes görebilir
        public async Task<IActionResult> Index()
        {
            var asistanlar = await _context.Asistan.Include(h => h.Departman).ToListAsync();
            return View(asistanlar);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Ad, Soyad, Mail, DepartmanId")] Asistan asistan)
        {

            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi", asistan.DepartmanId);

            _context.Add(asistan);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var asistan = await _context.Asistan.FindAsync(id);
            if (asistan == null) return NotFound();

            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi", asistan.DepartmanId);
            return View(asistan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Ad, Soyad, Mail, DepartmanId")] Asistan asistan)
        {
            if (id != asistan.Id) return NotFound();


            _context.Update(asistan);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var asistan = await _context.Asistan.Include(h => h.Departman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asistan == null) return NotFound();

            return View(asistan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asistan = await _context.Asistan.FindAsync(id);
            if (asistan != null)
            {
                _context.Asistan.Remove(asistan);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> KullaniciAsistanlari()
        {
            var asistanlar = await _context.Asistan
                                           .Include(a => a.Departman)
                                           .ToListAsync();

            return View(asistanlar);
        }
    }
}
