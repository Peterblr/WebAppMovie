using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMovie.Data.ViewModels
{
    public class Pager
    {
        private const string action = "ListMovies";

        public int TotalItem { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public string Action { get; set; } = action;
        public string SearchText { get; set; }
        public string SortExpression { get; set; }


        public Pager(int totalItems, int currentPage, int pageSize = 5)
        {
            TotalItem = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;

            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            TotalPages = totalPages;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            if (TotalItem == 0)
            {
                StartPage = 0;
                CurrentPage = 0;
            }
            else
            {
                StartPage = startPage;
                EndPage = endPage;
            }
        }
    }
}
