using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using WebAppMovie.Data;
using WebAppMovie.Models;
using WebAppMovie.Repository.Interfaces;
using NToastNotify;
using Microsoft.AspNetCore.Authorization;

namespace WebAppMovie.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _service;

        private readonly IProducerRepository _producer;

        private readonly IToastNotification _toastNotification;


        public MoviesController(IMoviesRepository service, IToastNotification toastNotification, IProducerRepository producer)
        {
            _service = service;

            _toastNotification = toastNotification;

            _producer = producer;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var movies = await _service.GetAllAsync(x => x.Actors);

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => string.Equals(s.Title, searchString, StringComparison.CurrentCultureIgnoreCase));
            }

            movies = sortOrder switch
            {
                "name_desc" => movies.OrderByDescending(s => s.Title),
                "Date" => movies.OrderBy(s => s.ReleaseDate),
                "date_desc" => movies.OrderByDescending(s => s.ReleaseDate),
                _ => movies.OrderBy(s => s.Title),
            };

            int pageSize = 3;

            int pageNumber = (page ?? 1);

            return View(movies.ToPagedList(pageNumber, pageSize));
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return View("NotFound");
            }

            return View(movie);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetMovieDropdownsValues();

            var producers = _producer.GetAllAsync(x => x.Movies);


            ViewData["ProducerId"] = new SelectList(await producers, "ProducerId", "FullName");



            ViewBag.Producers = new MultiSelectList(movieDropdownsData.SelectedProducers, "ProducerId", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.SelectedActors, "ActorId", "FullName");

            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,ImageUrl,Description,ReleaseDate,Genre,Ratin,ActorId,ProducerId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(movie);

                _toastNotification.AddSuccessToastMessage("Movie created");

                return RedirectToAction(nameof(Index));
            }
            var movieDropdownsData = await _service.GetMovieDropdownsValues();

            ViewBag.Producers = new MultiSelectList(movieDropdownsData.SelectedProducers, "ProducerId", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.SelectedActors, "ActorId", "FullName");

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
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,ImageUrl,Description,ReleaseDate,Genre,Rating,ActorId,ProducerId")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(movie);

                _toastNotification.AddSuccessToastMessage("Movie Updated");

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

            _toastNotification.AddAlertToastMessage("Movie Deleted");

            return RedirectToAction(nameof(Index));
        }
    }
}
