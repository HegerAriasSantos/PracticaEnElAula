using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrácticaEnElAula.Contexts2;
using PrácticaEnElAula.Models;

namespace PrácticaEnElAula.Controllers
{
    public class VacationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vacation.Include(v => v.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacations = await _context.Vacation
                .Include(v => v.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacations == null)
            {
                return NotFound();
            }

            return View(vacations);
        }

        public IActionResult Create()
        {
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdEmployee,FechaDeSalida,FechaDeIngreso")] Vacations vacations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Id", "Nombre", vacations.IdEmployee);
            return View(vacations);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacations = await _context.Vacation.FindAsync(id);
            if (vacations == null)
            {
                return NotFound();
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Id", "Nombre", vacations.IdEmployee);
            return View(vacations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEmployee,FechaDeSalida,FechaDeIngreso")] Vacations vacations)
        {
            if (id != vacations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationsExists(vacations.Id))
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
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Id", "Id", vacations.IdEmployee);
            return View(vacations);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacations = await _context.Vacation
                .Include(v => v.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacations == null)
            {
                return NotFound();
            }

            return View(vacations);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacations = await _context.Vacation.FindAsync(id);
            _context.Vacation.Remove(vacations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationsExists(int id)
        {
            return _context.Vacation.Any(e => e.Id == id);
        }
    }
}
