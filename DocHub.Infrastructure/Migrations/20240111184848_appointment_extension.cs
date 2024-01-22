using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class appointment_extension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "518d6500-a6b9-4657-b4b0-1a3287930996", "AQAAAAIAAYagAAAAEG0z1FSq7p0tJ++L9m8l7uYy5MkbHC5qjECR4b8AZcts2a5BzsfjTA/x+85JNSbG6w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "517b1dd3-a2a0-4ca3-a81c-02b52459a6c3", "AQAAAAIAAYagAAAAEDDP8qfJzQHlTfk+A5mBRbUVBxOJUSaYV2vZFIqT0QZg0jaERQMzGrSFNAns38J91Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "576925c9-8430-451d-bcc0-0b736f2d9215", "AQAAAAIAAYagAAAAEJUELj45fRmDaEAB2AYYsg0y7jvKPaJ8xP5WRZo5Xm++yvRfbZ5Gamq8uSe0HERWUg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b91bf7f4-844e-439f-b6c2-32547d5fb718", "AQAAAAIAAYagAAAAEHl9099oc719uVLjs4qyZEFo0vr3mkB4oYc1mABp2HoHd11X42DKiaIWA3DzFHDViw==" });
        }
    }
}
