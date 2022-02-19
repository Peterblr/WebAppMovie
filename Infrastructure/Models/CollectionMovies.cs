using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMovie.Models
{
    public class CollectionMovies
    {
        public int CollectionMoviesId { get; set; }

        //public int UserId { get; set; }

        //public User User { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public bool IsPublic { get; set; }
    }
}
