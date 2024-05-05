using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetDriveServer.Migrations
{
    /// <inheritdoc />
    public partial class ProfileMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "AuthorId", "PostedAt", "ReviewText", "Score", "UserId" },
                values: new object[] { 2, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Almost excellent trip!", 4, 1 });

            migrationBuilder.UpdateData(
                table: "Ride",
                keyColumn: "Id",
                keyValue: 1,
                column: "Departure",
                value: new DateTime(2024, 5, 24, 21, 11, 52, 551, DateTimeKind.Local).AddTicks(6632));

            migrationBuilder.InsertData(
                table: "Ride",
                columns: new[] { "Id", "AvailableSeats", "Canceled", "Departure", "Destination", "DriverId", "DriverNote", "MaxPassengerCount", "Price", "StartLocation" },
                values: new object[] { 2, 1, false, new DateTime(2024, 5, 14, 21, 11, 52, 551, DateTimeKind.Local).AddTicks(6711), "Košice", 2, "Beriem psa.", 3, 4.6m, "Bratislava" });

            migrationBuilder.InsertData(
                table: "UserRide",
                columns: new[] { "Id", "Accepted", "PassengerCount", "PassengerId", "PassengerNote", "RideId" },
                values: new object[] { 2, true, 1, 1, "Potrebujem prísť včas", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRide",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ride",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Ride",
                keyColumn: "Id",
                keyValue: 1,
                column: "Departure",
                value: new DateTime(2024, 5, 24, 20, 22, 13, 97, DateTimeKind.Local).AddTicks(4893));
        }
    }
}
