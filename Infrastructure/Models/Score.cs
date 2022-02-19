using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMovie.Models
{
    public class Score
    {
        public int ScoreId { get; set; }
        public double ScoreNumber { get; set; }
        //User
        //public int UserId { get; set; }
        //Movies
        public int MovieId { get; set; }
    }
}
