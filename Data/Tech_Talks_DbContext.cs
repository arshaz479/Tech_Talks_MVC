using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tech_Talks_MVC.Models;

namespace Tech_Talks_MVC.Data
{
    public class Tech_Talks_DbContext : DbContext
    {
        public Tech_Talks_DbContext (DbContextOptions<Tech_Talks_DbContext> options)
            : base(options)
        {
        }

        public DbSet<Tech_Talks_MVC.Models.Discussion> Discussion { get; set; }

        public DbSet<Tech_Talks_MVC.Models.Schedule> Schedule { get; set; }

        public DbSet<Tech_Talks_MVC.Models.Speaker> Speaker { get; set; }

        public DbSet<Tech_Talks_MVC.Models.Sponsor> Sponsor { get; set; }
    }
}
