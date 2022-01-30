using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data;
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

        public async Task AddNewMovieAsync(Movie data)
        {
            var newMovie = new Movie()
            {
                Title = data.Title,
                Description = data.Description,
                ImageUrl = data.ImageUrl,
                ReleaseDate = data.ReleaseDate,
                Rating = data.Rating,
                Genre = data.Genre,
                Producers = data.Producers,
                Actors = data.Actors,
                Scores = data.Scores,
                Comments = data.Comments
            };

            await _context.Movies.AddAsync(newMovie);

            ////Add Movie Actors
            //foreach (var actorId in data.Actors)
            //{
            //    var newActorMovie = new ActorMovie()
            //    {
            //        MovieId = newMovie.MovieId,
            //        ActorId = actorId
            //    };
            //    await _context. .Actors_Movies.AddAsync(newActorMovie);
            //}
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(a => a.Actors)
                .Include(p => p.Producers)
                .FirstOrDefaultAsync(n => n.MovieId == id);

            return movieDetails;
        }

        public async Task<Movie> GetMovieDropdownsValues()
        {
            var response = new Movie()
            {
                Actors = await _context.Actors.OrderBy(n => n.LastName).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.LastName).ToListAsync()
            };

            return response;
        }
    }
}
