using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesToAppoitnmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "42cc5de3-6adf-4c3f-a272-61843f452cc6", "AQAAAAIAAYagAAAAEA6oOXH9fq0kOyuMtQbkXoIRzjr7aFuFt4L5BHW9pdCPffoLOQAe1rWdVpU4ytwPmg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "43eea4f6-c621-4e68-9bb2-371f28a9b67b", "AQAAAAIAAYagAAAAEDSyVKaZofrCXxC6/gDTv1CYXECcqhtnQTuWcWZguPodA96Z1nMKCQ4AD4nliteHgA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Appointments");

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
    }
}
