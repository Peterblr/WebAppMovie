﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMovie.Models
{
    public class Actor
    {
        public int ActorId { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name cannot be longer than 50 characters and less 3.")]
        [Column("FullName")]
        public string FullName { get; set; }

        [Display(Name = "Date Of Birth"), DataType(DataType.Date)]
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
