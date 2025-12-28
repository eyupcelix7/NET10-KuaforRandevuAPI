using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuaforRandevuAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ServiceAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "JobEndTime",
                table: "Barbers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "JobStartTime",
                table: "Barbers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceDuration = table.Column<TimeOnly>(type: "time", nullable: false),
                    BarberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Barbers_BarberId",
                        column: x => x.BarberId,
                        principalTable: "Barbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_BarberId",
                table: "Service",
                column: "BarberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropColumn(
                name: "JobEndTime",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "JobStartTime",
                table: "Barbers");
        }
    }
}
