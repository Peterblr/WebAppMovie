using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMovie.Models
{
    public class Producer
    {
        public int ProducerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public string Biografy { get; set; }

        //movies
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
