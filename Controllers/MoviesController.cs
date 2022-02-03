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

namespace WebAppMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        private readonly IToastNotification _toastNotification;

        private readonly IProducerService _producer;

        //private readonly IActorsService _actorService;

        public MoviesController(IMoviesService service, IToastNotification toastNotification, IProducerService producer)
        {
            _service = service;

            _toastNotification = toastNotification;

            _producer = producer;
        }
        //public MoviesController(IActorsService actorService)
        //{
        //    _actorService = actorService;
        //}

        //GET: Movies
        //public async Task<IActionResult> Index() => View(await _service.GetAllAsync(x => x.Actors));

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

            //return View(movies);
        }



        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //var movie = await _service.GetByIdAsync(id);
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

            //var producerTest = _producer.GetAllAsync();


            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "ProducerId", "LastName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "ActorId", "LastName");

            //ViewBag.ProducerTest = new SelectList(producerTest.Result);

            //var list = new List<string>() { "one", "two", "three" };

            //ViewBag.list = list;

            //ViewBag.ActorId = new SelectList(_actorService.GetAllAsync());
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
                //await _service.AddNewMovieAsync(movie);

                await _service.AddAsync(movie);

                _toastNotification.AddSuccessToastMessage("Actor created");

                return RedirectToAction(nameof(Index));
            }
            var movieDropdownsData = await _service.GetMovieDropdownsValues();

            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "ProducerId", "LastName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "ActorId", "LastName");

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

                //await _service.SaveAsync();

                _toastNotification.AddSuccessToastMessage("Actor created");

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

            //await _service.SaveAsync();

            _toastNotification.AddAlertToastMessage("Actor deleted");

            return RedirectToAction(nameof(Index));
        }
    }
}
