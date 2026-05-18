using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCULIS.Models;

namespace OCULIS.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly UserManager<Korisnik> _userManager;

        public KorisnikController(UserManager<Korisnik> userManager)
        {
            _userManager = userManager;
        }

        // GET: Korisnik
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        // GET: Korisnik/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _userManager.FindByIdAsync(id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // GET: Korisnik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Korisnik/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Korisnik korisnik, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(korisnik, password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(korisnik);
        }

        // GET: Korisnik/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _userManager.FindByIdAsync(id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Korisnik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Korisnik korisnik)
        {
            if (id != korisnik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(id);

                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.Ime = korisnik.Ime;
                existingUser.Prezime = korisnik.Prezime;
                existingUser.Email = korisnik.Email;
                existingUser.UserName = korisnik.Email;
                existingUser.PhoneNumber = korisnik.PhoneNumber;

                var result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(korisnik);
        }

        // GET: Korisnik/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _userManager.FindByIdAsync(id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Korisnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var korisnik = await _userManager.FindByIdAsync(id);

            if (korisnik != null)
            {
                await _userManager.DeleteAsync(korisnik);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }
    }
}