using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPagingListDesign.Models
{
    public class Student
    {
        [Key]
        public string Number { get; set; }
        public string Name { get; set; }
    }
}
