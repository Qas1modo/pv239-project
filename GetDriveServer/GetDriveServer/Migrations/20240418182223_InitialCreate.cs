using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GetDriveServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Salt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    ReviewText = table.Column<string>(type: "TEXT", nullable: true),
                    PostedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_User_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ride",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DriverId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartLocation = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Destination = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    MaxPassengerCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Departure = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AvailableSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    DriverNote = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Canceled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ride", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ride_User_DriverId",
                        column: x => x.DriverId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRide",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PassengerId = table.Column<int>(type: "INTEGER", nullable: false),
                    RideId = table.Column<int>(type: "INTEGER", nullable: false),
                    PassengerCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Accepted = table.Column<bool>(type: "INTEGER", nullable: false),
                    PassengerNote = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRide", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRide_Ride_RideId",
                        column: x => x.RideId,
                        principalTable: "Ride",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRide_User_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Name", "Password", "Phone", "Salt" },
                values: new object[,]
                {
                    { 1, "user@example.com", "testuser1", "qr4l8Q5yoqCSkXOysKrTeYRQya8lPB1TJVLZEyLvVq6ApHN9nDyPkbeyvLJjfiF/KjcIdHoT+9mAkt5ZUFG1Iw==", "+421123456789", "AgubqMiRegELBk4cc4AgTg==" },
                    { 2, "user1@example.com", "testuser2", "qr4l8Q5yoqCSkXOysKrTeYRQya8lPB1TJVLZEyLvVq6ApHN9nDyPkbeyvLJjfiF/KjcIdHoT+9mAkt5ZUFG1Iw==", "+421123456789", "AgubqMiRegELBk4cc4AgTg==" },
                    { 3, "user2@example.com", "testuser3", "qr4l8Q5yoqCSkXOysKrTeYRQya8lPB1TJVLZEyLvVq6ApHN9nDyPkbeyvLJjfiF/KjcIdHoT+9mAkt5ZUFG1Iw==", "+421123456789", "AgubqMiRegELBk4cc4AgTg==" }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "AuthorId", "PostedAt", "ReviewText", "Score", "UserId" },
                values: new object[] { 1, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pretty Good!", 5, 1 });

            migrationBuilder.InsertData(
                table: "Ride",
                columns: new[] { "Id", "AvailableSeats", "Canceled", "Departure", "Destination", "DriverId", "DriverNote", "MaxPassengerCount", "Price", "StartLocation" },
                values: new object[] { 1, 2, false, new DateTime(2024, 5, 8, 20, 22, 23, 85, DateTimeKind.Local).AddTicks(6251), "Bratislava", 1, "Nebereme nikoho po cestě", 4, 2.1m, "Brno" });

            migrationBuilder.InsertData(
                table: "UserRide",
                columns: new[] { "Id", "Accepted", "PassengerCount", "PassengerId", "PassengerNote", "RideId" },
                values: new object[] { 1, true, 2, 2, "Test", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Review_AuthorId",
                table: "Review",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ride_DriverId",
                table: "Ride",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRide_PassengerId",
                table: "UserRide",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRide_RideId",
                table: "UserRide",
                column: "RideId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "UserRide");

            migrationBuilder.DropTable(
                name: "Ride");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
