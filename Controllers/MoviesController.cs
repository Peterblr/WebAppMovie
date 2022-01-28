using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMovie.Data;
using WebAppMovie.Models;
using WebAppMovie.Repository.Interfaces;

namespace WebAppMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }

        //GET: Movies
        //public async Task<IActionResult> Index() => View(await _service.GetAllAsync(x => x.Actors));

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = (IQueryable<string>)await _service.GetAllAsync(x => x.Genre.ToString());

            var movies = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => string.Equals(s.Title, searchString, StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => string.Equals(x.Genre., movieGenre, StringComparison.CurrentCultureIgnoreCase));
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = (List<Movie>)movies
            };

            return View(movieGenreVM);
        }



        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetByIdAsync(id);

            if (movie == null)
            {
                return View("NotFound");
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,ImageUrl,Description,ReleaseDate,Genre,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(movie);

                await _service.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _service.GetByIdAsync(id);

            if (movie == null)
            {
                return View("NotFound");
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,ImageUrl,Description,ReleaseDate,Genre,Rating")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(movie);

                await _service.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _service.GetByIdAsync(id);

            if (movie == null)
            {
                return View("NotFound");
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _service.GetByIdAsync(id);

            await _service.DeleteAsync(id);

            await _service.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
