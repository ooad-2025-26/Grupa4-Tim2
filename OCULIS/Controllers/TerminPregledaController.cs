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
    public class TerminPregledaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TerminPregledaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TerminPregleda
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TerminPregleda.Include(t => t.Korisnik).Include(t => t.Poslovnica);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TerminPregleda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminPregleda = await _context.TerminPregleda
                .Include(t => t.Korisnik)
                .Include(t => t.Poslovnica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (terminPregleda == null)
            {
                return NotFound();
            }

            return View(terminPregleda);
        }

        // GET: TerminPregleda/Create
        public IActionResult Create()
        {
            ViewData["IdKorisnik"] = new SelectList(_context.Korisnik, "Id", "Id");
            ViewData["IdPoslovnica"] = new SelectList(_context.Poslovnica, "Id", "Id");
            return View();
        }

        // POST: TerminPregleda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Datum,Vrijeme,Status,IdPoslovnica,IdKorisnik")] TerminPregleda terminPregleda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(terminPregleda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKorisnik"] = new SelectList(_context.Korisnik, "Id", "Id", terminPregleda.IdKorisnik);
            ViewData["IdPoslovnica"] = new SelectList(_context.Poslovnica, "Id", "Id", terminPregleda.IdPoslovnica);
            return View(terminPregleda);
        }

        // GET: TerminPregleda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminPregleda = await _context.TerminPregleda.FindAsync(id);
            if (terminPregleda == null)
            {
                return NotFound();
            }
            ViewData["IdKorisnik"] = new SelectList(_context.Korisnik, "Id", "Id", terminPregleda.IdKorisnik);
            ViewData["IdPoslovnica"] = new SelectList(_context.Poslovnica, "Id", "Id", terminPregleda.IdPoslovnica);
            return View(terminPregleda);
        }

        // POST: TerminPregleda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Datum,Vrijeme,Status,IdPoslovnica,IdKorisnik")] TerminPregleda terminPregleda)
        {
            if (id != terminPregleda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(terminPregleda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminPregledaExists(terminPregleda.Id))
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
            ViewData["IdKorisnik"] = new SelectList(_context.Korisnik, "Id", "Id", terminPregleda.IdKorisnik);
            ViewData["IdPoslovnica"] = new SelectList(_context.Poslovnica, "Id", "Id", terminPregleda.IdPoslovnica);
            return View(terminPregleda);
        }

        // GET: TerminPregleda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminPregleda = await _context.TerminPregleda
                .Include(t => t.Korisnik)
                .Include(t => t.Poslovnica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (terminPregleda == null)
            {
                return NotFound();
            }

            return View(terminPregleda);
        }

        // POST: TerminPregleda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var terminPregleda = await _context.TerminPregleda.FindAsync(id);
            if (terminPregleda != null)
            {
                _context.TerminPregleda.Remove(terminPregleda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminPregledaExists(int id)
        {
            return _context.TerminPregleda.Any(e => e.Id == id);
        }
    }
}
