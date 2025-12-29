using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuaforRandevuAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addBarberServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Barbers_BarberId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_BarberId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "BarberId",
                table: "Service");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BarberService",
                columns: table => new
                {
                    BarberId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarberService", x => new { x.BarberId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_BarberService_Barbers_BarberId",
                        column: x => x.BarberId,
                        principalTable: "Barbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarberService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ServiceId",
                table: "Reservations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BarberService_ServiceId",
                table: "BarberService",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Service_ServiceId",
                table: "Reservations",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Service_ServiceId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "BarberService");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ServiceId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "BarberId",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Service_BarberId",
                table: "Service",
                column: "BarberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Barbers_BarberId",
                table: "Service",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
