using ETG_TASK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETG_TASK.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}
