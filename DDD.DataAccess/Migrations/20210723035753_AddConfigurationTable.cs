using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDD.DataAccess.Migrations
{
    public partial class AddConfigurationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BigInt", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoomTemperature = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    Area = table.Column<decimal>(type: "decimal(38,2)", precision: 38, scale: 2, nullable: false),
                    CeilingHeight = table.Column<decimal>(type: "decimal(38,2)", precision: 38, scale: 2, nullable: false),
                    CurrentRating = table.Column<decimal>(type: "decimal(38,2)", precision: 38, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "Bit", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configurations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_UserId",
                table: "Configurations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");
        }
    }
}
