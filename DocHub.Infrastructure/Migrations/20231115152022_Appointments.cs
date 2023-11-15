using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Appointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TestProp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ae26e07c-ef26-4577-b6f3-f3fae98d72a7", "AQAAAAIAAYagAAAAEPirjAe4LaIkGhW77qTzu2w9rnbwTN0gwsdU0sN2YDzCfcrdbWh+HfdBjRQgWtDlFQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6e844cef-6ae0-4be9-8aba-a5a2f311fd69", "AQAAAAIAAYagAAAAEAxKhBjHRVNdnfU8Fv01Aidpnpej1kWawnTKfiG2PKrGyMLUMSJ3uyLGam3fGjn+hA==" });
        }
    }
}
