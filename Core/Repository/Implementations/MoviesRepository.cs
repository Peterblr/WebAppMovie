﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data;
using WebAppMovie.Data.Enums;
using WebAppMovie.Data.ViewModels;
using WebAppMovie.Infrastructure.Models;
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

        public async Task<PaginatedList<Movie>> GetAllMoviesPagerAsync(string sortProperty
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
                .Include(g => g.Grades)
                .Include(c => c.Comments)
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

            foreach (var actors in data.ActorsMovieId)
            {
                var newActorMovies = new ActorMovies()
                {
                    MovieId = newMovie.MovieId,
                    ActorId = actors
                };

                await _context.AddAsync(newActorMovies);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateNewMovieAsync(NewMovieViewModel data)
        {
            var updateMovie = await _context.Movies.FindAsync(data.NewMovieId);
            //var updateMovie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == data.NewMovieId);

            //updateMovie.Producers.Remove(await _context.Producers.FindAsync(data.ProducersMovieId));

            if (updateMovie != null)
            {
                updateMovie.Title = data.Title;
                updateMovie.ImageUrl = data.ImageUrl;
                updateMovie.Description = data.Description;
                updateMovie.ReleaseDate = data.ReleaseDate;
                updateMovie.Genre = data.Genre;
                updateMovie.Rating = data.Rating;


                var listProd = new ProducerMovies().Producer.FullName;

                //var showProd = listProd.Producer;

                foreach (var producer in data.ProducersMovieId)
                {
                    var newProducerMovies = new ProducerMovies()
                    {
                        MovieId = updateMovie.MovieId,
                        ProducerId = producer
                    };

                }

                // Remove deselected producers
                //updateMovie.Producers.Where(m => data.ProducersMovieId.Contains(m.ProducerId))
                //    .ToList().ForEach(producer => updateMovie.Producers.Remove(producer));

                // Add new producers
                //var updateProducer = updateMovie.Producers.Select(m => m.ProducerId);
                //_context.Movies.Where(m => data.ProducersMovieId.Except(updateProducer).Contains(m.MovieId))
                //    .ToList().ForEach(producer => updateMovie.Producers.Add(producer));

                await _context.SaveChangesAsync();
            }
        }
    }
}
