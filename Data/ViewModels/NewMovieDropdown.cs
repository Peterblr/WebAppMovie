using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Models;

namespace WebAppMovie.Data.ViewModels
{
    public class NewMovieDropdown
    {
        public NewMovieDropdown()
        {
            Producers = new List<Producer>();

            Actors = new List<Actor>();
        }

        public List<Producer> Producers { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
