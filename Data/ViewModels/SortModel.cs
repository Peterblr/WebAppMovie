using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data.Enums;

namespace WebAppMovie.Data.ViewModels
{
    public class SortModel
    {
        public string SortedProperty { get; set; }

        public SortOrder SortedOrder { get; set; }
    }
}
