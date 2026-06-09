using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCULIS.Constants;
using OCULIS.Data;
using OCULIS.Models;

namespace OCULIS.Controllers
{
    [Authorize]
    public class ReklamacijaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public ReklamacijaController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var query = _context.Reklamacija
                .Include(r => r.Korisnik)
                .Include(r => r.Narudzba)
                .AsQueryable();

            if (User.IsInRole(Uloge.Kupac))
            {
                var user = await _userManager.GetUserAsync(User);
                query = query.Where(r => r.IdKorisnik == user!.Id);
            }

            return View(await query.OrderByDescending(r => r.DatumPodnosenja).ToListAsync());
        }

        [Authorize(Roles = Uloge.Kupac)]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["IdNarudzba"] = new SelectList(
                await _context.Narudzba.Where(n => n.IdKorisnik == user!.Id).ToListAsync(),
                "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Uloge.Kupac)]
        public async Task<IActionResult> Create([Bind("Opis,IdNarudzba")] Reklamacija reklamacija)
        {
            var user = await _userManager.GetUserAsync(User);
            reklamacija.IdKorisnik = user!.Id;
            reklamacija.DatumPodnosenja = DateTime.Now;
            reklamacija.Status = StatusReklamacije.Podnesena;

            var karton = await _context.ElektronskiKarton.FirstOrDefaultAsync(e => e.IdKorisnik == user.Id);
            reklamacija.IdElektronskiKarton = karton?.Id;

            if (ModelState.IsValid)
            {
                _context.Add(reklamacija);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Reklamacija uspješno podnesena.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdNarudzba"] = new SelectList(
                await _context.Narudzba.Where(n => n.IdKorisnik == user.Id).ToListAsync(),
                "Id", "Id", reklamacija.IdNarudzba);
            return View(reklamacija);
        }

        [Authorize(Roles = $"{Uloge.Zaposlenik},{Uloge.Administrator}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var reklamacija = await _context.Reklamacija.FindAsync(id);
            if (reklamacija == null) return NotFound();
            return View(reklamacija);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Uloge.Zaposlenik},{Uloge.Administrator}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,Odgovor")] Reklamacija model)
        {
            var reklamacija = await _context.Reklamacija.FindAsync(id);
            if (reklamacija == null) return NotFound();

            reklamacija.Status = model.Status;
            reklamacija.Odgovor = model.Odgovor;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Reklamacija ažurirana.";
            return RedirectToAction(nameof(Index));
        }
    }
}
