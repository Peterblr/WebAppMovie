using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data.Enums;
using WebAppMovie.Data.ViewModels;
using WebAppMovie.Models;
using WebAppMovie.Repository.Base;

namespace WebAppMovie.Repository.Interfaces
{
    public interface IActorsService : IBaseRepository<Actor>
    {
        Task<PaginatedList<Actor>> GetAllActorsAsync(string sortProperty
            , SortOrder sortOrder
            , string searchText = ""
            , int pageIndex = 1
            , int pageSize = 3);
    }
}
