using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PediatriNobetYonetimSistemi.Data;
using PediatriNobetYonetimSistemi.Models;

namespace PediatriNobetYonetimSistemi.Controllers
{
    public class NobetController : Controller
    {
        private readonly DatabaseContext _context;

        public NobetController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Nobet
        [AllowAnonymous] // Nöbet listesini herkes görebilir
        public async Task<IActionResult> Index()
        {
            var nobetler = await _context.Nobet
                .Include(n => n.Asistan)            // Asistan bilgisi
                    .ThenInclude(a => a.Departman)  // Asistan'ın Departmanı
                .Include(n => n.Departman)          // Nöbetin ait olduğu Departman
                .ToListAsync();

            return View(nobetler);
        }

        // GET: Nobet/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AsistanId"] = new SelectList(
                _context.Asistan
                    .Include(a => a.Departman)
                    .Select(a => new
                    {
                        a.Id,
                        Text = $"{a.Departman.DepartmanAdi} - {a.Ad} {a.Soyad}"
                    }),
                "Id", "Text");
            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi");

            return View();
        }

        // POST: Nobet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("AsistanId, DepartmanId, BaslamaTarihi, BaslamaSaati, BitisTarihi, BitisSaati")] Nobet nobet)
        {
            
                _context.Add(nobet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            
        }

        // GET: Nobet/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var nobet = await _context.Nobet.FindAsync(id);
            if (nobet == null) return NotFound();

            ViewData["AsistanId"] = new SelectList(
                _context.Asistan
                    .Include(a => a.Departman)
                    .Select(a => new
                    {
                        a.Id,
                        Text = $"{a.Departman.DepartmanAdi} - {a.Ad} {a.Soyad}"
                    }),
                "Id", "Text", nobet.AsistanId);
            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi", nobet.DepartmanId);
            return View(nobet);
        }

        // POST: Nobet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, AsistanId, DepartmanId, BaslamaTarihi, BaslamaSaati, BitisTarihi, BitisSaati")] Nobet nobet)
        {
            if (id != nobet.Id) return NotFound();

           
                    _context.Update(nobet);
                    await _context.SaveChangesAsync();
                
               
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Nobet/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var nobet = await _context.Nobet
                .Include(n => n.Asistan)
                .Include(n => n.Departman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nobet == null) return NotFound();

            return View(nobet);
        }

        // POST: Nobet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nobet = await _context.Nobet.FindAsync(id);
            if (nobet != null)
            {
                _context.Nobet.Remove(nobet);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
