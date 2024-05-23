using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetDriveServer.Migrations
{
    /// <inheritdoc />
    public partial class MapMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Ride",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Departure", "DestinationLatitude", "DestinationLongitude", "StartLatitude", "StartLongitude" },
                values: new object[] { new DateTime(2024, 6, 12, 19, 0, 11, 940, DateTimeKind.Local).AddTicks(2725), 48.159260250000003, 17.139658691421687, 49.192244299999999, 16.611338199999999 });

            migrationBuilder.UpdateData(
                table: "Ride",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Departure", "DestinationLatitude", "DestinationLongitude", "StartLatitude", "StartLongitude" },
                values: new object[] { new DateTime(2024, 6, 2, 19, 0, 11, 940, DateTimeKind.Local).AddTicks(2790), 48.717227200000004, 21.249677399999999, 48.159260250000003, 17.139658691421687 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Ride",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Departure", "DestinationLatitude", "DestinationLongitude", "StartLatitude", "StartLongitude" },
                values: new object[] { new DateTime(2024, 6, 12, 1, 51, 30, 912, DateTimeKind.Local).AddTicks(7789), 0.0, 0.0, 0.0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Ride",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Departure", "DestinationLatitude", "DestinationLongitude", "StartLatitude", "StartLongitude" },
                values: new object[] { new DateTime(2024, 6, 2, 1, 51, 30, 912, DateTimeKind.Local).AddTicks(7862), 0.0, 0.0, 0.0, 0.0 });
        }
    }
}
