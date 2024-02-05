using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTestProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestProp",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1064fcce-794b-49ec-81f2-a5647591f61c", "AQAAAAIAAYagAAAAEDrS4FMSY/PqlhApzbqB1eZa5F52GQC1MF5lG5eGlpFZA0nsoqk3HshIim1xHt2LIw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "65616007-51a6-4f10-8738-58e6fb5efdd8", "AQAAAAIAAYagAAAAEC/OjRCLwO1L+/kh079iXYURSftqkbI0ie7coFnXFEguBu8/+JohYWHIfgO0XWfT5A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestProp",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ee2aa524-6881-45d0-ad56-e369bf3c984c", "AQAAAAIAAYagAAAAEJxJzT3ccTd5qXG84KgpbqulgHGRrQgHhNvpw4qWgo1Od2SQ1/oTep9G9qOcEy3mXg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "12dc906f-19b0-4619-b695-89defb538103", "AQAAAAIAAYagAAAAECPsD/ukYRKMfTAOtcOS4BgovMR7rHl/onyAUjApD+78QStp3JS1mmM/u7jQjmpmPw==" });
        }
    }
}
