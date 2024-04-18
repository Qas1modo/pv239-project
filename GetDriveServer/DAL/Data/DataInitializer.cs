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
            User user1 = new() { Id = 1, Name = "testuser1", Password= "qr4l8Q5yoqCSkXOysKrTeYRQya8lPB1TJVLZEyLvVq6ApHN9nDyPkbeyvLJjfiF/KjcIdHoT+9mAkt5ZUFG1Iw==", Email="user@example.com", Salt= "AgubqMiRegELBk4cc4AgTg==", Phone="+421123456789" }; //testtest
            modelBuilder.Entity<User>().HasData(user1);

            User user2 = new() { Id = 2, Name = "testuser2", Password = "qr4l8Q5yoqCSkXOysKrTeYRQya8lPB1TJVLZEyLvVq6ApHN9nDyPkbeyvLJjfiF/KjcIdHoT+9mAkt5ZUFG1Iw==", Email = "user1@example.com", Salt = "AgubqMiRegELBk4cc4AgTg==", Phone = "+421123456789" }; //testtest
            modelBuilder.Entity<User>().HasData(user2);

            User user3 = new() { Id = 3, Name = "testuser3", Password = "qr4l8Q5yoqCSkXOysKrTeYRQya8lPB1TJVLZEyLvVq6ApHN9nDyPkbeyvLJjfiF/KjcIdHoT+9mAkt5ZUFG1Iw==", Email = "user2@example.com", Salt = "AgubqMiRegELBk4cc4AgTg==", Phone = "+421123456789" }; //testtest
            modelBuilder.Entity<User>().HasData(user3);

            Ride ride1 = new()
            { Id = 1, DriverId=1, Departure=DateTime.Now.AddDays(20), DriverNote="Nebereme nikoho po cestě", Destination="Bratislava", StartLocation="Brno", Price=2.1m,
                MaxPassengerCount = 4, AvailableSeats= 2, Canceled = false };
            modelBuilder.Entity<Ride>().HasData(ride1);

            UserRide userRide1 = new() { RideId = 1, PassengerId = 2, PassengerCount = 2, Id = 1, Accepted = true, PassengerNote = "Test" };
            modelBuilder.Entity<UserRide>().HasData(userRide1);


            Review review1 = new() { Id = 1, AuthorId = 2, ReviewText = "Pretty Good!", Score = 5, UserId = 1 };
            modelBuilder.Entity<Review>().HasData(review1);
        }
    }
}
