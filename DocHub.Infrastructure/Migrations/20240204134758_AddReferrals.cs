using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReferrals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Referrals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referrals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Referrals_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_AppointmentId",
                table: "Referrals",
                column: "AppointmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Referrals");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10f2ba6b-d114-4a9a-ac8f-bf1edbab4135"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "687bd6de-a7c6-41fb-bf19-1888629d6ea8", "AQAAAAIAAYagAAAAEOlYzUFUZxqtPZBN0ouLb/qPbXH4zb6JOoeg//6kImUhOgRj8jFmINBo+ybD6YRK+Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("56123f55-83e3-4d3d-b59f-8a5e250f817e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e1383434-d151-4e58-a885-cab3df973ff3", "AQAAAAIAAYagAAAAEDDPgM3CTQkMvaoKMhkgE40Bm4XnScgKSrdU1GHr5aZSlsa9q3Df1immcLugeu6EOg==" });
        }
    }
}
