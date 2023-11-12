using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DocHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("776ff189-6d50-4c1c-aa39-120007f40bc9"), null, "Patient", "PATIENT" },
                    { new Guid("8fc5bb05-b154-4b14-97b5-caa6be775820"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"), 0, "ae26e07c-ef26-4577-b6f3-f3fae98d72a7", "user@localhost", false, "Jan", "Kowalski", false, null, "USER@LOCALHOST", "USER@LOCALHOST", "AQAAAAIAAYagAAAAEPirjAe4LaIkGhW77qTzu2w9rnbwTN0gwsdU0sN2YDzCfcrdbWh+HfdBjRQgWtDlFQ==", null, false, "582DC540-AAD8-4544-978B-847059FEE03E", false, "user@localhost" },
                    { new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"), 0, "6e844cef-6ae0-4be9-8aba-a5a2f311fd69", "admin@localhost", false, "Administrator", "Administrator", false, null, "ADMIN@LOCALHOST", "ADMIN@LOCALHOST", "AQAAAAIAAYagAAAAEAxKhBjHRVNdnfU8Fv01Aidpnpej1kWawnTKfiG2PKrGyMLUMSJ3uyLGam3fGjn+hA==", null, false, "89A60FE0-0F8A-477D-9953-720E8DA36B73", false, "admin@localhost" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("776ff189-6d50-4c1c-aa39-120007f40bc9"), new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135") },
                    { new Guid("8fc5bb05-b154-4b14-97b5-caa6be775820"), new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e") }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "Allergies", "City", "DateOfBirth", "Email", "FirstName", "HistoryOfDiseases", "LastName", "PeselNumber", "PhoneNumber", "PostalCode", "TakenMedications", "UserId" },
                values: new object[] { new Guid("f2ebb7ea-7e18-4b8d-bb41-a9f0d20722df"), null, null, null, null, "user@localhost", "Jan", null, "Kowalski", null, null, null, null, new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("776ff189-6d50-4c1c-aa39-120007f40bc9"), new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8fc5bb05-b154-4b14-97b5-caa6be775820"), new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e") });

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("f2ebb7ea-7e18-4b8d-bb41-a9f0d20722df"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("776ff189-6d50-4c1c-aa39-120007f40bc9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8fc5bb05-b154-4b14-97b5-caa6be775820"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"));
        }
    }
}
