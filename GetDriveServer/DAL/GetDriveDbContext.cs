using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class GetDriveDbContext : DbContext
    {
        public DbSet<Review> Review { get; set; }
        public DbSet<Ride> Ride { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRide> UserRide { get; set; }

        public GetDriveDbContext()
        {
        }

        public GetDriveDbContext(DbContextOptions<GetDriveDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
