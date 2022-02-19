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
            SelectedProducers = new List<Producer>();

            SelectedActors = new List<Actor>();
        }

        public List<Producer> SelectedProducers { get; set; }
        public List<Actor> SelectedActors { get; set; }
    }
}
