using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMovie.Data;
using WebAppMovie.Data.Enums;
using WebAppMovie.Data.ViewModels;
using WebAppMovie.Models;
using WebAppMovie.Repository.Interfaces;

namespace WebAppMovie.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class ManagerController : Controller
    {
        private readonly IMoviesRepository _serviceMovie;
        private readonly IActorsRepository _serviceActor;
        private readonly IProducerRepository _serviceProducer;

        public ManagerController(IMoviesRepository serviceMovie, IActorsRepository serviceActor, IProducerRepository serviceProducer)
        {
            _serviceMovie = serviceMovie;
            _serviceActor = serviceActor;
            _serviceProducer = serviceProducer;
        }


        //private async Task<SelectList> GetActors()
        //{
        //    PaginatedList<Actor> actors = await _serviceActor.GetAllActorsAsync("FullName", SortOrder.Ascending, "", 1, 100);

        //    var listActors = new SelectList(actors, "ActorId", "FullName");

        //    return listActors;
        //}

        //GET: Manager
        public async Task<IActionResult> ListMovies(string sortExpression = "", string searchText = "", int pg = 1, int pageSize = 3)
        {
            PaginatedList<Movie> movies;
            SortModel sortModel = new();

            sortModel.AddColumn("title");
            sortModel.AddColumn("description");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.searchText = searchText;

            movies = await _serviceMovie.GetAllMoviesAsync(sortModel.SortedProperty, sortModel.SortedOrder, searchText, pg, pageSize);

            var pager = new Pager(movies.TotalRecords, pg, pageSize)
            {
                SortExpression = sortExpression
            };

            ViewBag.Pager = pager;

            return View(movies);
        }

        //public async Task<IActionResult> ListActors(string sortExpression = "", string searchText = "", int pg = 1, int pageSize = 3)
        //{
        //    PaginatedList<Actor> actors;
        //    SortModel sortModel = new();

        //    sortModel.AddColumn("fullname");
        //    sortModel.ApplySort(sortExpression);
        //    ViewData["sortModel"] = sortModel;

        //    ViewBag.searchText = searchText;

        //    actors = await _serviceActor.GetAllActorsAsync(sortModel.SortedProperty, sortModel.SortedOrder, searchText, pg, pageSize);

        //    var pager = new Pager(actors.TotalRecords, pg, pageSize)
        //    {
        //        SortExpression = sortExpression
        //    };

        //    ViewBag.Pager = pager;

        //    return View(actors);
        //}

        public async Task<IActionResult> ListActors()
        {
            return View(await _serviceActor.GetAllAsync());
        }

        public async Task<IActionResult> ListProducers()
        {
            return View(await _serviceProducer.GetAllAsync());
        }

        // GET: Manager/Create
        public IActionResult CreateMovie()
        {
            Movie movie = new();

            return View(movie);
        }

        // POST: Manager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovie([Bind("MovieId,Title,ImageUrl,Description,ReleaseDate,Genre,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _serviceMovie.AddAsync(movie);
                return RedirectToAction(nameof(ListMovies));
            }
            return View(movie);
        }

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
