using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMovie.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentItem { get; set; }

        //user
        public int UserId { get; set; }
        public User User { get; set; }

        //movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
