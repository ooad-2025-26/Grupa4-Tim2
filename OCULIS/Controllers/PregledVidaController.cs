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
    public class PregledVidaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PregledVidaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PregledVida
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PregledVida.Include(p => p.ElektronskiKarton);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PregledVida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregledVida = await _context.PregledVida
                .Include(p => p.ElektronskiKarton)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pregledVida == null)
            {
                return NotFound();
            }

            return View(pregledVida);
        }

        // GET: PregledVida/Create
        public IActionResult Create()
        {
            ViewData["IdElektronskiKarton"] = new SelectList(_context.ElektronskiKarton, "Id", "Id");
            return View();
        }

        // POST: PregledVida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DatumPregleda,DioptrijaLijevo,DioptrijaDesno,Preporuka,IdElektronskiKarton")] PregledVida pregledVida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pregledVida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdElektronskiKarton"] = new SelectList(_context.ElektronskiKarton, "Id", "Id", pregledVida.IdElektronskiKarton);
            return View(pregledVida);
        }

        // GET: PregledVida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregledVida = await _context.PregledVida.FindAsync(id);
            if (pregledVida == null)
            {
                return NotFound();
            }
            ViewData["IdElektronskiKarton"] = new SelectList(_context.ElektronskiKarton, "Id", "Id", pregledVida.IdElektronskiKarton);
            return View(pregledVida);
        }

        // POST: PregledVida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DatumPregleda,DioptrijaLijevo,DioptrijaDesno,Preporuka,IdElektronskiKarton")] PregledVida pregledVida)
        {
            if (id != pregledVida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pregledVida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PregledVidaExists(pregledVida.Id))
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
            ViewData["IdElektronskiKarton"] = new SelectList(_context.ElektronskiKarton, "Id", "Id", pregledVida.IdElektronskiKarton);
            return View(pregledVida);
        }

        // GET: PregledVida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregledVida = await _context.PregledVida
                .Include(p => p.ElektronskiKarton)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pregledVida == null)
            {
                return NotFound();
            }

            return View(pregledVida);
        }

        // POST: PregledVida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pregledVida = await _context.PregledVida.FindAsync(id);
            if (pregledVida != null)
            {
                _context.PregledVida.Remove(pregledVida);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PregledVidaExists(int id)
        {
            return _context.PregledVida.Any(e => e.Id == id);
        }
    }
}
