using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MateAPI.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<user> Users { get; set; }

        public DbSet<Tmate> Tmates { get; set; }
        public DbSet<Shop> Shops { get; set; }

        public DbSet<Service> Services { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
