using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Final.Models;

namespace Final.Data
{
    public class FinalContext : DbContext
    {
        public FinalContext (DbContextOptions<FinalContext> options)
            : base(options)
        {
        }

        public DbSet<Final.Models.Course> Course { get; set; }

        public DbSet<Final.Models.Student> Student { get; set; }

        public DbSet<Final.Models.Teacher> Teacher { get; set; }

        public DbSet<Final.Models.Enrollment> Enrollment { get; set; }
    }
}
