using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCULIS.Constants;
using OCULIS.Data;
using OCULIS.Models;

namespace OCULIS.Controllers
{
    [Authorize(Roles = $"{Uloge.Zaposlenik},{Uloge.Administrator}")]
    public class PregledVidaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PregledVidaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.PregledVida
                .Include(p => p.ElektronskiKarton)
                .ThenInclude(e => e.Korisnik)
                .OrderByDescending(p => p.DatumPregleda)
                .ToListAsync());
        }

        public async Task<IActionResult> Create(int? kartonId)
        {
            ViewData["IdElektronskiKarton"] = new SelectList(
                await _context.ElektronskiKarton.Include(e => e.Korisnik).ToListAsync(),
                "Id", "Napomena", kartonId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DatumPregleda,DioptrijaLijevo,DioptrijaDesno,Preporuka,IdElektronskiKarton")] PregledVida pregled)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pregled);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Pregled vida evidentiran.";
                return RedirectToAction("Details", "ElektronskiKarton", new { id = pregled.IdElektronskiKarton });
            }

            ViewData["IdElektronskiKarton"] = new SelectList(
                await _context.ElektronskiKarton.ToListAsync(), "Id", "Napomena", pregled.IdElektronskiKarton);
            return View(pregled);
        }
    }
}
