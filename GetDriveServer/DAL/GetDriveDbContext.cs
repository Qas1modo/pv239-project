using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class GetDriveDbContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRide> UserRides { get; set; }

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
