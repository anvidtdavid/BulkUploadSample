using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkUploadSample.Model
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            :base(options)
        {
        }

        public DemoDbContext()
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
