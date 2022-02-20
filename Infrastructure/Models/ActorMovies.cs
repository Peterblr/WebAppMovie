using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Models;

namespace WebAppMovie.Infrastructure.Models
{
    public class ActorMovies
    {
        public int ActorId { get; set; }
        public Actor Actor { get; set; }


        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
