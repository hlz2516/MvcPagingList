using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPagingListDesign.Models
{
    public class StuContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public StuContext(DbContextOptions<StuContext> opt) : base(opt)
        {

        }
    }
}
