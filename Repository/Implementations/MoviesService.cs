using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data;
using WebAppMovie.Data.ViewModels;
using WebAppMovie.Models;
using WebAppMovie.Repository.Base;
using WebAppMovie.Repository.Interfaces;

namespace WebAppMovie.Repository.Implementations
{
    public class MoviesService : BaseRepository<Movie>, IMoviesService
    {
        private readonly ApplicationDbContext _context;

        public MoviesService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(a => a.Actors)
                .Include(p => p.Producers)
                .FirstOrDefaultAsync(n => n.MovieId == id);

            return movieDetails;
        }

        public async Task<NewMovieDropdown> GetMovieDropdownsValues()
        {
            var response = new NewMovieDropdown()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }
    }
}
