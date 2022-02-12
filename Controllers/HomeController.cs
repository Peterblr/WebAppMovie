using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Models;
using WebAppMovie.Repository.Interfaces;

namespace WebAppMovie.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMoviesService _service;

        public HomeController(IMoviesService service)
        {
            _service = service;
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
    }
}
