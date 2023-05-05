using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerryMVC.Data;
using BerryMVC.Models;

namespace BerryMVC.Controllers
{
    public class BerryModelsController : Controller
    {
        private readonly BerryMVCContext _context;

        public BerryModelsController (BerryMVCContext context)
        {
            _context = context;
        }

        // GET: BerryModels
        public async Task<IActionResult> Index ()
        {
            return _context.BerryModel != null ?
                        View(await _context.BerryModel.ToListAsync()) :
                        Problem("Entity set 'BerryMVCContext.BerryModel'  is null.");
        }

        // GET: BerryModels/Details/5
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null || _context.BerryModel == null)
            {
                return NotFound();
            }

            var berryModel = await _context.BerryModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (berryModel == null)
            {
                return NotFound();
            }

            return View(berryModel);
        }

        // GET: BerryModels/Create
        public IActionResult Create ()
        {
            return View();
        }

        // POST: BerryModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind("ID,Name,Version,ContentsB64")] BerryModel berryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(berryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(berryModel);
        }

        // GET: BerryModels/Edit/5
        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null || _context.BerryModel == null)
            {
                return NotFound();
            }

            var berryModel = await _context.BerryModel.FindAsync(id);
            if (berryModel == null)
            {
                return NotFound();
            }
            return View(berryModel);
        }

        // POST: BerryModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind("ID,Name,Version,ContentsB64")] BerryModel berryModel)
        {
            if (id != berryModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(berryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BerryModelExists(berryModel.ID))
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
            return View(berryModel);
        }

        // GET: BerryModels/Delete/5
        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null || _context.BerryModel == null)
            {
                return NotFound();
            }

            var berryModel = await _context.BerryModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (berryModel == null)
            {
                return NotFound();
            }

            return View(berryModel);
        }

        // POST: BerryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            if (_context.BerryModel == null)
            {
                return Problem("Entity set 'BerryMVCContext.BerryModel'  is null.");
            }
            var berryModel = await _context.BerryModel.FindAsync(id);
            if (berryModel != null)
            {
                _context.BerryModel.Remove(berryModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BerryModelExists (int id)
        {
            return (_context.BerryModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
