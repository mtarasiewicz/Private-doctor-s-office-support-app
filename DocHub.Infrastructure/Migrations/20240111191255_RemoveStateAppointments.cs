using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStateAppointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d1928122-0df6-4b71-994d-4ccfc67fce5d", "AQAAAAIAAYagAAAAELOm35bnIZYdiCsQ90oEP/vNtTHGdHbft8Ll8VonXBK1E6JU9CLm1jjsM8hn6YrDNA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "57a1c807-c593-471d-828c-2aa54ab0e273", "AQAAAAIAAYagAAAAELzFQ5r21OfHoRIjiIExpt/rJfiHO2dTPkdbgJkTzO+bJi9JSFXyl1HtTuZ4W7C2SA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
