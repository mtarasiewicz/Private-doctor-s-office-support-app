using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateToVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4d09534d-1e19-4635-93d1-d15aed21ad9f", "AQAAAAIAAYagAAAAEFebBoPiV85iV6vC8+Lde550a8KqRJcaJSlnOEFsFF4NPuTK6h3qrCmaHerJek1d4A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "96725939-a189-4fe6-a00f-316a4ec12245", "AQAAAAIAAYagAAAAEMufV12KHpTe6KUaHNHre78v/r6woj/u3mPwEYiKCbtoXV66BdMf+a/09uVnmM7MuQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "965f22bf-2d49-463e-96a8-af46c5507a14", "AQAAAAIAAYagAAAAEE8GzRh3ZiBx2j1chGVhAQNjaEhAA9YIE3NKEhCpkbzUtL3JWha1vtUAWAc8WUp1+w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "27ef9482-8946-4490-9c36-7889e6fcf13b", "AQAAAAIAAYagAAAAEAQW/76TLLmYutFTuxENJ2IvYEqa0QkRbbY6lwejW3qbcStb5GjBA83KQJnRRDCo0A==" });
        }
    }
}
