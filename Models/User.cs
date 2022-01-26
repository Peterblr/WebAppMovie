using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMovie.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string Username { get; set; }
        public string? ImageUrl { get; set; }
        public string Email { get; set; }

        //Role
        public int RoleId { get; set; }
        public Role Role { get; set; }

        //Collection Movies
        public virtual ICollection<CollectionMovies> CollectionMovies { get; set; }

        //Comments
        public virtual ICollection<Comment> Comments { get; set; }

        //scores
        public virtual ICollection<Score> Scores { get; set; }
    }
}
