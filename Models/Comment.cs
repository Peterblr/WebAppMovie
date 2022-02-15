using Microsoft.AspNetCore.Identity;
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
        public string UserId { get; set; }

        //movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
