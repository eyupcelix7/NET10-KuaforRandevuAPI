using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuaforRandevuAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGetServicesByBarberId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarberService_Barbers_BarberId",
                table: "BarberService");

            migrationBuilder.DropForeignKey(
                name: "FK_BarberService_Service_ServiceId",
                table: "BarberService");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Service_ServiceId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarberService",
                table: "BarberService");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "BarberService",
                newName: "BarberServices");

            migrationBuilder.RenameIndex(
                name: "IX_BarberService_ServiceId",
                table: "BarberServices",
                newName: "IX_BarberServices_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarberServices",
                table: "BarberServices",
                columns: new[] { "BarberId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BarberServices_Barbers_BarberId",
                table: "BarberServices",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarberServices_Services_ServiceId",
                table: "BarberServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Services_ServiceId",
                table: "Reservations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarberServices_Barbers_BarberId",
                table: "BarberServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BarberServices_Services_ServiceId",
                table: "BarberServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Services_ServiceId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarberServices",
                table: "BarberServices");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Service");

            migrationBuilder.RenameTable(
                name: "BarberServices",
                newName: "BarberService");

            migrationBuilder.RenameIndex(
                name: "IX_BarberServices_ServiceId",
                table: "BarberService",
                newName: "IX_BarberService_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarberService",
                table: "BarberService",
                columns: new[] { "BarberId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BarberService_Barbers_BarberId",
                table: "BarberService",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarberService_Service_ServiceId",
                table: "BarberService",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Service_ServiceId",
                table: "Reservations",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
