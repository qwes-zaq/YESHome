using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using YESHome.Data.Models;

namespace YesHome.Data
{
    public class YESHomeDb : IdentityDbContext<User>
    {
        public DbSet<Place> Places { get; set; }
        public DbSet<Report> Reports { get; set; }
        public YESHomeDb(DbContextOptions<YESHomeDb> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<User>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Report>().HasKey(sc => sc.Id);
            modelBuilder.Entity<Place>().HasKey(sc => sc.Id);

            modelBuilder.Entity<Report>()
               .HasOne<Place>(s => s.Place)
               .WithMany(g => g.Reports)
               .HasForeignKey(s => s.PlaceId);

            modelBuilder.Entity<Report>()
              .HasOne<User>(s => s.User)
              .WithMany(g => g.Reports)
              .HasForeignKey(s => s.UserId);
        }
    }

}


