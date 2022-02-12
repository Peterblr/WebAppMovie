using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMovie.Data;
using WebAppMovie.Models;
using WebAppMovie.Repository.Interfaces;

namespace WebAppMovie.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class ManagerController : Controller
    {
        private readonly IMoviesService _serviceMovie;
        private readonly IActorsService _serviceActor;
        private readonly IProducerService _serviceProducer;

        public ManagerController(IMoviesService serviceMovie, IActorsService serviceActor, IProducerService serviceProducer)
        {
            _serviceMovie = serviceMovie;
            _serviceActor = serviceActor;
            _serviceProducer = serviceProducer;
        }

        // GET: Manager
        public async Task<IActionResult> ListMovies()
        {
            return View(await _serviceMovie.GetAllAsync());
        }

        public async Task<IActionResult> ListActors()
        {
            return View(await _serviceActor.GetAllAsync());
        }

        public async Task<IActionResult> ListProducers()
        {
            return View(await _serviceProducer.GetAllAsync());
        }

        //// GET: Manager/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movies
        //        .FirstOrDefaultAsync(m => m.MovieId == id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movie);
        //}

        //// GET: Manager/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Manager/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("MovieId,Title,ImageUrl,Description,ReleaseDate,Genre,Rating")] Movie movie)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(movie);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(movie);
        //}

        //// GET: Manager/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movies.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(movie);
        //}

        //// POST: Manager/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,ImageUrl,Description,ReleaseDate,Genre,Rating")] Movie movie)
        //{
        //    if (id != movie.MovieId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(movie);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MovieExists(movie.MovieId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(movie);
        //}

        //// GET: Manager/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movies
        //        .FirstOrDefaultAsync(m => m.MovieId == id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movie);
        //}

        //// POST: Manager/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var movie = await _context.Movies.FindAsync(id);
        //    _context.Movies.Remove(movie);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MovieExists(int id)
        //{
        //    return _context.Movies.Any(e => e.MovieId == id);
        //}
    }
}
