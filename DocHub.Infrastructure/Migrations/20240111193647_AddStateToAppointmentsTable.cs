using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStateToAppointmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Finished",
                table: "Appointments",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ac958be5-0af6-40da-b17f-99d03a88df3c", "AQAAAAIAAYagAAAAEELXuF3BgczMsLET7DW1djptqJPu/4R3JjasfxRay6mfIWJ82HQ56rxBC8Ru1/CFqA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "76ca9f66-069d-44ec-a687-34f942f30645", "AQAAAAIAAYagAAAAEE3hjkEKfNdj53LwhvWURsB9W5Lk9+nce4we9r4HdRarr/7IwpjYObp1H9aPOKHTzg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "Finished",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

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
    }
}
