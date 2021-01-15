using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StreamingPlataforms.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StreamingPlataforms.Models.Authentication;

namespace StreamingPlataforms.Contexts
{
    public class PlataformDbContext : IdentityDbContext
    {
        public PlataformDbContext(DbContextOptions<PlataformDbContext> options) : base(options)
        {

        }
        public DbSet<PlataformEntity> Plataform{ get; set; }
        public DbSet<SerieEntity> Series { get; set; }
        public DbSet<AppUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlataformEntity>().ToTable("plataform");
            modelBuilder.Entity<PlataformEntity>().HasMany(d => d.Series).WithOne(c => c.Plataform);
            modelBuilder.Entity<PlataformEntity>().Property(d => d.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<SerieEntity>().ToTable("series");
            modelBuilder.Entity<SerieEntity>().HasOne(c => c.Plataform).WithMany(d => d.Series);
            modelBuilder.Entity<SerieEntity>().Property(c => c.PlataformId).ValueGeneratedOnAdd();
        }
    }
}
