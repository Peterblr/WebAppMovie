using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data;
using WebAppMovie.Models;
using WebAppMovie.Repository.Base;
using WebAppMovie.Repository.Interfaces;

namespace WebAppMovie.Repository.Implementations
{
    public class ProducerRepository : BaseRepository<Producer>, IProducerRepository
    {
        public ProducerRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
