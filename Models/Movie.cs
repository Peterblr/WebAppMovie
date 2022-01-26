using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data.Enums;

namespace WebAppMovie.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }

        //Genre
        public Genre Genre { get; set; }

        //Rating
        public Rating Rating { get; set; }

        //actors
        public virtual ICollection<Actor> Actors { get; set; }

        //producers
        public virtual ICollection<Producer> Producers { get; set; }

        //comments        
        public virtual ICollection<Comment> Comments { get; set; }

        //score
        public virtual ICollection<Score> Scores { get; set; }
    }
}
