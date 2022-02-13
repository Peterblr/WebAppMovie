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
    public class ActorsService : BaseRepository<Actor>, IActorsService
    {
        private readonly ApplicationDbContext _context;
        public ActorsService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        private List<Actor> DoSort(List<Actor> movies, string sortProperty, SortOrder sortOrder)
        {
            if (sortProperty.ToLower() == "fullname")
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    movies = movies.OrderBy(a => a.FullName).ToList();
                }
                else
                {
                    movies = movies.OrderByDescending(a => a.FullName).ToList();
                }
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    movies = movies.OrderBy(a => a.Biografy.Length).ToList();
                }
                else
                {
                    movies = movies.OrderByDescending(a => a.Biografy.Length).ToList();
                }
            }

            return movies;
        }

        public Task<PaginatedList<Actor>> GetAllActorsAsync(string sortProperty
            , SortOrder sortOrder
            , string searchText = ""
            , int pageIndex = 1
            , int pageSize = 3)
        {
            List<Actor> actors;

            if (!string.IsNullOrEmpty(searchText))
            {
                actors = _context.Actors.Where(n => n.FullName.Contains(searchText) || n.Biografy.Contains(searchText)).ToList();

            }
            else
            {
                actors = _context.Actors.ToList();
            }

            actors = DoSort(actors, sortProperty, sortOrder);

            PaginatedList<Actor> retActor = new(actors, pageIndex, pageSize);

            return Task.FromResult(retActor);
        }
    }
}
