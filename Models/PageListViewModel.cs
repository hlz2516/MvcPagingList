using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPagingListDesign.Models
{
    public class PageListViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
    }
}
