using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMovie.Data;
using WebAppMovie.Models;

namespace WebAppMovie.Controllers
{
    public class CollectionMoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollectionMoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CollectionMovies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CollectionMovies.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CollectionMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionMovies = await _context.CollectionMovies
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CollectionMoviesId == id);
            if (collectionMovies == null)
            {
                return NotFound();
            }

            return View(collectionMovies);
        }

        // GET: CollectionMovies/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: CollectionMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CollectionMoviesId,UserId,IsPublic")] CollectionMovies collectionMovies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collectionMovies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", collectionMovies.UserId);
            return View(collectionMovies);
        }

        // GET: CollectionMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionMovies = await _context.CollectionMovies.FindAsync(id);
            if (collectionMovies == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", collectionMovies.UserId);
            return View(collectionMovies);
        }

        // POST: CollectionMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CollectionMoviesId,UserId,IsPublic")] CollectionMovies collectionMovies)
        {
            if (id != collectionMovies.CollectionMoviesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collectionMovies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionMoviesExists(collectionMovies.CollectionMoviesId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", collectionMovies.UserId);
            return View(collectionMovies);
        }

        // GET: CollectionMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionMovies = await _context.CollectionMovies
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CollectionMoviesId == id);
            if (collectionMovies == null)
            {
                return NotFound();
            }

            return View(collectionMovies);
        }

        // POST: CollectionMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collectionMovies = await _context.CollectionMovies.FindAsync(id);
            _context.CollectionMovies.Remove(collectionMovies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionMoviesExists(int id)
        {
            return _context.CollectionMovies.Any(e => e.CollectionMoviesId == id);
        }
    }
}
