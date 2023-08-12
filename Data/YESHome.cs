using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YesHome.Data.Models;

namespace YesHome.Data
{
    public class YESHomeDb : IdentityDbContext<User, Role, int>
    {
        public DbSet<Place> Places { get; set; }
        public DbSet<Report> Reports { get; set; }
        public YESHomeDb(DbContextOptions<YESHomeDb> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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


