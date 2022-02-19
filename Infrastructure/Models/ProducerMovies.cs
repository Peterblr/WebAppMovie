using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMovie.Models
{
    public class ProducerMovies
    {
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }


        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
