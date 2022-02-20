using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMovie.Models
{
    public class Grade
    {
        public int GradeId { get; set; }


        public double GradeNumber { get; set; }


        //User
        public string UserId { get; set; }


        //Movies
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
