using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCULIS.Data;
using OCULIS.Models;

namespace OCULIS.Controllers
{
    public class ElektronskiKartonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ElektronskiKartonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ElektronskiKarton
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ElektronskiKarton.Include(e => e.Korisnik);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ElektronskiKarton/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elektronskiKarton = await _context.ElektronskiKarton
                .Include(e => e.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (elektronskiKarton == null)
            {
                return NotFound();
            }

            return View(elektronskiKarton);
        }

        // GET: ElektronskiKarton/Create
        public IActionResult Create()
        {
            ViewData["IdKorisnik"] = new SelectList(_context.Korisnik, "Id", "Id");
            return View();
        }

        // POST: ElektronskiKarton/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DatumKreiranja,Napomena,IdKorisnik")] ElektronskiKarton elektronskiKarton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(elektronskiKarton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKorisnik"] = new SelectList(_context.Korisnik, "Id", "Id", elektronskiKarton.IdKorisnik);
            return View(elektronskiKarton);
        }

        // GET: ElektronskiKarton/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elektronskiKarton = await _context.ElektronskiKarton.FindAsync(id);
            if (elektronskiKarton == null)
            {
                return NotFound();
            }
            ViewData["IdKorisnik"] = new SelectList(_context.Korisnik, "Id", "Id", elektronskiKarton.IdKorisnik);
            return View(elektronskiKarton);
        }

        // POST: ElektronskiKarton/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DatumKreiranja,Napomena,IdKorisnik")] ElektronskiKarton elektronskiKarton)
        {
            if (id != elektronskiKarton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(elektronskiKarton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElektronskiKartonExists(elektronskiKarton.Id))
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
            ViewData["IdKorisnik"] = new SelectList(_context.Korisnik, "Id", "Id", elektronskiKarton.IdKorisnik);
            return View(elektronskiKarton);
        }

        // GET: ElektronskiKarton/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elektronskiKarton = await _context.ElektronskiKarton
                .Include(e => e.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (elektronskiKarton == null)
            {
                return NotFound();
            }

            return View(elektronskiKarton);
        }

        // POST: ElektronskiKarton/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var elektronskiKarton = await _context.ElektronskiKarton.FindAsync(id);
            if (elektronskiKarton != null)
            {
                _context.ElektronskiKarton.Remove(elektronskiKarton);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElektronskiKartonExists(int id)
        {
            return _context.ElektronskiKarton.Any(e => e.Id == id);
        }
    }
}
