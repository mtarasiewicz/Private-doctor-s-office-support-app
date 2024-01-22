using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExpandAppointmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interview",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recommendations",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f13dccbe-d400-4549-b3ef-52dceb0d30b7", "AQAAAAIAAYagAAAAEJk6LN6egIm18ksqGywMidlxWPuJyxMTg48FbMpUxC1hyp87My9MN20OVqDWBGQzgw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7ade4dbd-17c1-40c7-a23e-48d74bea8c3e", "AQAAAAIAAYagAAAAEJL+m9ffW+dsRmtqnuHV2sBTivy+Fz/gPsvtnMV6+rcj4uX/FuC4duUnTlSsfb3h1A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Interview",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Recommendations",
                table: "Appointments");

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
    }
}
