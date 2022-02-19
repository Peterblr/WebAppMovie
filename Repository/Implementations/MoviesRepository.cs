using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data;
using WebAppMovie.Data.Enums;
using WebAppMovie.Data.ViewModels;
using WebAppMovie.Models;
using WebAppMovie.Repository.Base;
using WebAppMovie.Repository.Interfaces;

namespace WebAppMovie.Repository.Implementations
{
    public class MoviesRepository : BaseRepository<Movie>, IMoviesRepository
    {
        private readonly ApplicationDbContext _context;

        public MoviesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        private static List<Movie> DoSort(List<Movie> movies, string sortProperty, SortOrder sortOrder)
        {
            if (sortProperty.ToLower() == "title")
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    movies = movies.OrderBy(a => a.Title).ToList();
                }
                else
                {
                    movies = movies.OrderByDescending(a => a.Title).ToList();
                }
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    movies = movies.OrderBy(a => a.Description.Length).ToList();
                }
                else
                {
                    movies = movies.OrderByDescending(a => a.Description.Length).ToList();
                }
            }

            return movies;
        }

        public async Task<PaginatedList<Movie>> GetAllMoviesAsync(string sortProperty
            , SortOrder sortOrder
            , string searchText = ""
            , int pageIndex = 1
            , int pageSize = 3)
        {
            List<Movie> movies = await _context.Movies.ToListAsync();

            if (!string.IsNullOrEmpty(searchText))
            {
                movies = _context.Movies.Where(n => n.Title.Contains(searchText) || n.Description.Contains(searchText))
                    .Include(a => a.Actors)
                    .Include(p => p.Producers)
                    .ToList();
            }
            else
            {
                movies = _context.Movies.Include(a => a.Actors).Include(p => p.Producers).ToList();
            }

            movies = DoSort(movies, sortProperty, sortOrder);

            PaginatedList<Movie> retMovie = new(movies, pageIndex, pageSize);

            return retMovie;
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
                SelectedActors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                SelectedProducers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task AddNewMovieAsync(NewMovieViewModel data)
        {
            var newMovie = new Movie()
            {
                Title = data.Title,
                ImageUrl = data.ImageUrl,
                Description = data.Description,
                ReleaseDate = data.ReleaseDate,
                Genre = data.Genre,
                Rating = data.Rating,
                Actors = data.ActorsMovie,
                Comments = data.CommentsMovie,
            };

            await _context.Movies.AddAsync(newMovie);

            await _context.SaveChangesAsync();

            foreach (var producer in data.ProducersMovieId)
            {
                var newProducerMovies = new ProducerMovies()
                {
                    MovieId = newMovie.MovieId,
                    ProducerId = producer
                };

                await _context.AddAsync(newProducerMovies);
            }

            await _context.SaveChangesAsync();
        }
    }
}
