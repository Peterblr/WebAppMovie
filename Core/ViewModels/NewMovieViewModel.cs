using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data.Enums;
using WebAppMovie.Models;

namespace WebAppMovie.Data.ViewModels
{
    public class NewMovieViewModel
    {
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

        //Grade
        //public List<double> Grade { get; set; }


        //actors
        public List<int> ActorsMovieId { get; set; }


        //producers
        public List<int> ProducersMovieId { get; set; }


        //comments        
        public List<Comment> CommentsMovie { get; set; }
    }
}
