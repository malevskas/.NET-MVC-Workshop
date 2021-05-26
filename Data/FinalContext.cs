using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Final.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Final.Areas.Identity.Data;

namespace Final.Data
{
    public class FinalContext : IdentityDbContext<FinalUser>
    {
        public FinalContext (DbContextOptions<FinalContext> options)
            : base(options)
        {
        }

        public DbSet<Final.Models.Course> Course { get; set; }

        public DbSet<Final.Models.Student> Student { get; set; }

        public DbSet<Final.Models.Teacher> Teacher { get; set; }

        public DbSet<Final.Models.Enrollment> Enrollment { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
