using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data.Enums;

namespace WebAppMovie.Models
{
    public class Movie
    {
        public int MovieId { get; set; }



        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title"), StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }

        [StringLength(1500, MinimumLength = 3, ErrorMessage = "Descriptipn cannot be longer than 1500 characters and less 3.")]
        public string Description { get; set; }

        [Display(Name = "Release Date"), DataType(DataType.Date)]
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

        ////score
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
