using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuaforRandevuAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class errFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServicePrice",
                table: "Services",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "Services",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ServiceDuration",
                table: "Services",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "JobStartTime",
                table: "Barbers",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "JobEndTime",
                table: "Barbers",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "BarberName",
                table: "Barbers",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Services",
                newName: "ServicePrice");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Services",
                newName: "ServiceName");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Services",
                newName: "ServiceDuration");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Barbers",
                newName: "JobStartTime");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Barbers",
                newName: "BarberName");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Barbers",
                newName: "JobEndTime");
        }
    }
}
