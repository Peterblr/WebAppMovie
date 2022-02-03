using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data.ViewModels;
using WebAppMovie.Models;
using WebAppMovie.Repository.Base;

namespace WebAppMovie.Repository.Interfaces
{
    public interface IMoviesService : IBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdown> GetMovieDropdownsValues();

        //Task AddNewMovieAsync(Movie data);
    }
}
