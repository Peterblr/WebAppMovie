using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data;
using WebAppMovie.Data.ViewModels;

namespace WebAppMovie.API.Controllers
{
    public class ExampleController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExampleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var item = _context.Producers.ToList();

            NewMovieViewModel newMovie = new NewMovieViewModel();

            newMovie.ProducersMovie = (List<Models.Producer>)item.Select(a => new SelectListItem()
            {
                Value = a.ProducerId.ToString(),
                Text = a.FullName
            });

            return View(newMovie);
        }
    }
}
