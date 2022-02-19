using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMovie.Models
{
    public class Producer
    {
        public int ProducerId { get; set; }

        //[Required(ErrorMessage = "First Name is required")]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "First name cannot be longer than 50 characters and less 3.")]
        //[Column("FirstName")]
        //public string FirstName { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name cannot be longer than 50 characters and less 3.")]
        [Column("FullName")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DayOfBirth { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }

        public string Biografy { get; set; }

        //movies
        public virtual ICollection<Movie> Movies { get; set; }

        ////FullName
        //[Display(Name = "Full Name")]
        //public string FullName
        //{
        //    get
        //    {
        //        return FirstName + " " + LastName;
        //    }
        //}
    }
}
