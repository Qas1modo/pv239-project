using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            User user1 = new() { Id = 1, Name = "testuser", Password="test", Email="Test", Salt="test" };
            modelBuilder.Entity<User>().HasData(user1);

            User user2 = new() { Id = 2, Name = "marek", Password = "test", Email = "Test", Salt = "test" };
            modelBuilder.Entity<User>().HasData(user2);

            User user3 = new() { Id = 3, Name = "samuel", Password = "test", Email = "Test", Salt = "test" };
            modelBuilder.Entity<User>().HasData(user3);

            Ride ride1 = new()
            { Id = 1, DriverId=1, Departure=DateTime.Now, DriverNote="Nebereme nikoho po cestě", Destination="Bratislava", StartLocation="Brno", Price=2.1m,
                MaxPassangerCount = 4 };
            modelBuilder.Entity<Ride>().HasData(ride1);

            UserRide userRide1 = new() { RideId = 1, PassengerId = 2, PassengerCount = 2, Id = 1 };
            modelBuilder.Entity<UserRide>().HasData(userRide1);


            Review review1 = new() { Id = 1, AuthorId = 2, ReviewText = "Pretty Good!", Score = 5, UserId = 1 };
            modelBuilder.Entity<Review>().HasData(review1);
        }
    }
}
