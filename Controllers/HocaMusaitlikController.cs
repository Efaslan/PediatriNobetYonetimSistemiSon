using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PediatriNobetYonetimSistemi.Data;
using PediatriNobetYonetimSistemi.Models;

namespace PediatriNobetYonetimSistemi.Controllers
{
    public class HocaMusaitlikController : Controller
    {
        private readonly DatabaseContext _context;

        public HocaMusaitlikController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: HocaMusaitlik
        [AllowAnonymous]
        public IActionResult Index()
        {
            var musaitlikler = _context.HocaMusaitlik
                .Include(hm => hm.Hoca)
                .ToList();

            return View(musaitlikler);
        }

        // GET: HocaMusaitlik/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["HocaId"] = new SelectList(
                _context.Hoca.Select(h => new
                {
                    h.Id,
                    Text = $"{h.Departman.DepartmanAdi} - {h.Ad} {h.Soyad}"
                }),
                "Id", "Text");

            return View();
        }

        // POST: HocaMusaitlik/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("HocaId,Tarih,BaslamaSaati,BitisSaati")] HocaMusaitlik musaitlik)
        {
            
                _context.Add(musaitlik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: HocaMusaitlik/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var musaitlik = await _context.HocaMusaitlik
                .Include(hm => hm.Hoca)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (musaitlik == null) return NotFound();

            return View(musaitlik);
        }

        // POST: HocaMusaitlik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musaitlik = await _context.HocaMusaitlik.FindAsync(id);
            if (musaitlik != null)
            {
                _context.HocaMusaitlik.Remove(musaitlik);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: HocaMusaitlik/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var musaitlik = await _context.HocaMusaitlik.FindAsync(id);
            if (musaitlik == null) return NotFound();

            ViewData["HocaId"] = new SelectList(
                _context.Hoca.Select(h => new
                {
                    h.Id,
                    Text = $"{h.Departman.DepartmanAdi} - {h.Ad} {h.Soyad}"
                }),
                "Id", "Text", musaitlik.HocaId
            );

            return View(musaitlik);
        }

        // POST: HocaMusaitlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HocaId,Tarih,BaslamaSaati,BitisSaati")] HocaMusaitlik musaitlik)
        {
            if (id != musaitlik.Id) return NotFound();

                    _context.Update(musaitlik);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
    }
}
